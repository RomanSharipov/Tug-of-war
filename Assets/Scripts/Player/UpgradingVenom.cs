using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpgradingVenom : MonoBehaviour
{
    [SerializeField] private int _currentLevelVenom = 1;
    [SerializeField] private float _stepAddScale = 1f;
    [SerializeField] private float _speedGrowScale = 20f;
    [SerializeField] private int _requiredHealthForUpgrade = 4;
    [SerializeField] private int _levelVenomForNextLevel = 4;

    private Vector3 _targetScale = new Vector3();
    private Player _player ;

    public int RequiredHealthForUpgrade => _requiredHealthForUpgrade;

    public event UnityAction PlayerWasUpgraded;
    public event UnityAction WasGotNextLevel;

    public void Init(Player player)
    {
        _player = player;
    }

    public void UpgradeVenomLevel()
    {
        PlayerWasUpgraded?.Invoke();
        if (_currentLevelVenom == _levelVenomForNextLevel)
        {
            WasGotNextLevel?.Invoke();
        }
        _currentLevelVenom++;
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
