using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridObject))]
public class WaterObject : MonoBehaviour
{
    private GridObject _gridObject;
    public GridObject GridObject
    {
        get
        {
            if (_gridObject == null)
                _gridObject = GetComponent<GridObject>();

            return _gridObject;
        }
    }

    private void Awake()
    {
    }

    private void TryRegisterToGridObject()
    {
        if (GridObject.ParentGrid == null)
            GridObject.OnParentSet += OnParentGridSet;
        else
            OnParentGridSet();
    }

    private void OnParentGridSet()
    {
        RegisterToNeighborList();
    }

    private void RegisterToNeighborList()
    {
        //ParentGrid.NeighborGridList.ForEach(
            //val => val)
    }

    private void RegisterToNeighbor(Grid g)
    {
        //g.OnGridObjectSet
    }

    private void UnregisterFromNeigborList()
    {

    }

    private void UnregisterFromNeighbor(Grid g)
    {

    }
}
