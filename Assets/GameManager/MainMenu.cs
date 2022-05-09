using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static int highScore;
    AudioSource audioSource;
    [SerializeField] AudioClip click;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        highScore = PlayerPrefs.GetInt("HighScore", 0);

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartGame()
    {
        SceneManager.LoadScene("RoboDance");
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
