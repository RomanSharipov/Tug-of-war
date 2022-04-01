using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentBuilding : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private MeshCollider _meshCollider;

    public void Init()
    {
        _rigidbody = gameObject.AddComponent<Rigidbody>();
        _meshCollider = gameObject.AddComponent<MeshCollider>();
        _rigidbody.isKinematic = true;
        _meshCollider.convex = true;
        _meshCollider.enabled = false;
        gameObject.SetActive(false);
    }

    public void SwithOnRigidbody()
    {
        gameObject.SetActive(true);
        _rigidbody.isKinematic = false;
        _meshCollider.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject,3f);
    }
}
