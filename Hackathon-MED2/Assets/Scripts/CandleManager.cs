using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CandleManager : MonoBehaviour
{
    [SerializeField] private float lifeSpan;
    [SerializeField] private GameObject candle;

    private Stopwatch stopwatch = new Stopwatch();

    void FixedUpdate()
    {
        candle.transform.localScale -= new Vector3(0,Time.deltaTime / ((lifeSpan * 60)/3.5f), 0);
        if(candle.transform.localScale.y <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }

        
    }
}
