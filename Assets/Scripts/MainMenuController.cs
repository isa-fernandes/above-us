using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void startGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    
    public void openMenu ()
    {
        SceneManager.LoadScene(0);
    }

    public void Resume ()
    {
        GameObject.Find("Canvas/GameManager").GetComponent<GameManager>().Resume();
    }

    public void RestartGame ()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
