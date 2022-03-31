using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerObjects : MonoBehaviour
{
    private Transform[] _points;

    [SerializeField] private List<GameObject> _allObjects = new List<GameObject>();
    [SerializeField] private GameObject _template;

    public void Init()
    {
        _points = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            _points[i] = transform.GetChild(i);
        }
    }

    public void Spawn()
    {
        foreach (var point in _points)
        {
            GameObject gameObject = Instantiate(_template, point.position, point.rotation, transform);
            _allObjects.Add(gameObject);
        }
    }

    public void DestroyAllObjects()
    {
        foreach (var Object in _allObjects)
        {
            Destroy(Object.gameObject);

        }
        _allObjects = new List<GameObject>();
    }
}
