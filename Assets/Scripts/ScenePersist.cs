using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    void Awake()
    {
        //find all the game sessions
        int numScenePersists = FindObjectsByType<ScenePersist>(FindObjectsSortMode.None).Length;

        if (numScenePersists > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }

    public void ResetScenePersist()
    {
        Destroy(gameObject);
    }
}
