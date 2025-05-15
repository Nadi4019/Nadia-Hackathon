using UnityEngine;

public class PickUp : MonoBehaviour, Interactable
{
    [SerializeField] private GameObject playerHand;
    [SerializeField] private Transform playerHandLocation;

    [SerializeField] private Vector3 pickedUpRotation;

    private bool inHand = false;

    public void Interact()
    {
        inHand = true;
        playerHand.GetComponent<Animator>().SetTrigger("Grab");
        this.gameObject.GetComponent<Rigidbody>().useGravity = false;
    }

    void Update()
    {
        if (inHand)
        {
            this.transform.position = playerHandLocation.position;
            this.transform.eulerAngles = new Vector3 (pickedUpRotation.x, playerHandLocation.eulerAngles.y, playerHandLocation.eulerAngles.z);
        }
    }
}
