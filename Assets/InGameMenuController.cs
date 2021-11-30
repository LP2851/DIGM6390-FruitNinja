using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenuController : MonoBehaviour
{
    [Serializable]
    enum MenuState
    {
        PLAYING, OVER
    }

    [SerializeField] private MenuState currentState = MenuState.PLAYING;
    
    [Space, SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Text playerScoreText, highScoreText, coinsEarnedText, playerCoinsText;
    
    [Space, SerializeField, Range(0f, 1f)] private float scoreToCoinsMultiplier = 0.1f;

    [Space, SerializeField] private NinjaPlayer player;

    [ContextMenu("End Game")]
    private void EndGame()
    {
        ChangeState(false);
    }

    public void ChangeState(bool isPlaying)
    {
        
        MenuState nextState = (isPlaying) ? MenuState.PLAYING : MenuState.OVER;

        if (nextState == currentState) return;
        currentState = nextState;
        switch (currentState)
        {
            case MenuState.PLAYING:
                RestartGame();
                break;
            case MenuState.OVER:
                GameIsOver();
                break;
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene("NinjaGame");
    }

    void GameIsOver()
    {
        gameOverScreen.SetActive(true);
        int score = GetGameScore();
        int highScore = PlayerDataImporter.instance.highScore;
        bool hasNewHighScore = score > highScore;
        int coinsCollected = (int) (score * scoreToCoinsMultiplier);
        
        // Score
        playerScoreText.text = "Score: " + score;
        
        // High Score
        highScoreText.text = (hasNewHighScore) ? "New High Score: " + score + "!!!" : "High Score: " + highScore;
        highScoreText.color = (hasNewHighScore) ? coinsEarnedText.color : Color.white; 
        if (hasNewHighScore) PlayerDataImporter.instance.SetNewHighScore(score);
        
        // Coins Earned
        coinsEarnedText.text = "Coins Earned: " + coinsCollected;
        
        // Total Money
        PlayerDataImporter.instance.GiveMoney(coinsCollected);
        playerCoinsText.text = "" + PlayerDataImporter.instance.amountOfMoney;
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    int GetGameScore()
    {
        return player.GetScore();
    }
    
}
