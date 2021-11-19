using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script counts the score,
/// </summary>
public class GameManager : MonoBehaviour
{
    // Game score in integer form
    public int score;

    // Game score in text form. Used to show score in the GUI
    public Text scoreText;


    // Game highscore in text form. Used to show highscore in the GUI
    public Text highScoreText;

    /// <summary>
    /// When the game starts, display the high score.
    /// </summary>
    private void Awake()
    {
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
    }
}
