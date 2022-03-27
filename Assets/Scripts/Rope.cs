using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    private CableProceduralCurve _cableProceduralCurve;

    private void Start()
    {
        _cableProceduralCurve = GetComponent<CableProceduralCurve>();
        _cableProceduralCurve.SetEndPoint(_enemy.Player.LassoJointPoint);
    }
}
