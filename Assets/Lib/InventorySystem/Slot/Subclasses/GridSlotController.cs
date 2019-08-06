using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Controller for slots
public class GridSlotController : SlotController
{
    public Vector2Int gridPos;
    public GridDataController gridData;

    protected override void Awake()
    {
        base.Awake();
        gridData = FindObjectOfType<GridDataController>();
    }

    private void Start()
    {
        text.text = gridPos.x + "," + gridPos.y;
    }

    //Override this to determine how derived slot controllers send data to inventory data
    protected override void AddItemToInventoryData(GameObject item)
    {
        gridData.AddItemToGrid(gridPos, item);
    }

    //Override this to determine how derived slot controllers send data to inventory data
    protected override void RemoveItemFromInventoryData(GameObject item)
    {
        gridData.RemoveItemFromGrid(gridPos);
    }
}
