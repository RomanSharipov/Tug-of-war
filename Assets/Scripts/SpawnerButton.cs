using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerButton : MonoBehaviour
{
    [SerializeField] private Button _buttonCrashBuildingTemplate;
    [SerializeField] private float _lifeTimeButton;
    
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Canvas _canvas;

    private bool _isButtonPressed = false;

    public bool IsButtonPressed => _isButtonPressed;

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
        newButton.onClick.AddListener(delegate { _isButtonPressed = true; });

        
        yield return new WaitForSeconds(seconds);
        newButton.onClick.RemoveListener(delegate { _isButtonPressed = true; });
        Destroy(newButton.gameObject);
    }
}
