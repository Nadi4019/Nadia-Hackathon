using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{

    // Name of the scene to load
    public string sceneToLoad;
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider has the "Player" tag
        if (other.CompareTag("Player"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
        }

    }
}