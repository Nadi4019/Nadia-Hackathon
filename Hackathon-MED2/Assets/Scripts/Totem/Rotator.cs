using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour, Interactable
{
    public int rotation = 0;

    private float timer = 0;
   
    private float rotationAmount = 0;

    private Vector3 _rotation = new Vector3(0, 1, 0);
    [SerializeField] private float _speed;
    [SerializeField] private AudioClip rotateSound;



    public void Interact()
    {
        //if (Input.GetKey(KeyCode.E)) _rotation = Vector3.up;
        //else if (Input.GetKey(KeyCode.D)) _rotation = Vector3.down;
        //else _rotation = Vector3.zero;
        if (timer <=0)
        {
            rotationAmount = 90f;
            timer = 100f;
            rotation++;
            if (rotation >= 4) rotation = 0;
            SoundFXManager.Instance.PlaySoundClip(rotateSound, transform, .1f);

        }

    }

    public void RotateAmount(float amount)
    {
        rotation = (int)amount;
        rotationAmount = amount * 90f;
    }

   private void FixedUpdate()
   {
       timer --; 

       if(rotationAmount <= 0) return; 
       if(rotationAmount <= _speed)
       {
            transform.Rotate(new Vector3(transform.eulerAngles.x, rotationAmount, transform.eulerAngles.z));
            rotationAmount = 0;
            return;
       } 

       transform.Rotate(_rotation * _speed);
       rotationAmount -= _speed;
    
   }
}
