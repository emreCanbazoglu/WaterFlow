using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGridObject : GridObject
{
    protected override void ParentGridSetting()
    {
        UnregisterFromNeigborList();

        base.ParentGridSetting();
    }

    protected override void ParentGridSet()
    {
        RegisterToNeighborList();

        base.ParentGridSet();
    }

    private void RegisterToNeighborList()
    {
        //ParentGrid.NeighborGridList.ForEach(
            //val => val)
    }

    private void RegisterToNeighbor(Grid g)
    {
        //g.OnGridObjectSet
    }

    private void UnregisterFromNeigborList()
    {

    }

    private void UnregisterFromNeighbor(Grid g)
    {

    }
}
