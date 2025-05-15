using UnityEngine;

public class Void : MonoBehaviour
{
    [SerializeField] private GameObject[] ObjectsToActivate;
    [SerializeField] private GameObject[] ObjectsToDeActivate;
    void Awake()
    {
        foreach (GameObject obj in ObjectsToActivate)
        {
            obj.SetActive(true);
        }

        foreach (GameObject obj in ObjectsToDeActivate)
        {
            obj.SetActive(false);
        }
    }
}
