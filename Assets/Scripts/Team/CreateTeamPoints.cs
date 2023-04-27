using System.Collections.Generic;
using UnityEngine;

public class CreateTeamPoints : MonoBehaviour
{
    [SerializeField] private float _layerCount;
    [SerializeField] private float _startAngle;
    [SerializeField] private float _divideLayerSize;

    [HideInInspector] public List<TeammatePoint> _teammatePoints;

    private void Awake()
    {
        CreateWithSinAndCos();
    }

    private void CreateWithSinAndCos()
    {
        AddList(0, 0, 0);

        for (int layer = 1; layer < _layerCount; layer++)
        {
            float angle = _startAngle / layer;

            for (float totalAngle = 0; totalAngle < 360; totalAngle += angle)
            {
                float sinX = Mathf.Sin(totalAngle * Mathf.PI / 180);
                float cosX = Mathf.Cos(totalAngle * Mathf.PI / 180);

                AddList(sinX, cosX, layer);
            }
        }
    }

    private void AddList(float sinX, float cosX, int layer)
    {
        TeammatePoint newTeammatePoint = new TeammatePoint();
        newTeammatePoint.Point = new Vector3(sinX, 0, cosX) * layer / _divideLayerSize;
        _teammatePoints.Add(newTeammatePoint);

        //GameObject go;
        //go = Instantiate(player, transform.position + newTeammatePoint.Point, Quaternion.identity);
        //go.transform.parent = transform;
    }
}
