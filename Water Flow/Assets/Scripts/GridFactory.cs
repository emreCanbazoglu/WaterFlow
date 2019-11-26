﻿using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridFactory : MonoBehaviour
{
    private static GridFactory _instance;
    public static GridFactory Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<GridFactory>();
             
            return _instance;
        }
    }

    [SerializeField]
    private Grid _gridPrefab;

    [SerializeField]
    private float _edgeSize;

    [SerializeField]
    private float _heightPerLevel;

    private readonly float _sqrt3 = Mathf.Sqrt(3);

    public Grid CreateGridInstance(Vector3 origin, int row, int coloumn)
    {
        Grid g = Instantiate(_gridPrefab);

        InitGrid(g, origin, row, coloumn);

        return g;
    }

    private void InitGrid(
        Grid g,
        Vector3 origin,
        int row,
        int coloumn)
    {
        InitGridCoordinates(g, origin, row, coloumn);
        InitGridScale(g);
    }

    private void InitGridCoordinates(
        Grid g,
        Vector3 origin,
        int row,
        int coloumn)
    {
        Vector3 pos = origin;

        pos.x += coloumn * _sqrt3 * _edgeSize;
        pos.z += row * 2 * _edgeSize;

        g.transform.position = pos;

    }

    private void InitGridScale(Grid g)
    {
        Vector3 scale = transform.localScale * _edgeSize;

        g.transform.localScale = scale;
    }
}