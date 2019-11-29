using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
[ExecuteInEditMode]
public class GridLevelController : MonoBehaviour
{
    private Grid _grid;
    public Grid Grid
    {
        get
        {
            if (_grid == null)
                _grid = GetComponent<Grid>();

            return _grid;
        }
    }

    private Vector3 _initScale;

    private void Awake()
    {
        RegisterToGrid();

        _initScale = transform.localScale;

        UpdateLevel();
    }

    private void OnDestroy()
    {
        UnregisterFromGrid();
    }

    private void RegisterToGrid()
    {
        Grid.OnLevelUpdated += OnLevelUpdated;
    }

    private void UnregisterFromGrid()
    {
        Grid.OnLevelUpdated -= OnLevelUpdated;
    }

    private void OnLevelUpdated()
    {
        UpdateLevel();
    }

    private void UpdateLevel()
    {
        Vector3 newScale = transform.localScale;

        newScale.y = _initScale.y * Grid.Level;

        transform.localScale = newScale;
    }
}
