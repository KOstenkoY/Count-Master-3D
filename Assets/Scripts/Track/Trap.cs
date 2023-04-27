using DG.Tweening;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private float _disatnceX = 2;
    [SerializeField] private float _moveDuration = 1;

    private void Start()
    {
        transform.DOLocalMoveX(transform.position.x + _disatnceX, _moveDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<TeamMember>().LeaveTeam();
    }
}
