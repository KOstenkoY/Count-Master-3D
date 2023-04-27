using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToSwipeDirection : MoveToDirection
{
    [SerializeField] private float _swiperStopTolerance;
    [SerializeField] private float _endPointSpeed;

    private Vector3 _startPoint;
    private Vector3 _endPoint;

    private float _inputSpeed;

    private Camera _camera;

    void Start()
    {
        _endPoint = Vector3.zero;
        _camera = Camera.main;
    }

    public override Vector3 CalculateDirection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startPoint = _camera.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _camera.nearClipPlane));

            _endPoint = _startPoint;
        }
        if (Input.GetMouseButton(0))
        {
            _startPoint = _camera.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _camera.nearClipPlane));

            _inputSpeed = GetSwipeSpeed(_startPoint.x, _endPoint.x);
            _endPoint = Vector3.Lerp(_endPoint, _startPoint, _endPointSpeed * Time.deltaTime);

            return new Vector3(_inputSpeed, 0, 0);
        }

        return Vector3.zero;
    }

    float GetSwipeSpeed(float startPoint, float endPoint)
    {
        float inputSpeed = startPoint - endPoint;

        if (Mathf.Abs(inputSpeed) > _swiperStopTolerance)
        {
            return inputSpeed;
        }
        else
        {
            return 0;
        }
    }
}
