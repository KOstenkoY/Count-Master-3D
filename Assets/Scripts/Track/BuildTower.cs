using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTower : MonoBehaviour
{
    [SerializeField] private int _perRowMaxStickmanCount;
    [SerializeField] private float _distanceBetweenStickmans;

    private List<int> _towerCountList;
    private List<GameObject> _towerList;

    private CameraMovement _cameraAnimations;

    private bool _move;

    private void Start()
    {
        _towerCountList = new List<int>();
        _towerList = new List<GameObject>();
        _cameraAnimations = Camera.main.transform.parent.GetComponent<CameraMovement>();
    }

    void FixedUpdate()
    {
        if (_move)
        {
            transform.GetComponent<Movement>().Move(Vector3.forward);
        }
    }

    public  void Build()
    {
        GetComponent<TeamLeader>().TextActiveFalse();
        GetComponent<MoveToDirection>().StopMoveToDirection();
        GetComponent<Movement>().StopMovement();

        FillTowerList();

        StartCoroutine(BuildTowerCoroutine());

        _cameraAnimations.EndGameAnim(_towerList[0].transform);
    }

    void FillTowerList()
    {
        int humanCount = GetComponent<TeamLeader>().stickmanCount;

        for (int i = 1; i <= _perRowMaxStickmanCount; i++)
        {
            if (humanCount < i)
            {
                break;
            }

            humanCount -= i;

            _towerCountList.Add(i);
        }

        for (int i = _perRowMaxStickmanCount; i > 0; i--)
        {
            if (humanCount >= i)
            {
                humanCount -= i;

                _towerCountList.Add(i);

                i++;
            }
        }

        _towerCountList.Sort();
    }

    IEnumerator BuildTowerCoroutine()
    {
        int towerId = 0;
        Vector3 sum;
        GameObject tower;
        float tempTowerHumanCount;

        transform.position = new Vector3(0, transform.position.y, transform.position.z);

        foreach (int towerHumanCount in _towerCountList)
        {
            foreach (GameObject child in _towerList)
            {
                child.transform.localPosition += Vector3.up;
            }

            tower = new GameObject("Tower" + towerId);
            tower.transform.parent = transform;
            tower.transform.localPosition = new Vector3(0, 0, 0);
            _towerList.Add(tower);

            sum = Vector3.zero;

            tempTowerHumanCount = 0;

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);

                if (child.CompareTag(Constants.STICKMAN_TAG))
                {
                    child.GetComponent<Collider>().isTrigger = true;
                    child.GetComponent<MoveToDirection>().StopMoveToDirection();
                    child.GetComponent<Movement>().StopMovement();

                    child.transform.parent = tower.transform;
                    child.transform.localPosition = new Vector3(tempTowerHumanCount * _distanceBetweenStickmans, 0, 0);
                    sum += child.transform.position;

                    tempTowerHumanCount++;

                    i--;

                    if (tempTowerHumanCount >= towerHumanCount)
                    {
                        break;
                    }
                }
            }

            tower.transform.position = new Vector3(-sum.x / towerHumanCount, tower.transform.position.y, tower.transform.position.z);
            sum = Vector3.zero;
            towerId++;

            yield return new WaitForSeconds(0.1f);
        }

        GetComponent<Movement>().StartMovement();

        _move = true;
    }
}
