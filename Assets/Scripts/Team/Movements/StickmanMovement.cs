using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanMovement : Movement
{
    [SerializeField] private float tempSpeed;

    private float _speed;

    public override void Move(Vector3 direction)
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, direction, _speed * Time.fixedDeltaTime);
    }

    public override void StopMovement()
    {
        _speed = 0;
    }

    public override void StartMovement()
    {
        _speed = tempSpeed;
    }
}
