using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TabletPuzzle : MonoBehaviour, Interactable
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform cameraLokation;
    [SerializeField] private Transform playerLokation;

    [SerializeField] private BasePlayer PCPlayerMovement;
    [SerializeField] private BasePlayer MobilePlayerMovement;

    [SerializeField] private List<PuzzleTile> puzzleTiles = new List<PuzzleTile>();
    [SerializeField] private Transform[] locations;
    private List<List<PuzzleTile>> matrixPuzzleTiles = new List<List<PuzzleTile>>();
    private List<List<Transform>> matrixLocations = new List<List<Transform>>();

    [SerializeField] private GameObject canvas;

    [SerializeField] private float tileSpeed;

    [SerializeField] private GameObject gate;
    [SerializeField] private AudioClip rotateSound;
    [SerializeField] private AudioClip completedSound;


    private int emptyX;
    private int emptyY;

    void Start()
    {
        canvas.SetActive(false);

        for (int i = 0; i < puzzleTiles.Count; i++)
        {
            puzzleTiles[i].MoveInstant(locations[i]);
        }

        int n = 0;
        for(int i = 0; i < 3; i++)
        {
            matrixPuzzleTiles.Add(new List<PuzzleTile>());
            matrixLocations.Add(new List<Transform>());
            for (int j = 0; j < 3; j++)
            {
                if (puzzleTiles[n].tileNumber == 8)
                {
                    emptyX = i;
                    emptyY = j;
                }
                matrixPuzzleTiles[i].Add(puzzleTiles[n]);
                matrixLocations[i].Add(locations[n]);
                n++;
            }
        }

        shuffel();
    }

    public void Interact()
    {
        canvas.SetActive(true);

        cameraTransform.position = cameraLokation.position;
        cameraTransform.eulerAngles = cameraLokation.eulerAngles;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PCPlayerMovement.freeLook = false;
        MobilePlayerMovement.freeLook = false;
    }

    public void resetCamera()
    {
        canvas.SetActive(false);
        cameraTransform.localPosition = new Vector3(-18.5f,12f,.7f);
        cameraTransform.localEulerAngles = Vector3.zero;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PCPlayerMovement.freeLook = true;
        MobilePlayerMovement.freeLook = true;
    }


    public void moveTile(string loc)
    {
        string temp = loc.Substring(0, 1);
        int x = int.Parse(temp);
        temp = loc.Substring(1, 1);
        int y = int.Parse(temp);

        int _x = x - emptyX;
        int _y = y - emptyY;
        

        if ((_x <= 1 && _x >= -1 && _y == 0) || (_y <= 1 && _y >= -1 && _x == 0))
        {
            SoundFXManager.Instance.PlaySoundClip(rotateSound, transform, .1f);


            matrixPuzzleTiles[x][y].Move(matrixLocations[emptyX][emptyY], tileSpeed);
            matrixPuzzleTiles[emptyX][emptyY].Move(matrixLocations[x][y], tileSpeed);

            PuzzleTile tempTile0 = matrixPuzzleTiles[x][y];
            PuzzleTile tempTile1 = matrixPuzzleTiles[emptyX][emptyY];
            matrixPuzzleTiles[emptyX][emptyY] = tempTile0;
            matrixPuzzleTiles[x][y] = tempTile1;

            emptyX = x; emptyY = y;

            int n = 0;
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    closeGate();
                    if (matrixPuzzleTiles[i][j].tileNumber != n) return;
                    n++;
                }
            }

            openGate();
        }
    }

    private void shuffel()
    {
        for (int timesShuffeld = 0; timesShuffeld < 100; timesShuffeld++)
        {

            int x = UnityEngine.Random.Range(0,3);
            int y = UnityEngine.Random.Range(0,3);

            int _x = x - emptyX;
            int _y = y - emptyY;


            if ((_x <= 1 && _x >= -1 && _y == 0) || (_y <= 1 && _y >= -1 && _x == 0))
            {
                matrixPuzzleTiles[x][y].MoveInstant(matrixLocations[emptyX][emptyY]);
                matrixPuzzleTiles[emptyX][emptyY].MoveInstant(matrixLocations[x][y]);

                PuzzleTile tempTile0 = matrixPuzzleTiles[x][y];
                PuzzleTile tempTile1 = matrixPuzzleTiles[emptyX][emptyY];
                matrixPuzzleTiles[emptyX][emptyY] = tempTile0;
                matrixPuzzleTiles[x][y] = tempTile1;

                emptyX = x; emptyY = y;
            }
        }
    }

    private void openGate()
    {
        SoundFXManager.Instance.PlaySoundClip(completedSound, transform, 1f);
        gate.SetActive(false);
    }
    private void closeGate()
    {
        gate.SetActive(true);
    }
}
