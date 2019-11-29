using UnityEngine;
using System;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Grid : MonoBehaviour
{
    public Board Board { get; private set; }

    public int X { get; private set; }
    public int Y { get; private set; }

    [SerializeField]
    private int _level;
    public int Level
    {
        get
        {
            return _level;
        }
    }

    [SerializeField]
    private GridObject _gridObject;
    public GridObject GridObject
    {
        get
        {
            return _gridObject;
        }
    }

    [SerializeField]
    [HideInInspector]
    private List<Grid> _neighborGridList;
    public List<Grid> NeighborGridList
    {
        get
        {
            return _neighborGridList;
        }
    }

    public bool IsActiveGrid { get; private set; }

    #region Events
    public Action<bool> OnGridSetActive { get; set; }

    public Action OnGridObjectSet { get; set; }

    public Action OnLevelUpdated { get; set; }
    #endregion

    public void SetGridActive(bool isActive)
    {
        gameObject.SetActive(isActive);

        IsActiveGrid = isActive;

        OnGridSetActive?.Invoke(IsActiveGrid);
    }

    public void InitGrid(Board b, int x, int y)
    {
        Board = b;

        X = x;
        Y = y;

        _level = 1;
    }

    public void PostInitGrid()
    {
        InitNeighborGrids();
    }

    private void InitNeighborGrids()
    {
        _neighborGridList = Board.GetNeighborGrids(this);
    }

    public void IncreaseLevel(int level)
    {
        _level += level;

        OnLevelUpdated?.Invoke();
    }

    public void SetGridObject(GridObject.EType type)
    {
        DestroyCurGridObject();

        GridObject gridObj = GridObjectFactory.Instance
            .CreateGridObjectInstance(type);

        _gridObject = gridObj;

        gridObj.SetParentGrid(this);

        OnGridObjectSet?.Invoke();
    }

    private void DestroyCurGridObject()
    {
        GridObject.DestroyGridObject();

        _gridObject = null;
    }
}