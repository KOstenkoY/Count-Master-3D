using TMPro;
using UnityEngine;

public class StickmanCreator : MonoBehaviour
{
    [SerializeField] private Operation _operation;
    [SerializeField] private float _value;
    [SerializeField] private TMP_Text _text;

    private StickmanCreatorController _stickmanCreatorController;

    private float _count;

    enum Operation { Sum, Multiplication };

    private void Start()
    {
        _stickmanCreatorController = transform.parent.GetComponent<StickmanCreatorController>();

        if (_operation == Operation.Multiplication)
        {
            _text.text = "X";
        }

        _text.text += _value.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        float randomPositionRange = 0.5f;

        if (!_stickmanCreatorController.GetIsTaken())
        {
            Transform parent = other.transform.parent;

            if (_operation == Operation.Sum)
            {
                _count = _value;
            }
            else if (_operation == Operation.Multiplication)
            {
                _count = parent.GetComponent<TeamLeader>().stickmanCount * (_value - 1);
            }
            for (int i = 0; i < _count; i++)
            {
                Vector3 position = new Vector3(
                    Random.Range(parent.position.x - randomPositionRange, parent.position.x + randomPositionRange),
                    parent.position.y,
                    Random.Range(parent.position.z - randomPositionRange, parent.position.z + randomPositionRange));

                GameObject stickman = ObjectPooler.SharedInstance.GetPooledObject(Constants.STICKMAN_TAG);
                stickman.transform.position = position;
                stickman.transform.SetParent(parent);
                stickman.GetComponent<TeamMember>().JoinTeam();
                stickman.SetActive(true);
            }

            _stickmanCreatorController.Take();
        }
    }
}
