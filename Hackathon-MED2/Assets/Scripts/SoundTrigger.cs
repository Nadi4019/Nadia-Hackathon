using Unity.VisualScripting;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip[] MonsterSoundClips;
    public float cooldownTime = 20f;
    private float lastUsedTime;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Check if the cooldown period has elapsed
            if (Time.time >= lastUsedTime + cooldownTime)
            {
                // Play the sound and update the last used time
                SoundFXManager.Instance.PlayRandomSoundClip(MonsterSoundClips, transform, 1f);
                lastUsedTime = Time.time;
            }
        }
    }
}
