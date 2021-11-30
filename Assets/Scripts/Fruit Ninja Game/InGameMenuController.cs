using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This class controls the UI elements during the game (in the NinjaGame scene).
/// </summary>
public class InGameMenuController : MonoBehaviour
{
    // What state is the UI in
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

    /// <summary>
    /// Changes the UI state based on if the game is playing or is over
    /// </summary>
    /// <param name="isPlaying">If the game is playing then true, otherwise false </param>
    public void ChangeState(bool isPlaying)
    {
        // Getting the next state
        MenuState nextState = (isPlaying) ? MenuState.PLAYING : MenuState.OVER;

        // Making sure this code only fires when there is actually a change in state.
        if (nextState == currentState) return;
        currentState = nextState;
        
        // Restarts the scene if PLAYING, otherwise shows the game over screen
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

    /// <summary>
    /// Reloads the NinjaGame scene. 
    /// </summary>
    void RestartGame()
    {
        SceneManager.LoadScene("NinjaGame");
    }

    /// <summary>
    /// Called when the game state is changed to OVER. Sets all of the text elements to the correct values
    /// and activates the game over screen.
    /// </summary>
    void GameIsOver()
    {
        gameOverScreen.SetActive(true);
        
        // Getting important values
        int score = player.GetScore();
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

    /// <summary>
    /// Loads the MainMenu scene. Used by a button.
    /// </summary>
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
