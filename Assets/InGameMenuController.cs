using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenuController : MonoBehaviour
{
    [SerializeField]
    enum MenuState
    {
        PLAYING, OVER
    }

    [SerializeField] private MenuState currentState = MenuState.PLAYING;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Text playerScoreText, highScoreText, coinsEarnedText, playerCoinsText;
    [SerializeField, Range(0f, 1f)] private float scoreToCoinsMultiplier = 0.1f;

    public void ChangeState(bool isPlaying)
    {
        currentState = (isPlaying) ? MenuState.PLAYING : MenuState.OVER;
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
        return 0;
        // return player.score???
    }
    
}
