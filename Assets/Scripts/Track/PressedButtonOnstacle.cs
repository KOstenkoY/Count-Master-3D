using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CapsuleCollider))]
public class PressedButtonOnstacle : MonoBehaviour
{
    [SerializeField] private Transform _wall;

    [SerializeField] private float _targetPositionY = -2;
    private float _duration = 0.5f;

    private CapsuleCollider _capsuleCollider;

    private void Awake()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.STICKMAN_TAG))
        {
            _wall.DOMoveY(_targetPositionY, _duration);
            _capsuleCollider.enabled = false;
        }
    }
}
