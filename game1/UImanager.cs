




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject pauseScreen;
    public GameObject levelUI;
    public GameObject levelsScreen;
    public float moveX;
    public float moveY;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void closePauseScreen()
    {

        pauseScreen.SetActive(false);
        levelUI.SetActive(true);
    }
    public void showPauseScreen()
    {

        pauseScreen.SetActive(true);
        levelUI.SetActive(false);

    }

    public void showLoseScreen()
    {

        loseScreen.SetActive(true);
        levelUI.SetActive(false);

    }

    public void loadLevel(string level)
    {

        SceneManager.LoadScene(level);

    }
    public void showLevelsScreen()
    {
        Debug.Log("levels btn click");
        pauseScreen.SetActive(false);
        levelsScreen.SetActive(true);
        levelUI.SetActive(false);

    }


    public void PlayerMoveRight_Down()
    {

        moveX = 1;
        Debug.Log("moveX:" + moveX);

    }

    public void PlayerMoveRightUp()
    {

        moveX = 0;

    }

    public void PlayerMoveLeft_Down()
    {

        moveX = -1;

    }
    public void PlayerMoveLeft_Up()
    {

        moveX = 0;

    }

    public void PlayerMoveUp_Down()
    {

        moveY = 1;

    }
    public void PlayerMoveUp_Up()
    {

        moveY = 0;

    }



}
