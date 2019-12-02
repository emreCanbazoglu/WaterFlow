using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridFactory : MonoBehaviour
{
    private static GridFactory _instance;
    public static GridFactory Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<GridFactory>();
             
            return _instance;
        }
    }

    [SerializeField]
    private Grid _gridPrefab;

    [SerializeField]
    private float _edgeSize;

    [SerializeField]
    private float _heightPerLevel;

    private readonly float _sqrt3 = Mathf.Sqrt(3);

    public Grid CreateGridInstance(Vector3 origin, int row, int coloumn)
    {
        Grid g = Instantiate(_gridPrefab);

        InitGrid(g, origin, row, coloumn);

        return g;
    }

    private void InitGrid(
        Grid g,
        Vector3 origin,
        int row,
        int coloumn)
    {
        InitGridCoordinates(g, origin, row, coloumn);
        //InitGridScale(g);
    }

    private void InitGridCoordinates(
        Grid g,
        Vector3 origin,
        int row,
        int coloumn)
    {
        Vector3 pos = origin;

        float width = _sqrt3 * _edgeSize;
        float height = 2 * _edgeSize;

        float initPosXOffset = ((row + 1) % 2) * (width / 2.0f);
        float initPosZOffset = (height / 2.0f);

        pos.x += initPosXOffset + coloumn * width;
        pos.z -= initPosZOffset + row * height * 0.75f;

        g.transform.position = pos;

    }

    private void InitGridScale(Grid g)
    {
        Vector3 scale = transform.localScale * _edgeSize;

        g.transform.localScale = scale;
    }
}
