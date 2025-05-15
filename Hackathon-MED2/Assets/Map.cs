using System.Net.NetworkInformation;
using UnityEngine;

public class Map : MonoBehaviour
{
     [SerializeField] private Vector3 cornerLocation;
    [SerializeField] private Vector3 centerLocation;
    private bool zoomed;

    void Start()
    {
        zoomed = false;
    }

    public void ChangeMap()
    {
        if (zoomed)
        {
            zoomed = false ;
            this.transform.localPosition = cornerLocation;
            this.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
        else
        {
            zoomed=true;
            this.transform.localPosition = centerLocation;
            this.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }


    }

}
