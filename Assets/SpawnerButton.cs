using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerButton : MonoBehaviour
{
    [SerializeField] private Button _buttonCrashBuildingTemplate;
    [SerializeField] private float _lifeTimeButton;
    [SerializeField] private Building _building;
    [SerializeField] private EnemyContainer _enemyContainer;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Canvas _canvas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyContainer enemyContainer))
        {
            StartCoroutine(SpawnButtonForSeconds(_lifeTimeButton));
        }
    }

    private IEnumerator SpawnButtonForSeconds(float seconds)
    {
        var newButton = Instantiate(_buttonCrashBuildingTemplate,_canvas.transform);
        newButton.transform.position = _spawnPoint.position;
        newButton.onClick.AddListener(delegate { _building.CrushBuilding(); });
        newButton.onClick.AddListener(delegate { _enemyContainer.StopMoveForSeconds(0.5f); });
        
        yield return new WaitForSeconds(seconds);
        Destroy(newButton.gameObject);
    }
}
