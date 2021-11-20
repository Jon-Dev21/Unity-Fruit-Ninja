using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// This script counts the score,
/// </summary>
public class GameManager : MonoBehaviour
{
    // This header will appear in the Inspector
    [Header("Score Elements")]
    // Game score in integer form
    public int score;

    // Game score in text form. Used to show score in the GUI
    public Text scoreText;

    // Game highscore in text form. Used to show highscore in the GUI
    public Text highScoreText;

    // This header will appear in the Inspector
    [Header("Game Over")]
    // Game Over Panel
    public GameObject gameOverPanel;
    // Score text in the game over panel
    public Text gameOverScoreText;

    /// <summary>
    /// When the game starts, display the high score.
    /// </summary>
    private void Awake()
    {
        // Deactivate the Game Over Panel when the game starts
        gameOverPanel.SetActive(false);

        // Assign the HighScore to the HighScore Text when the game starts
        highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore");
    }

    /// <summary>
    /// This method increases the score and assigns it to our score text.
    /// </summary>
    public void IncreaseScore()
    {
        // Increment Score
        score++;

        // Assign score to score text in order to display it in the gui.
        scoreText.text = "Score: "+score.ToString();
        
        

        // If the score is greater than the highscore.
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
            // Assign HighScore to highscore text in order to display it in the gui.
            highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore").ToString();
        }
    }

    /// <summary>
    /// Method runs whenever the bomb is sliced. 
    /// Stops the game when a bomb is hit.
    /// </summary>
    public void OnBombHit()
    { 
        Time.timeScale = 0;                 // This line of code stops the game.

        // Set the score to the gameOverScoreText
        gameOverScoreText.text = "Score: " + score.ToString();

        gameOverPanel.SetActive(true);     // Activate the Game Over Panel when the game ends
    }

    /// <summary>
    /// This method restarts the game.
    /// </summary>
    public void RestartGame()
    {
        // One alternative
        //Time.timeScale = 1;
        //SceneManager.LoadScene(0);

        // Another way to restart the game
        Time.timeScale = 1;
        score = 0;
        scoreText.text = "Score: 0";

        gameOverPanel.SetActive(false);
        gameOverScoreText.text = "Score: 0";

        // Destroy each game object
        foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag("Interactable"))
        {
            Destroy(gameObj);
        }

    }
}
