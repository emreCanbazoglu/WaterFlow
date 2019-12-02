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
    [HideInInspector]
    private Grid[] _gridArr;
    public Grid[] GridArr
    {
        get
        {
            return _gridArr;
        }
    }

    [SerializeField]
    [HideInInspector]
    private GameObject[] _rowCarrierArr;

    public void CreateBoard()
    {
        if (_gridArr != null)
            DestroyBoard();

        _rowCarrierArr = new GameObject[RowCount];

        _gridArr = new Grid[ColoumnCount * RowCount];

        for(int i = 0; i < RowCount; i++)
        {
            _rowCarrierArr[i] = new GameObject("Row_" + i.ToString());

            _rowCarrierArr[i].transform.SetParent(transform);

            for(int j = 0; j < ColoumnCount; j++)
            {
                _gridArr[GetGridIndex(j, i)] = GridFactory.Instance.CreateGridInstance(
                    _origin,
                    i, j);

                GetGrid(j, i).InitGrid(this, j, i);

                GetGrid(j, i).transform.SetParent(_rowCarrierArr[i].transform);
            }
        }

        foreach (Grid g in GridArr)
            g.PostInitGrid();
    }

    public void DestroyBoard()
    {
        foreach(GameObject g in _rowCarrierArr)
        {
            DestroyImmediate(g);
        }

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

        bool isRowOdd = y % 2 == 1;

        int tempX;
        int tempY;

        if (isRowOdd)
        {
            tempX = x - 1;

            for (int i = -1; i <= 1; i++)
            {
                tempY = y + i;

                if (!IsValidGrid(tempX, tempY))
                    continue;

                neigborList.Add(GetGrid(tempX, tempY));
            }

            tempX = x;

            for (int i = -1; i <= 1; i++)
            {
                tempY = y + i;

                if (!IsValidGrid(tempX, tempY)
                    || tempY == y)
                    continue;

                neigborList.Add(GetGrid(tempX, tempY));
            }

            tempX = x + 1;
            tempY = y;

            if (IsValidGrid(tempX, y))
                neigborList.Add(GetGrid(tempX, tempY));
        }
        else
        {
            tempX = x + 1;

            for (int i = -1; i <= 1; i++)
            {
                tempY = y + i;

                if (!IsValidGrid(tempX, tempY))
                    continue;

                neigborList.Add(GetGrid(tempX, tempY));
            }

            tempX = x;

            for (int i = -1; i <= 1; i++)
            {
                tempY = y + i;

                if (!IsValidGrid(tempX, tempY)
                    || tempY == y)
                    continue;

                neigborList.Add(GetGrid(tempX, tempY));
            }

            tempX = x - 1;
            tempY = y;

            if (IsValidGrid(tempX, y))
                neigborList.Add(GetGrid(tempX, tempY));
        }

        return neigborList;
    }

    private bool IsValidGrid(int coloumn, int row)
    {
        if (row < 0
            || row >= RowCount)
            return false;

        if (coloumn < 0
            || coloumn >= ColoumnCount)
            return false;

        return true;
    }

    public Grid GetGrid(int coloumn, int row)
    {
        return _gridArr[GetGridIndex(coloumn, row)];
    }

    private int GetGridIndex(int coloumn, int row)
    {
        return row * ColoumnCount + coloumn;
    }
}
