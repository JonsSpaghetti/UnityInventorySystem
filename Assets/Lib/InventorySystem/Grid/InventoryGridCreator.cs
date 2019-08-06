using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

//This class creates the grid and that's its only job.

class InventoryGridCreator : MonoBehaviour
{
    #region Singleton Pattern
    static InventoryGridCreator singleton;

    public static InventoryGridCreator Singleton
    {
        get
        {
            singleton = singleton ?? FindObjectOfType<InventoryGridCreator>();
            return singleton;
        }
    }
    #endregion

    public GameObject[,] slotGrid;
    public GameObject slotPrefab;
    public Vector2Int gridSize;
    public float slotSize;
    public float edgePadding;
    public InventoryGridController gridController;

    public void Awake()
    {
        // Create the 2d array of gameobjects (item slots)
        slotGrid = new GameObject[gridSize.x, gridSize.y];
        ResizePanel();
        CreateSlots();
        //GetComponent<InvenGridManager>().gridSize = gridSize;
    }

    private void CreateSlots()
    {
        // Actually create the slots with padding.
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                GameObject obj = (GameObject)Instantiate(slotPrefab);

                obj.transform.name = "slot[" + x + "," + y + "]";
                obj.transform.SetParent(this.transform);
                RectTransform rect = obj.transform.GetComponent<RectTransform>();
                rect.localPosition = new Vector3(x * slotSize + edgePadding, y * slotSize + edgePadding, 0);
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);
                obj.GetComponent<RectTransform>().localScale = Vector3.one;
                obj.GetComponent<GridSlotController>().gridPos = new Vector2Int(x, y);
                slotGrid[x, y] = obj;
            }
        }
        // Pass those to the controller.
        gridController.SetGrid(slotGrid);
    }

    private void ResizePanel()
    {
        float width, height;
        width = (gridSize.x * slotSize) + (edgePadding * 2);
        height = (gridSize.y * slotSize) + (edgePadding * 2);

        RectTransform rect = GetComponent<RectTransform>();
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        rect.localScale = Vector3.one;
    }

}
