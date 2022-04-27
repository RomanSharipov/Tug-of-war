using UnityEngine;
using UnityEngine.Events;

public class Turn : MonoBehaviour
{
    public UnityEvent Turned;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Turned?.Invoke();
        }
    }
}