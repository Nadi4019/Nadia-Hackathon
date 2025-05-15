using UnityEngine;

public class RotateOnLook : MonoBehaviour
{
   public float rayDistance = 10f;
   public KeyCode rotateKey = KeyCode.E;
   private bool hasRotated = false;

   void Update()
   {
    Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, rayDistance))
    {
        if (hit.transform == transform && Input.GetKeyDown(rotateKey))
        {
            RotateObject();
        }
    }

   }

   void RotateObject()
   {
    transform.Rotate(0f, 90f, 0f);
    hasRotated = true;
   }
}
