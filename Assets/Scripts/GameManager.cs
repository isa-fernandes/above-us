using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform spawnPoint;

    [SerializeField] GameObject pauseMenu;

    [SerializeField] GameObject[] hearts;

    bool isPaused;
    int lives = 3;

    // Start is called before the first frame update
    void Start()
    {
        if (spawnPoint != null)
        {
            GameObject player = Instantiate(playerPrefab, spawnPoint.position, playerPrefab.transform.rotation);
            player.GetComponent<PlayerScript>().HitByEnemy += Player_HitByEnemy;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HideHeart(int imageIndex)
    {
        hearts[imageIndex].gameObject.SetActive(false);
    }

    void Player_HitByEnemy ()
    {
        lives--;

        if (lives < 0)
            GameOver();
        else
            HideHeart(lives);
    }

    private void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Pause ()
    {
        isPaused = true;
        pauseMenu.SetActive(isPaused);
        Time.timeScale = 0f;
    }

    public void Resume ()
    {
        isPaused = false;
        pauseMenu.SetActive(isPaused);
        Time.timeScale = 1f;
    }
}
