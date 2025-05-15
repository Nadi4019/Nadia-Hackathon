using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class NumberMatch : MonoBehaviour
{
    [SerializeField] private int[] gameNumbers = new int[3];
    [SerializeField] private Sprite[] bloodNumbers;
    [SerializeField] private NumberInput[] playerNumbers = new NumberInput[3];
    [SerializeField] private SpriteRenderer[] CodeSprites;
    [SerializeField] private GameObject gate;

    [SerializeField] private AudioClip completedSound;

    private void GetNumbers()
    {
        for (int i = 0; i < gameNumbers.Length; i++)
        {
            gameNumbers[i] = UnityEngine.Random.Range(0, 10);
            CodeSprites[i].sprite = bloodNumbers[gameNumbers[i]];
        }
        Debug.Log(gameNumbers[0] + "," + gameNumbers[1] + ", " + gameNumbers[2]);
    }

    
    void Start()
    {
        GetNumbers();
    }

    private void FixedUpdate()
    {
        if (gameNumbers[0] == playerNumbers[0].Input && gameNumbers[1] == playerNumbers[1].Input && gameNumbers[2] == playerNumbers[2].Input)
        {
            if (gate.activeInHierarchy)
            {
                SoundFXManager.Instance.PlaySoundClip(completedSound, transform, 1f);
                gate.SetActive(false);
            }
        }
    }
}
