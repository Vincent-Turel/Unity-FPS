using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    
    public GameObject deathScreenText;
    public GameObject deathScreenImage;
    public GameObject restartButton;
    public GameObject quitButton;
    public GameObject crosshair;
    public Camera camera;
    public GameObject progressBar;
    
    void Start()
    {
        deathScreenImage.SetActive(false);
        deathScreenText.SetActive(false);
        restartButton.SetActive(false);
        quitButton.SetActive(false);
        crosshair.SetActive(true);
        progressBar.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quit();
        }
        if (PlayerInfo.playerDead)
        {
            camera.cullingMask &= ~(1 << LayerMask.NameToLayer("Player"));
            deathScreenImage.SetActive(true);
            deathScreenText.GetComponent<Text>().text = "Game Over \n Score: "+ PlayerInfo.score;
            deathScreenText.SetActive(true);
            restartButton.SetActive(true);
            quitButton.SetActive(true);
            crosshair.SetActive(false);
            progressBar.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            if (Input.GetKeyDown(KeyCode.R))
            {
                restart();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                quit();
            }
        }
    }

    public static void restart()
    {
        PlayerInfo.score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public static void quit()
    {
        Application.Quit();
    }
}