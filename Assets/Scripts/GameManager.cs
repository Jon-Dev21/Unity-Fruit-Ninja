using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

    // This header will appear in the Inspector
    [Header("Game Over")]
    // Game Over Panel
    public GameObject gameOverPanel;
    // Score text in the game over panel
    public Text gameOverScoreText;

    // An array of audio clips for the slice sounds.
    public AudioClip[] sliceSounds;
    
    // Audio clip for the fruit spawn sound
    public AudioClip fruitSpawnSound;

    // Audio clip for the bomb explosion.
    public AudioClip bombExplosionSound;

    // Audio source used to play sounds internally.
    private AudioSource audioSource;

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
    /// </summary>
    public IEnumerator OnBombHit()
    {
        Debug.Log("Bomb Hit Called");
        // Advertisement.Show();
        Time.timeScale = 0.01f;                 // This line of code stops the game.

        // Set the score to the gameOverScoreText
        gameOverScoreText.text = "Score: " + score.ToString();
        while (true)
        {
            yield return new WaitForSeconds(0.013f);
            Debug.Log("Activate Panel");
            gameOverPanel.SetActive(true);     // Activate the Game Over Panel when the game ends
            Time.timeScale = 0f;                 // This line of code stops the game.
            break;
        }
        
    }

    /// <summary>
    /// Calls the Show Game Over Panel method with a delay of 1.3 seconds. 
    /// (In time for the bomb explosion sound)
    /// </summary>
    public void InvokeGameOverPanel()
    {
        StartCoroutine(OnBombHit());
        //Invoke("ShowGameOverPanel", 1.3f);
    }

    /// <summary>
    /// This method shows the gameOver Panel.
    /// </summary>
    private void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);     // Activate the Game Over Panel when the game ends
        Debug.Log("Show GameOver Panel");
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
