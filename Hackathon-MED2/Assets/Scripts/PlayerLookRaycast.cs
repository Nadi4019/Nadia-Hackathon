using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLookRaycast : MonoBehaviour
{
    private bool readyToInteract = false;
    private RaycastHit hit;

    [SerializeField] private GameObject interactButton;


    void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        

        if (Physics.Raycast(ray, out hit, 3))
        {
            Debug.DrawLine(ray.origin, hit.point);

            if(hit.collider.gameObject.GetComponent<Interactable>() == null)
            {
                readyToInteract = false;
                interactButton.SetActive(false);
                return;
            }
            interactButton.SetActive(true);
            readyToInteract = true;

            if (Input.GetKey(KeyCode.E))
            {
                hit.collider.gameObject.GetComponent<Interactable>().Interact();
            }
        }
        
    }

    public void activateInteractable()
    {
        if (readyToInteract)
        {
            Debug.Log("Interacted");
            hit.collider.gameObject.GetComponent<Interactable>().Interact();
        }
    }
}
