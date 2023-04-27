using UnityEngine;

public class StickmanCreatorController : MonoBehaviour
{
    [SerializeField] private Transform _leftGate;
    [SerializeField] private Transform _rightGate;

    private bool _isTaken;

    public void Take()
    {
        _isTaken = true;

        _leftGate.GetComponent<BoxCollider>().enabled = false;
        _rightGate.GetComponent<BoxCollider>().enabled = false;

        Destroy(gameObject);
    }

    public bool GetIsTaken()
    {
        return _isTaken;
    }
}
