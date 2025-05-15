using UnityEngine;

public class PlatformInitializer : MonoBehaviour
{
    public MobilePlayer mobilePlayerScript;
    public PCPlayer pcPlayerScript;
    public GameObject joystickMove;
    public GameObject joystickLook;

    void Awake()
    {
#if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
        pcPlayerScript.enabled = true;
        mobilePlayerScript.enabled = false;
        joystickMove.SetActive(false);
        joystickLook.SetActive(false);
#else
        pcPlayerScript.enabled = false;
        mobilePlayerScript.enabled = true;
#endif
    }
}