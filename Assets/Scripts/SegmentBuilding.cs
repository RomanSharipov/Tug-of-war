using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class SegmentBuilding : MonoBehaviour
{
    [SerializeField] private float _speedSettingTransparent;
    
    [SerializeField] private float _delayBeforeSetTransparentMaterial = 1f ;
    


    
    private Rigidbody _rigidbody;
    private MeshCollider _meshCollider;
    private MeshRenderer _meshRenderer;
    private float _valueTransparentColor;
    

    public void Init()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _meshCollider = gameObject.GetComponent<MeshCollider>();
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
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
        Invoke(nameof(SetTransparentMaterial), _delayBeforeSetTransparentMaterial);
    }

    private IEnumerator SetTransparentSmooth()
    {
        while (_meshRenderer.material.color.a != 0 )
        {
            _valueTransparentColor = Mathf.MoveTowards(_meshRenderer.material.color.a,0, _speedSettingTransparent * Time.deltaTime);
            _meshRenderer.material.color = new Color(_meshRenderer.material.color.r, _meshRenderer.material.color.g, _meshRenderer.material.color.b, _valueTransparentColor);
            yield return null;
        }
    }

    public void SetTransparentMaterial()
    {
        StartCoroutine(SetTransparentSmooth());
    }
}
