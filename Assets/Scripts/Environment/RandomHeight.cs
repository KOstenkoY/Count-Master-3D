using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHeight : MonoBehaviour
{
    [SerializeField] private float _maxRange = -40f;
    [SerializeField] private float  _minRange = -60f;

    void Start()
    {
        foreach (Transform child in transform)
        {
            child.transform.position = new Vector3(child.position.x, Random.Range(_minRange, _maxRange), child.position.z);
        }
    }
}
