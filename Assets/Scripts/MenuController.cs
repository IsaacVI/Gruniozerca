using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

   
    public GameObject lives;
    public Image[] live = new Image[3];
    public Button play;
    public Button exit;
    public static MenuController singleton;
    public Text score;
    public Text bestScore;
    public GameObject mobileInput;
    public bool mobileInputTest;
    public GameObject musicController;
    public GameObject options;
    public Button optionsButton;
    public Text musicText, soundText, antialiasingText, postProcessText;
    public MonoBehaviour antialiasing;
    public MonoBehaviour[] postProcess;


    public void Awake()
    {
        singleton = this;
        GameController.bestScore = PlayerPrefs.GetInt("BestScore");
        GameController.music = PlayerPrefs.GetInt("NoMusic") == 1 ? false : true;
        GameController.sound = PlayerPrefs.GetInt("NoSound") == 1 ? false : true;
        GameController.antialiasing = PlayerPrefs.GetInt("NoAntialiasing") == 1 ? false : true;
        GameController.postProcess = PlayerPrefs.GetInt("NoPostProcess") == 1 ? false : true;
        antialiasing.enabled = GameController.antialiasing;
        for (int i = 0; i < postProcess.Length; i++)
            postProcess[i].enabled = GameController.postProcess;
        musicText.text = "MUSIC " + (GameController.music ? "ON" : "OFF");
        soundText.text = "SOUND " + (GameController.sound ? "ON" : "OFF");
        postProcessText.text = "VIDEO EFFECTS " + (GameController.postProcess ? "ON" : "OFF");
        antialiasingText.text = "ANTIALAISING " + (GameController.antialiasing ? "ON" : "OFF");
        musicController.SetActive(GameController.music);
        UpdateBestScore();
    }

    public void ShowOptions()
    {
        options.SetActive(true);
    }
    public void ExitOptions()
    {
        options.SetActive(false);
    }

    public void SetPostProcess()
    {
        GameController.postProcess = !GameController.postProcess;
        PlayerPrefs.SetInt("NoPostProcess", GameController.postProcess ? 0 : 1);
        PlayerPrefs.Save();
        postProcessText.text = "VIDEO EFFECTS " + (GameController.postProcess ? "ON" : "OFF");
        for (int i = 0; i < postProcess.Length; i++)
            postProcess[i].enabled = GameController.postProcess;
    }
    public void SetAntialiasing()
    {
        GameController.antialiasing = !GameController.antialiasing;
        PlayerPrefs.SetInt("NoAntialiasing", GameController.antialiasing ? 0 : 1);
        PlayerPrefs.Save();
        antialiasingText.text = "ANTIALAISING " + (GameController.antialiasing ? "ON" : "OFF");
        antialiasing.enabled = GameController.antialiasing;
    }

    public void SetSound()
    {
        GameController.sound = !GameController.sound;
        PlayerPrefs.SetInt("NoSound", GameController.sound ? 0 : 1);
        PlayerPrefs.Save();
        soundText.text = "SOUND " + (GameController.sound ? "ON" : "OFF");

    }

    public void SetMusic()
    {
        GameController.music = !GameController.music;
        PlayerPrefs.SetInt("NoMusic", GameController.sound ? 0 : 1);
        PlayerPrefs.Save();
        musicController.SetActive(GameController.music);
        musicText.text = "MUSIC " + (GameController.music ? "ON" : "OFF");

    }

    public void StartGame()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || mobileInputTest)
        {
            mobileInput.SetActive(true);
        }
            GameController.score = 0;
        UpdateScore();
        PlayerController.lives = 3;
        lives.SetActive(true);
        UpdateLives();
        play.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
        optionsButton.gameObject.SetActive(false);
        GameController.isPlay = true;
    }

    public static void EndGame()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || singleton.mobileInputTest)
        {
            singleton.mobileInput.SetActive(false);
        }
        GameController.isPlay = false;
        singleton.lives.SetActive(false);
        singleton.optionsButton.gameObject.SetActive(true);

        singleton.play.gameObject.SetActive(true);
        singleton.exit.gameObject.SetActive(true);
        if (GameController.score > GameController.bestScore)
        {
            GameController.bestScore = GameController.score;
            PlayerPrefs.SetInt("BestScore",GameController.bestScore);
            PlayerPrefs.Save();
            UpdateBestScore();
        }
    }
    public static void UpdateScore()
    {
        singleton.score.text = GameController.score.ToString();
    }

    public static void UpdateBestScore()
    {
        singleton.bestScore.text = GameController.bestScore.ToString();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public static void UpdateLives()
    {
        for (int i = 0; i < singleton.live.Length; i++)
            singleton.live[i].gameObject.SetActive(i < PlayerController.lives ? true : false);

    }
}
