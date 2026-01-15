using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class LevelLoader : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1.0f;


    void Update()
    {
        
    }

    public void LoadNextLevel()
    {      
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelInd)
    {
        // play anim, wait for it to end and load new scene.

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelInd);

    }
}
