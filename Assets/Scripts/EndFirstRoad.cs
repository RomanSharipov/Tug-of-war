using UnityEngine;
using UnityEngine.Events;

public class EndFirstRoad : MonoBehaviour
{
    public event UnityAction PlayerFinishedFirstRoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            PlayerFinishedFirstRoad?.Invoke();
        }
    }
}
