using UnityEngine;
using UnityEngine.Audio;

public class FootstepSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] leftFootprints;
    private int lastLeft = 0;
    [SerializeField] private Transform leftFoot;

    [SerializeField] private GameObject[] rightFootprints;
    private int lastRight = 0;
    [SerializeField] private Transform rightFoot;
    private bool stepSwitch = false;
    
    private Vector3 lastStep;
    [SerializeField] private float stepLength;
    [SerializeField] private float stepFadingSpeed;
    [SerializeField] private float footprintHight = 0.06f;
    [SerializeField] private AudioClip footstepSound;



    void Start()
    {
        //Hide all footprints on start
        foreach(GameObject _footprint in leftFootprints)
        {
            _footprint.SetActive(false);
        }
        foreach (GameObject _footprint in rightFootprints)
        {
            _footprint.SetActive(false);
        }
        
        lastStep = this.transform.position;
    }

    void FixedUpdate()
    {
        //Slowly fade out all footprints
        foreach (GameObject _footprint in leftFootprints)
        {
            _footprint.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, _footprint.GetComponent<SpriteRenderer>().color.a - stepFadingSpeed);
            if(_footprint.GetComponent<SpriteRenderer>().color.a <= 0) { _footprint.SetActive(false); } 
        }
        foreach (GameObject _footprint in rightFootprints)
        {
            _footprint.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, _footprint.GetComponent<SpriteRenderer>().color.a - stepFadingSpeed);
            if (_footprint.GetComponent<SpriteRenderer>().color.a <= 0) { _footprint.SetActive(false); }
        }
        
        //Place new footprint if the player has moved enough
        if ((this.transform.position - lastStep).magnitude > stepLength)
        {
            //Debug.Log((this.transform.position - lastStep).magnitude);
            if (stepSwitch)
            {
                spawnFootprint(leftFootprints[lastLeft], leftFoot);
                lastLeft++;
                if(lastLeft == 3) { lastLeft = 0; }
                stepSwitch = false;
                SoundFXManager.Instance.PlaySoundClip(footstepSound, transform, .3f);
            }
            else
            {
                spawnFootprint(rightFootprints[lastRight], rightFoot);
                lastRight++;
                if (lastRight == 3) { lastRight = 0; }
                stepSwitch = true;
                SoundFXManager.Instance.PlaySoundClip(footstepSound, transform, .3f);

            }
        }
    }

    private void spawnFootprint(GameObject _footprint, Transform _transform)
    {
        lastStep = this.transform.position;

        //Move footprint to under the player
        _footprint.transform.position = new Vector3(_transform.position.x, this.transform.position.y - 1 + footprintHight, _transform.position.z);

        //Rotate the footprint to the players rotation
        Quaternion quaternion = Quaternion.Euler(90, this.transform.localEulerAngles.y, 0);
        _footprint.transform.rotation = quaternion;

        //Make the footprint viseble
        _footprint.SetActive(true);
        _footprint.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 255);

    }
}
