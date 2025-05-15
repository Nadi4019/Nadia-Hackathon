using UnityEngine;

public class PuzzleTile : MonoBehaviour
{
    public Transform currentLocation;
    public int tileNumber;

    private Vector3 targetLocation;
    private float _speed;
    private bool moving = false;

    public void MoveInstant(Transform _transform)
    {
        currentLocation.position = _transform.position;
    }
    public void Move(Transform _transform, float speed)
    {
        moving = true;
        _speed = speed;
        targetLocation = _transform.position;
    }

    private void FixedUpdate()
    {
        if (!moving) return;
        if ((targetLocation - currentLocation.position).magnitude < 0.01f)
        { 
            currentLocation.position = targetLocation;
            moving = false;
        }
        currentLocation.position += (targetLocation - currentLocation.position) / _speed;
    }
}
