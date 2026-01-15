using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    
    public LevelLoader levelLoader;

    public void StartGame()
    {
        levelLoader.LoadNextLevel();
    }

    public void Options()
    {
        Debug.Log("Options");
    }

    public void Quit()
    {
        Application.Quit();
    }

}
