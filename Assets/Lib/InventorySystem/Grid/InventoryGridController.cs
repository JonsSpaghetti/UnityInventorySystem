using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// This class controlls the grid.
public class InventoryGridController : MonoBehaviour
{
    #region Singleton  Pattern
    static InventoryGridController singleton;

    public static InventoryGridController Singleton
    {
        get
        {
            singleton = singleton ?? FindObjectOfType<InventoryGridController>();
            return singleton;
        }
    }
    #endregion

    public Transform dropParent;
    public GridDataController inventoryData;

    private GameObject[,] grid; 

    // On start after everything has been set up, it should set itself to inactive to await keypress triggering open.
    private void Start()
    {
        transform.parent.gameObject.SetActive(false);
    }

    private void Update()
    {
    }

    // For use to initially set grid on create.
    public void SetGrid(GameObject[,] grid)
    {
        this.grid = grid;
    }
    

    //Returns true if pick up was successful, otherwise false.
    public bool PickUpWorldItem(GameObject item)
    {
        Vector2Int firstOpenSlot = FirstOpenSlot(); 
        if(firstOpenSlot.x < 0)
        {
            return false;
        }
        else
        {
            // This prevents us from accidentally duping the item upon picking it up.
            item.GetComponent<CanvasGroup>().blocksRaycasts = false;
            GridSlotController slotController = grid[firstOpenSlot.x, firstOpenSlot.y].GetComponent<GridSlotController>();
            slotController.StoreItem(item);
            slotController.LeaveHighlight();
            return true;
        }
    }

    /// <summary>
    /// Will return a Vector2Int of the gridpos that's first open OR will return -1, -1 to indicate that it's not open.
    /// </summary>
    /// <returns></returns>
    Vector2Int FirstOpenSlot()
    {
        foreach(GameObject slot in grid)
        {
            GridSlotController slotController = slot.GetComponent<GridSlotController>();
            // For first unoccupied
            if(slotController.isOccupied == false)
            {
                // return slot pos
                return slotController.gridPos;
            }
            else
            {
                continue;
            }
        }
        return new Vector2Int(-1, -1);
    }
 }
