using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _lookSpeed;
    [HideInInspector] public Vector3 lookAtDeflection;
    
    private Animation _animation;
    private Transform _camera;

    private Vector3 _offset;
    private bool _look;

    private void Start()
    {
        _camera = transform.GetChild(0);

        _animation = GetComponent<Animation>();

        _offset = transform.position - _target.position;

        lookAtDeflection = Vector3.up;
    }
    void Update()
    {
        transform.position = _target.position + _offset;

        if (_look)
        {
            _camera.rotation = Quaternion.RotateTowards(_camera.rotation,
                Quaternion.LookRotation(_target.position - _camera.position + lookAtDeflection),
                Time.deltaTime * _lookSpeed);
        }
    }
    public void EndGameAnim(Transform target)
    {
        _animation.Play(Constants.END_GAME_CAMERA_ANIM);

        this._target = target;
        _offset = transform.position - target.position;

        _look = true;

        lookAtDeflection = Vector3.zero;
    }
}
