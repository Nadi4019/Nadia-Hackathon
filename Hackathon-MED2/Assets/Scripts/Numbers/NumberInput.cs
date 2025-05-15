using System;
using UnityEngine;
[Serializable]
public class NumberInput : MonoBehaviour, Interactable
{
    public int Input = 0;

    private float addedDelta;
    [SerializeField] private float neededDelta = 0.5f;
    [SerializeField] private AudioClip turnSound;


    private float rotationAmount = 0;
    private Vector3 _rotation = new Vector3(0, 0, -1);
    [SerializeField] private float _speed = 1;
    public void Interact()
    {
        Debug.Log(addedDelta);
        if (addedDelta < neededDelta)
        {
            return;
        }

        Input++;
        rotationAmount = 36f;
        addedDelta = 0;

        SoundFXManager.Instance.PlaySoundClip(turnSound, transform, .1f);

        if (Input > 9)
        {
            Input = 0;
        }
        

        //transform.Rotate(0, 0, -36);
    }

    private void FixedUpdate()
    {
        addedDelta += Time.deltaTime;

         if(rotationAmount <= 0) return; 
       if(rotationAmount <= _speed)
       {
            transform.Rotate(_rotation*rotationAmount);
            rotationAmount = 0;
            return;
       } 

       transform.Rotate(_rotation * _speed);
       rotationAmount -= _speed;
    }
}
