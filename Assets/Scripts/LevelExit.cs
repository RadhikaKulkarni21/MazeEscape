using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    //coroutines - wait a second and load next scene
    [SerializeField] float levelLoadDelay = 1f;
    void OnTriggerEnter2D(Collider2D collision)
    {
        //adding tag because exit sign detecting and starting next level when bullet hits
        if(collision.tag == "Player")
        {
            StartCoroutine(LoadNextLevel());
        }

    }
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
            FindAnyObjectByType<ScenePersist>().ResetScenePersist();
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
