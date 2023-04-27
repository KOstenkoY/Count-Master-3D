using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TeamLeader : MonoBehaviour
{
    public GameManager manager;

    [SerializeField] private TMP_Text _stickmanCountText;
    [SerializeField] private int _defaultStickmanCount = 1;

    [HideInInspector] public int stickmanCount;

    private int _finishedStickmanCount;

    private void Start()
    {
        CreateDefaultStickman();
    }

    void CreateDefaultStickman()
    {
        for (int i = 0; i < _defaultStickmanCount; i++)
        {
            GameObject stickman = ObjectPooler.SharedInstance.GetPooledObject(Constants.STICKMAN_TAG);
            stickman.transform.position = transform.position;
            stickman.transform.SetParent(transform);
            stickman.GetComponent<TeamMember>().JoinTeam();
            stickman.SetActive(true);
        }
    }

    public void IncreaseFinishedStickmanCount()
    {
        _finishedStickmanCount++;

        if (_finishedStickmanCount == stickmanCount)
        {
            GetComponent<BuildTower>().enabled = false;
            manager.Win();
        }
    }

    public void IncreaseHumanCount()
    {
        stickmanCount++;
        UpdateText();
    }

    public void DecreaseHumanCount()
    {
        stickmanCount--;

        UpdateText();

        if (stickmanCount == 0)
        {
            manager.Lose();
        }
    }

    void UpdateText()
    {
        _stickmanCountText.text = stickmanCount.ToString();
    }

    public void TextActiveFalse()
    {
        _stickmanCountText.gameObject.SetActive(false);
    }
}
