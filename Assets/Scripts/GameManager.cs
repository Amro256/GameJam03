using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject timeOverPanel;
    private bool ispaused = false;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource cauldonSFX;

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        timeOverPanel.SetActive(false);
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
           PauseGame();
        }
    }

    //Method

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("LevelScene");
    }

    public void  PauseGame()
    {
        ispaused = !ispaused;

        if(ispaused)
        {
            Time.timeScale = 0f;
            bgmSource.Pause();
            cauldonSFX.Pause();
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            bgmSource.Play();
            cauldonSFX.Play();
            pauseMenu.SetActive(false);
        }
    }

    public void ShowCredits()
    {
        mainMenu.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        mainMenu.SetActive(true);
        creditsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Game Quit!");
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene("LevelScene");
    }

    public void TimeOverScreen()
    {
        timeOverPanel.SetActive(true);
        bgmSource.Pause();
        cauldonSFX.Pause();
    }
}
