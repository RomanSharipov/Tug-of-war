using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpgradingVenom : MonoBehaviour
{
    [SerializeField] private int _levelVenom = 1;
    [SerializeField] private float _stepAddScale;
    [SerializeField] private float _speedGrowScale;
    [SerializeField] private int _requiredHealthForUpgrade;

    private Vector3 _targetScale = new Vector3();

    public int RequiredHealthForUpgrade => _requiredHealthForUpgrade;

    public event UnityAction PlayerWasUpgraded;

    public void UpgradeSlimeLevel()
    {
        PlayerWasUpgraded?.Invoke();
        _levelVenom++;
        _targetScale = new Vector3(transform.localScale.x + _stepAddScale, transform.localScale.y + _stepAddScale, transform.localScale.z + _stepAddScale);
        StartCoroutine(UpgradeScale(_targetScale));
    }

    private IEnumerator UpgradeScale(Vector3 targetScale)
    {
        while (transform.localScale.y < targetScale.y)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, _speedGrowScale * Time.deltaTime);
            yield return null;
        }
    }
}
