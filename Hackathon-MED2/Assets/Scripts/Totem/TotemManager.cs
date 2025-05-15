using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotemManager : MonoBehaviour
{
    private List<List<Rotator>> totems = new List<List<Rotator>>();
    [SerializeField] private int[] code = new int[3];
    [SerializeField] private Rotator[] _totems;
    [SerializeField] private Sprite[] symbols;
    [SerializeField] private SpriteRenderer[] gateImages;

    [SerializeField] private GameObject gate;

    [SerializeField] private AudioClip completedSound;
    void Awake()
    {
        for (int i = 0; i < 4; i++)
        {
            totems.Add(new List<Rotator>());
            for (int j = 0; j < 3; j++)
            {
                totems[i].Add(_totems[j + (i * 3)]);
            }
        }
        for (int i = 0; i < 3; i++)
        {
            code[i] = UnityEngine.Random.Range(0, 4);
            gateImages[i].sprite = symbols[code[i]];
        }

        foreach (List<Rotator> totem in totems)
        {
            foreach (Rotator box in totem)
            {
                box.RotateAmount(UnityEngine.Random.Range(0, 4));
            }
        }
    }

    void FixedUpdate()
    {
        foreach (List<Rotator> totem in totems)
        {
            for (int i = 0; i < 3; i++)
            {
                CloseGate();
                if (totem[i].rotation != code[i]) return;
            }
        }

        OpenGate();
    }

    private void OpenGate()
    {
        SoundFXManager.Instance.PlaySoundClip(completedSound, transform, 1f);
        gate.SetActive(false);
    }

    private void CloseGate()
    {
        gate.SetActive(true);
    }
}
