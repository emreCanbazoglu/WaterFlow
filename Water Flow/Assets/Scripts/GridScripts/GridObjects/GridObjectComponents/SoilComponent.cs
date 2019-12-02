using UnityEngine;

[RequireComponent(typeof(Irrigable))]
public class SoilComponent : MonoBehaviour
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

    private Irrigable _irrigable;
    public Irrigable Irrigable
    {
        get
        {
            if (_irrigable == null)
                _irrigable = GetComponent<Irrigable>();

            return _irrigable;
        }
    }

    private void Awake()
    {
        RegisterToIrrigable();
    }

    private void OnDestroy()
    {
        UnregisterFromIrrigable();
    }

    private void RegisterToIrrigable()
    {
        Irrigable.OnIrrigated += OnIrrigated;
    }

    private void UnregisterFromIrrigable()
    {
        Irrigable.OnIrrigated -= OnIrrigated;
    }

    private void OnIrrigated(WaterComponent obj)
    {
        GridObject.ParentGrid.SetGridObject(GridObject.EType.Grass);
    }
}
