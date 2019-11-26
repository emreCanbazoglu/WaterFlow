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

    public Grid ParentGrid { get; private set; }

    public void SetParentGrid(Grid grid)
    {
        ParentGridSetting();

        ParentGrid = grid;

        transform.SetParent(ParentGrid.transform);

        ParentGridSet();
    }

    protected virtual void ParentGridSetting()
    {

    }

    protected virtual void ParentGridSet()
    {

    }

    public void DestroyGridObject(Action onDestroyedCallback)
    {
        Action onCompletedDel = delegate ()
        {
            Destroy(gameObject);

            onDestroyedCallback?.Invoke();
        };

        GridObjectDestroying(onCompletedDel);
    }

    protected virtual void GridObjectDestroying(Action onCompletedCallback)
    {
        onCompletedCallback?.Invoke();
    }
}
