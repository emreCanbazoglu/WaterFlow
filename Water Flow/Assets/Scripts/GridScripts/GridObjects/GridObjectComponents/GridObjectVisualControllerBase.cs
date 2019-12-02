using UnityEngine;

[RequireComponent(typeof(GridObject))]
[ExecuteInEditMode]
public class GridObjectVisualControllerBase : MonoBehaviour
{
    [SerializeField]
    private Material _objMaterial;

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
        InitGridMaterial();
    }

    private void OnDestroy()
    {
        if (GridObject)
        {
            GridObject.OnParentSet -= OnGridSet;

            if (GridObject.ParentGrid)
                GridObject.ParentGrid.VisualController.ResetGridMaterial();
        }
    }

    private void InitGridMaterial()
    {
        if (GridObject.ParentGrid != null)
            SetGridMaterial();
        else
            GridObject.OnParentSet += OnGridSet;
    }

    private void OnGridSet()
    {
        SetGridMaterial();
    }

    private void SetGridMaterial()
    {
        GridObject.ParentGrid.VisualController.SetGridMaterial(_objMaterial);

        InitObjMaterial();
    }

    protected virtual void InitObjMaterial()
    {

    }
}
