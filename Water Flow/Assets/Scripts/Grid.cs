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


    public GridObject GridObject { get; private set; }
    public List<Grid> NeighborGridList { get; private set; }

    private Vector3 _initScale;

    #region Events
    public Action OnGridObjectSet { get; set; }
    public Action OnGridObjectRemoved { get; set; }

    public Action OnLevelUpdated { get; set; }
    #endregion

    public void InitGrid(Board b, int x, int y)
    {
        Board = b;

        X = x;
        Y = y;

        _level = 1;

        _initScale = transform.localScale;
    }

    public void PostInitGrid()
    {
        InitNeighborGrids();
    }

    private void InitNeighborGrids()
    {
        NeighborGridList = Board.GetNeighborGrids(this);
    }

    public void IncreaseLevel(int level)
    {
        _level += level;

        Vector3 newScale = transform.localScale;

        newScale.y = _initScale.y * Level;

        transform.localScale = newScale;
    }

    public void RemoveGridObject()
    {
        Action onDestroyedDel = delegate
        {
            OnGridObjectRemoved?.Invoke();
        };

       DestroyCurGridObject(onDestroyedDel);
    }

    public void SetGridObject(GridObject.EType type)
    {
        Action onDestroyedDel = delegate
        {
            GridObject gridObj = GridObjectFactory.Instance
            .CreateGridObjectInstance(type);

            GridObject = gridObj;

            gridObj.SetParentGrid(this);

            OnGridObjectSet?.Invoke();
        };

        DestroyCurGridObject(onDestroyedDel);
    }

    private void DestroyCurGridObject(Action onDestroyedCallback)
    {
        GridObject.DestroyGridObject(onDestroyedCallback);

        GridObject = null;
    }
}