using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject _splash;
    public void PlayGame()
    {
        if (Input.GetKey(KeyCode.Space)) SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        //Menu
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _splash.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(1);
        }
    }
}