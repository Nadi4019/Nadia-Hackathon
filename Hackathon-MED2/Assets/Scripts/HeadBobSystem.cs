using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeadBobSystem : MonoBehaviour
{
    [Range(0.001f, 0.01f)]
    public float Amount = 0.002f;

    [Range(0.001f, 0.01f)]
    public float Frequency = 10.0f;

    [Range(0.001f, 0.01f)]
    public float Smooth = 10.0f;

    Vector3 StartPos;

    void start()
    {
        StartPos = transform.localPosition;
    }

    void update ()
    {
        CheckForHeadBobTrigger();
        StopHeadBob();
    }

    private void CheckForHeadBobTrigger()
    {
        float inputMagnitude = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;

        if(inputMagnitude > 0)
        {
            StartHeadBob();
        }
    }

    private Vector3 StartHeadBob()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * Frequency) * Amount * 1.4f, Smooth * Time.deltaTime);
        pos.x += Mathf.Lerp(pos.x, Mathf.Cos(Time.time * Frequency / 2f) * Amount * 1.6f, Smooth * Time.deltaTime);
        transform.localPosition += pos;

        return pos;
    }

    private void StopHeadBob()
    {
        if (transform.localPosition == StartPos) return;
        transform.localPosition = Vector3.Lerp(transform.localPosition, StartPos, Time.deltaTime);
    }

}
