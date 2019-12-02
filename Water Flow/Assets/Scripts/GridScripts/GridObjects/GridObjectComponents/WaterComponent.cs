using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

[RequireComponent(typeof(GridObject))]
public class WaterComponent : MonoBehaviour
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

    [SerializeField]
    private float _irrigationDuration;
    public float WaterLevel { get; private set; }

    private List<Grid> _registeredNeighborList = new List<Grid>();
    private List<Irrigable> _irrigatableNeighborList = new List<Irrigable>();

    private IEnumerator _irrigationRoutine;

    #region Events
    public Action<Grid> OnIrrigatedGrid { get; set; }
    #endregion

    private void Awake()
	{
		TryRegisterToGridObject();
	}

	private void OnDestroy()
	{
		UnregisterFromGridObject();
        UnregisterFromNeigborList();
    }

    private void Update()
    {
        TryIrrigateAllNeighbors();
    }

    private void TryRegisterToGridObject()
	{
		GridObject.OnParentSet += OnParentGridSet;

		if (GridObject.ParentGrid != null)
			OnParentGridSet();
	}

	private void UnregisterFromGridObject()
	{
		GridObject.OnParentSet -= OnParentGridSet;
	}

	private void OnParentGridSet()
	{
        UnregisterFromNeigborList();

        RegisterToNeighborList();

        IrrigateSelf();
    }

    private void IrrigateSelf()
    {
        _irrigationRoutine = IrrigateSelfProgress();
        StartCoroutine(_irrigationRoutine);
    }

    private IEnumerator IrrigateSelfProgress()
    {
        float curDuration = 0;

        float irrigationPerc = 0;

        while(curDuration < _irrigationDuration)
        {
            irrigationPerc = curDuration / _irrigationDuration;

            WaterLevel = (1.0f - irrigationPerc) * GridObject.ParentGrid.LevelController.Level;

            curDuration += Time.deltaTime;

            yield return null;
        }
    }

    private void RegisterToNeighborList()
	{
		foreach (Grid g in GridObject.ParentGrid.NeighborGridList)
			RegisterToNeighbor(g);
	}

	private void RegisterToNeighbor(Grid g)
	{
		g.OnGridObjectSet += OnNeighborGridObjectSet;

        _registeredNeighborList.Add(g);

        TryAddToIrritableGridList(g);

    }

	private void OnNeighborGridObjectSet(Grid g)
	{
        TryAddToIrritableGridList(g);
    }

    private void TryAddToIrritableGridList(Grid g)
    {
        if (!g.GridObject)
            return;

        Irrigable i = g.GridObject.GetComponent<Irrigable>();

        if (!i)
        {
            _irrigatableNeighborList.Remove(i);

            return;
        }

        if (!_irrigatableNeighborList.Contains(i))
            _irrigatableNeighborList.Add(i);
    }

    private void TryIrrigateAllNeighbors()
    {
        for(int i = 0; i < _irrigatableNeighborList.Count; i++)
        {
            if (TryIrrigateGrid(_irrigatableNeighborList[i]))
                i--;
        }
    }

    private bool TryIrrigateGrid(Irrigable i)
	{
        if (i.GridObject.ParentGrid.LevelController.Level > GridObject.ParentGrid.LevelController.Level)
            return false;

        if (WaterLevel > i.GridObject.ParentGrid.LevelController.Level)
            return false;

        if (i.TryIrrigate(this))
        {
            _irrigatableNeighborList.Remove(i);

            OnIrrigatedGrid?.Invoke(i.GridObject.ParentGrid);

            return true;
        }

        return false;
    }


	private void UnregisterFromNeigborList()
	{
        while(_registeredNeighborList.Count > 0)
            UnregisterFromNeighbor(_registeredNeighborList[0]);
	}

	private void UnregisterFromNeighbor(Grid g)
	{
		g.OnGridObjectSet += OnNeighborGridObjectSet;

        _registeredNeighborList.Remove(g);
        _irrigatableNeighborList.Remove(g.GridObject.GetComponent<Irrigable>());
	}
}
