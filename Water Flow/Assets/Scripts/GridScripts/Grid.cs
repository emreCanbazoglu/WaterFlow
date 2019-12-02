using UnityEngine;
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

[ExecuteInEditMode]
public class Grid : SerializedMonoBehaviour
{
    public Board Board { get; private set; }

    public int X { get; private set; }
    public int Y { get; private set; }

    [SerializeField]
    [HideInInspector]
    private GridLevelController _levelController;
    public GridLevelController LevelController
    {
        get
        {
            if(_levelController == null)
                _levelController = GetComponent<GridLevelController>();

            return _levelController;
        }
    }

    [SerializeField]
    [HideInInspector]
    private GridVisualController _visualController;
    public GridVisualController VisualController
    {
        get
        {
            if (_visualController == null)
                _visualController = GetComponent<GridVisualController>();

            return _visualController;
        }
    }

    [SerializeField]
    [OnValueChanged("SetGridObject")]
    private GridObject.EType _gridObjectType;
    public GridObject.EType GridObjectType
    {
        get
        {
            return _gridObjectType;
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
    public Action<Grid, bool> OnGridSetActive { get; set; }

    public Action<Grid> OnGridObjectSet { get; set; }
    #endregion

    private void Awake()
    {
        SetGridObject(GridObjectType);
    }

    public void SetGridActive(bool isActive)
    {
        gameObject.SetActive(isActive);

        IsActiveGrid = isActive;

        OnGridSetActive?.Invoke(this, IsActiveGrid);
    }

    public void InitGrid(Board b, int x, int y)
    {
        Board = b;

        X = x;
        Y = y;
    }

    public void PostInitGrid()
    {
        InitNeighborGrids();
    }

    private void InitNeighborGrids()
    {
        _neighborGridList = Board.GetNeighborGrids(this);
    }


    public void SetGridObject(GridObject.EType type)
    {
        DestroyCurGridObject();

        GridObject gridObj = GridObjectFactory.Instance
            .CreateGridObjectInstance(type);

        if(gridObj != null)
        {
            _gridObject = gridObj;

            gridObj.SetParentGrid(this);
        }

        OnGridObjectSet?.Invoke(this);
    }

    private void DestroyCurGridObject()
    {
        if (GridObject == null)
            return;

        GridObject.DestroyGridObject();

        _gridObject = null;
    }
}