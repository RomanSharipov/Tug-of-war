using UnityEngine;

public class Venom : MonoBehaviour
{
    [SerializeField] private Transform  _lassoJointPoint;

    public Transform LassoJointPoint => _lassoJointPoint;
}
