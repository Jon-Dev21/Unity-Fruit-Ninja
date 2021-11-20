using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

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

    // ------------------------------------------------------------------------------
    // This header will appear in the Inspector
    [Header("Game Over")]

    // Game Over Panel
    public GameObject gameOverPanel;

    // Score text in the game over panel
    public Text gameOverScoreText;

    // ------------------------------------------------------------------------------
    [Header("Sounds")]
    // An array of audio clips for the slice sounds.
    public AudioClip[] sliceSounds;
    
    // Audio clip for the fruit spawn sound
    public AudioClip fruitSpawnSound;

    // Audio clip for the bomb explosion.
    public AudioClip bombExplosionSound;

    // Audio source used to play sounds internally.
    private AudioSource audioSource;

    // ------------------------------------------------------------------------------

    /// <summary>
    /// When the game starts, display the high score.
    /// </summary>
    private void Awake()
    {
        // Initializing Advertisements
        // (For this to work, you need to pass in your Game Id as a string parameter)
        //Advertisement.Initialize("");

        // Deactivate the Game Over Panel when the game starts
        gameOverPanel.SetActive(false);

        // Assign the HighScore to the HighScore Text when the game starts
        highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore");

        // Initializing Audio Source
        audioSource = GetComponent<AudioSource>();
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
    /// Plays the game over sound and shows the GameOver panel
    /// </summary>
    public IEnumerator OnBombHit()
    {
        // Advertisement.Show();
        Time.timeScale = 0.01f;                 // This line of code slows down the game noticeably.
        // Need to slow down the game and not set it to 0
        // for the yield return to work

        // Set the score to the gameOverScoreText
        gameOverScoreText.text = "Score: " + score.ToString();

        // Loop used for the yield return to work.
        // Inside this loop, I show the game over panel after 1.3 seconds.
        while (true)
        {
            // Wait for 0.013 seconds.
            // This number was calculated by multiplying 0.01 by 1.3 seconds.
            // The explosion takes 1.3 seconds to be heard.
            // Since I wanted to show the game over panel when the explosion sound came in
            // after the bomb-omb sound, I used this value to time it perfectly.
            yield return new WaitForSeconds(0.013f);

            gameOverPanel.SetActive(true);     // Activate the Game Over Panel when the game ends
            Time.timeScale = 0f;               // This line of code stops the game.
            break;                             // Break from the while loop
        }
    }

    /// <summary>
    /// Calls the OnHitBomb coroutine. 
    /// </summary>
    public void InvokeGameOver()
    {
        // Execute OnBombHit coroutine.
        StartCoroutine(OnBombHit());
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

    /// <summary>
    /// Method used to play a random slice sound.
    /// </summary>
    public void PlayRandomSliceSound()
    {
        // Get a random audio clip from the sliceSounds array and play it
        AudioClip randomSliceSound = sliceSounds[Random.Range(0, sliceSounds.Length)];
        audioSource.PlayOneShot(randomSliceSound);
    }

    /// <summary>
    /// This method plays the fruit spawn sound.
    /// </summary>
    public void PlayFruitSpawnSound()
    {
        if(fruitSpawnSound)
        {
            // Play the fruit spawn sound.
            audioSource.PlayOneShot(fruitSpawnSound);
        }
    }

    /// <summary>
    /// This method plays the Bomb Explosion Sound
    /// </summary>
    public void PlayBombExplosionSound()
    {
        if (bombExplosionSound)
        {
            // Play the Bomb Explosion sound.
            audioSource.PlayOneShot(bombExplosionSound);
        }
    }
}
