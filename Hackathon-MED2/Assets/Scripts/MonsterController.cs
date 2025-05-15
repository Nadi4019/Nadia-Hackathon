using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private float monsterKillRange;

    private NavMeshAgent monsterAgent;
    public GameObject monster;
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject[] randomLocations;

    private bool chasingPlayer = false;

    [SerializeField] private int monsterWalkSpeed;
    [SerializeField] private int monsterRunSpeed;
    [SerializeField] public AudioClip monsterChaseSound;
    [SerializeField] public AudioClip[] monsterSounds;
    private float cooldownTime;

    private bool readyToInteract = false;
    private RaycastHit hit;

    private void Awake()
    {
        monsterAgent = monster.GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if (cooldownTime <= 0)
        {
            cooldownTime = Random.Range(1000, 3000);
            SoundFXManager.Instance.PlayRandomSoundClip(monsterSounds, transform, 1f);
        }
        else
        {
            cooldownTime -= Time.deltaTime;
        }
            Ray ray = new Ray(monster.transform.position, player.transform.position - monster.transform.position);
        if (Physics.Raycast(ray, out hit, 15))
        {
            Debug.DrawLine(ray.origin, hit.point);
            if (hit.collider.gameObject.name == "Player")
            {
                if((player.transform.position - monster.transform.position).magnitude < monsterKillRange)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
                }

                Debug.Log("Tarket player");
                if(!chasingPlayer)
                {
                    chasingPlayer = true;
                    SoundFXManager.Instance.PlaySoundClip(monsterChaseSound, transform, 1f);
                }
                monsterAgent.speed = monsterRunSpeed;
                monsterAgent.destination = player.transform.position;
            }
        }


        if (monsterAgent.destination == null)
        {
            Debug.Log("First Random");
            monsterAgent.speed = monsterWalkSpeed;
            monsterAgent.destination = randomLocations[UnityEngine.Random.Range(0, randomLocations.Length)].transform.position;
            return;
        } 

        if((monsterAgent.destination - monster.transform.position).magnitude < 1)
        {
            Debug.Log("New Random");
            monsterAgent.speed = monsterWalkSpeed;
            monsterAgent.destination = randomLocations[UnityEngine.Random.Range(0, randomLocations.Length)].transform.position;
            return;
        }   
    }
}
