using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridObjectFactory : MonoBehaviour
{
    private static GridObjectFactory _instance;
    public static GridObjectFactory Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<GridObjectFactory>();

            return _instance;
        }
    }

    [SerializeField]
    private List<GridObject> _gridObjectPrefabList;


    public GridObject CreateGridObjectInstance(GridObject.EType type)
    {
        GridObject gridObjPrefab = _gridObjectPrefabList
            .First(val => val.Type == type);

        GridObject cloneObj = Instantiate(gridObjPrefab);

        return cloneObj;
    }
}
