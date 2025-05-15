using UnityEngine;
using System.Collections.Generic;

public class Trigger : MonoBehaviour
{
    public List<GameObject> objects;

    void OnTriggerEnter(UnityEngine.Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Activate();
        }
    }

    public void Activate()
    {
        foreach(GameObject objects in objects)
        {
            objects.GetComponent<Rigidbody>().useGravity = true;
        }

    }
}
