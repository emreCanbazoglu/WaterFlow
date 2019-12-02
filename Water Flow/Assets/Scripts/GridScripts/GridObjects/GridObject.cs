using UnityEngine;
using System;

public class GridObject : MonoBehaviour
{
    public enum EType
    {
        None,
        Water,
        Grass,
        Soil,
    }

    [SerializeField]
    private EType _type;
    public EType Type
    {
        get
        {
            return _type;
        }
    }

    #region Events
    public Action OnParentSet { get; set; }
    #endregion

    public Grid ParentGrid { get; private set; }

    public void SetParentGrid(Grid grid)
    {
        ParentGrid = grid;

        transform.SetParent(ParentGrid.transform);

        OnParentSet?.Invoke();
    }

    public void DestroyGridObject()
    {
        DestroyImmediate(gameObject);
    }
}
