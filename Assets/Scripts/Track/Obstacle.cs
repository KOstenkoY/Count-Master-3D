using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<TeamMember>().DiactivateStickman();
    }
}
