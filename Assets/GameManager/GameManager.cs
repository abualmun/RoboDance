using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    bool gamePaused;
    public float score;
    float highScore;
    [SerializeField] float slowTimePeriod;
    public float zOffset = 1.05f;

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
    [SerializeField] GameObject extraLife;
    [SerializeField] TMP_Text scoreMultiplierText;
    [SerializeField] Image scoreMultiplierImage;
    public GameObject slowTimeButton;

    ///////////////////////////////////
    public List<GameObject> walls = new List<GameObject>();
    public static GameManager gameManager;
    [SerializeField] AudioClip click;
    int powerupsCount;
    int wallsCount;
    bool extraLifeCooldown;
    public bool slowMotionBool;
    public bool isJumping;
    public float returnTimer;
    public float scoreMultiplier;

    private void Awake()
    {
        gameManager = this;
    }
    void Start()
    {
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
        if (scoreMultiplier > 0)
        {
            scoreMultiplier -= Time.deltaTime;
        }
        scoreMultiplierImage.fillAmount = (scoreMultiplier % 24) / 24;
        scoreMultiplierText.text = "x" + ((int)(scoreMultiplier / 24)).ToString();
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

        if (powerupsCount * 3 < wallsCount)
        {
            powerupsCount++;
            Instantiate(powerups[Random.Range(0, powerups.Length)],
            new Vector3(Random.Range(-2, 2), 0.6f, Random.Range(63, 78)),
            Quaternion.identity);
        }
        returnTimer -= Time.deltaTime;
        extraLife.SetActive(hasExtraLife);
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
        slowMotionBool = true;
        yield return new WaitForSeconds(slowTimePeriod);
        slowMotionBool = false;
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
        score += 500 * (int)(scoreMultiplier / 24);
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
