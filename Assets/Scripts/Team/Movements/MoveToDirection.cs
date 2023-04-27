using UnityEngine;

public abstract class MoveToDirection : MonoBehaviour
{
    private Vector3 _direction;
    private Movement _movement;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
    }

    private void Update()
    {
        _direction = CalculateDirection();
    }

    void FixedUpdate()
    {
        _movement.Move(_direction);
    }

    public void StartMoveToDirection()
    {
        enabled = true;
    }

    public void StopMoveToDirection()
    {
        enabled = false;
    }
    public abstract Vector3 CalculateDirection();
}