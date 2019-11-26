using UnityEngine;
using System.Collections.Generic;

public class Board : MonoBehaviour
{
    [SerializeField]
    private Vector2 _origin;

    [SerializeField]
    private int _rowCount;
    public int RowCount
    {
        get
        {
            return _rowCount;
        }
        set
        {
            _rowCount = value;
        }
    }

    [SerializeField]
    private int _coloumnCount;
    public int ColoumnCount
    {
        get
        {
            return _coloumnCount;
        }
        set
        {
            _coloumnCount = value;
        }
    }

    [SerializeField]
    private Grid[,] _gridArr;
    public Grid[,] GridArr
    {
        get
        {
            return _gridArr;
        }
    }

    public void CreateBoard()
    {
        if (_gridArr != null)
            DestroyBoard();

        _gridArr = new Grid[ColoumnCount, RowCount];

        for(int i = 0; i < RowCount; i++)
        {
            for(int j = 0; j < ColoumnCount; j++)
            {
                _gridArr[j, i] = GridFactory.Instance.CreateGridInstance(
                    _origin,
                    i, j);

                _gridArr[j, i].InitGrid(this, j, i);

                _gridArr[j, i].transform.SetParent(transform);
            }
        }

        foreach (Grid g in GridArr)
            g.PostInitGrid();
    }

    public void DestroyBoard()
    {
        foreach(Grid g in _gridArr)
        {
            if (g == null)
                continue;

            DestroyImmediate(g.gameObject);
        }
    }

    public List<Grid> GetNeighborGrids(Grid grid)
    {
        List<Grid> neigborList = new List<Grid>();

        int x = grid.X;
        int y = grid.Y;

        for(int i = -1; i <= 1; i++)
        {
            for(int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                    continue;

                int tempX = x + i;
                int tempY = y + j;

                if (tempX == ColoumnCount)
                    continue;

                if (tempY == RowCount)
                    continue;

                neigborList.Add(GridArr[tempX, tempY]);
            }
        }

        return neigborList;
    }
}
