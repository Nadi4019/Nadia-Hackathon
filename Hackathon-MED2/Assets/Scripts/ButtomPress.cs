using UnityEngine;

public class ButtomPress : MonoBehaviour

{
    public AudioClip buttonPressAudio;

    public void OnButtomPress()
    {

        SoundFXManager.Instance.PlaySoundClip(buttonPressAudio, transform, 1f);
    }
}
