using UnityEngine;
using System;

[RequireComponent(typeof(GridObject))]
public class Irrigable : MonoBehaviour
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

    public bool IsIrrigated { get; private set; }

    #region Events
    public Action<WaterComponent> OnIrrigated { get; set; }
    #endregion

    public bool TryIrrigate(WaterComponent water)
    {
        if (IsIrrigated)
            return false;

        Debug.Log("Irrigated!");

        IsIrrigated = true;

        OnIrrigated?.Invoke(water);

        return true;
    }
}
