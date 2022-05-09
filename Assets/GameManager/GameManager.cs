using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    bool gamePaused;
    public float score;
    float highScore;
    [SerializeField] float slowTimePeriod;

    // Components ///////////////
    [SerializeField] GameObject[] wallPrefabs;
    [SerializeField] GameObject[] powerups;

    [SerializeField] float respawnPosition;
    AudioSource audioSource;

    // Levels /////
    public int level = 1;
    public bool gameOver;
    public bool hasExtraLife;
    public bool hasSlowTime;
    Vector3 defaultLeveltextHight;
    // UI Components///////////////////
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject playMenu;
    [SerializeField] GameObject loseMenu;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text finalScoreText;
    public GameObject slowTimeButton;

    ///////////////////////////////////
    public List<GameObject> walls = new List<GameObject>();
    public static GameManager gameManager;
    [SerializeField] AudioClip click;
    int powerupsCount;
    int wallsCount;
    bool extraLifeCooldown;


    void Start()
    {
        gameManager = this;
        GameObject newWall = Instantiate(
        wallPrefabs[Random.Range(0, wallPrefabs.Length)],
        new Vector3(Random.Range(-2.5f, 2.5f), 0.6f, 40),
        Quaternion.identity
        );
        walls.Add(newWall);
        wallsCount -= -1;

    }

    void Update()
    {
        if (gameOver) return;
        if (Input.GetKeyDown(KeyCode.Escape)) TogglePause();

        scoreText.text = "Score: " + (int)score;
        if (wallsCount > level * 5)
        {
            LevelUp();
        }

        if (walls[walls.Count - 1].transform.position.z <= respawnPosition)
        {
            GameObject newWall = Instantiate(
            wallPrefabs[Random.Range(0, wallPrefabs.Length)],
            new Vector3(Random.Range(-2, 2), 0.6f, 60),
            Quaternion.identity
            );
            walls.Add(newWall);

            wallsCount -= -1;
        }

        if (powerupsCount < wallsCount)
        {
            powerupsCount++;
            Instantiate(powerups[Random.Range(0, powerups.Length)],
            new Vector3(Random.Range(-2, 2), 0.6f, Random.Range(63, 78)),
            Quaternion.identity);
        }
    }
    void LevelUp()
    {
        level++;
    }
    public void UseSlowTime()
    {
        slowTimeButton.SetActive(false);
        StartCoroutine(SlowTime());
    }
    IEnumerator SlowTime()
    {
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime *= 0.5f;
        yield return new WaitForSeconds(slowTimePeriod);
        Time.timeScale = 1;
        Time.fixedDeltaTime *= 2f;
    }
    public void GetExtraLife()
    {
        hasExtraLife = true;
    }
    IEnumerator ExtraLifeEffect()
    {
        extraLifeCooldown = true;
        yield return new WaitForSeconds(2);
        hasExtraLife = false;
        extraLifeCooldown = false;
    }
    public void AddWallScore()
    {
        score += 500;
    }

    public void TogglePause()
    {
        playMenu.SetActive(gamePaused);
        pauseMenu.SetActive(!gamePaused);
        gamePaused = !gamePaused;
        Time.timeScale = gamePaused ? 0 : 1;
    }
    public void EndGame()
    {
        if (hasExtraLife)
        {
            if (!extraLifeCooldown)
                StartCoroutine(ExtraLifeEffect());
            return;
        }
        level = -7;
        if (MainMenu.highScore < score)
        {
            MainMenu.highScore = (int)score;
            PlayerPrefs.SetInt("HighScore", MainMenu.highScore);

        }
        gameOver = true;
        playMenu.SetActive(false);
        pauseMenu.SetActive(false);
        loseMenu.SetActive(!false);
        finalScoreText.text = "Final Score: " + (int)score + "\nHighScore: " + MainMenu.highScore;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void ClickSound()
    {
        // audioSource.PlayOneShot(click);
    }
}
