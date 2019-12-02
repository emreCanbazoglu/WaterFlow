using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVisualController : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _renderer;
    [SerializeField]
    private Material _initMaterial;

    private void Awake()
    {
        InitMaterial();
    }

    private void InitMaterial()
    {
        _initMaterial = _renderer.sharedMaterial;
    }

    public void SetGridMaterial(Material mat)
    {
        _renderer.material = mat;
    }

    public void ResetGridMaterial()
    {
        _renderer.material = _initMaterial;
    }
}
