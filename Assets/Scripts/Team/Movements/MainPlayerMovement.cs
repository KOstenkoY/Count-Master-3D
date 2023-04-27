using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerMovement : Movement
{
    [SerializeField] private float _tempHorizontalSpeed;
    [SerializeField] private float _tempVerticleSpeed;

    private float _horizontalSpeed;
    private float _verticleSpeed;

    private Rigidbody _rigidBody;
    
    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    public override void Move(Vector3 direction)
    {
        _rigidBody.velocity = new Vector3(direction.x * _horizontalSpeed, 0, _verticleSpeed);
    }

    public override void StopMovement()
    {
        _horizontalSpeed = 0;

        _verticleSpeed = 0;

        _rigidBody.velocity = Vector3.zero;
    }

    public override void StartMovement()
    {
        _horizontalSpeed = _tempHorizontalSpeed;

        _verticleSpeed = _tempVerticleSpeed;
    }
}
