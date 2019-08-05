using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public InventoryData inventoryData;

    private GameObject[,] grid; 
    private GameObject highlightedSlot;
    /* TODO
     * Need to add ability to push changes to grid to InventoryData list and vice versa (pick up item in overworld and it just goes into first open inventory slot).
     * methods needed: 
     * FindFirstOpenSlot
     * SyncWithInventory - Maybe this should be event driven?  Item comes in, send event.  Could use scriptable object event manager.
     * Need world prefabs for items.
     * Need to add drop zone and controller to the inventory screen
     * Need to add character equip zone + controller + data to the inventory screen
     */

    private void Start()
    {
        transform.parent.gameObject.SetActive(false);
    }

    private void Update()
    {
        UpdateHighlightedSlotColor();
        HandleInput();
    }

    void HandleInput()
    {
        // When someone lets go of the mouse on a slot & they have an item on their cursor, do something..
        if (Input.GetMouseButtonUp(0))
        {
            GameObject selectedItem = ItemController.GetSelectedItem();
            if(highlightedSlot != null && selectedItem != null)
            {
                if (highlightedSlot.GetComponent<SlotController>().isOccupied)
                {
                    //If occupied, swap
                    ItemController.SetSelectedItem(SwapItem(selectedItem));
                }
                else
                {
                    //else, just store item
                    StoreItem(selectedItem);
                    ItemController.ResetSelectedItem();
                }
            }
            else if(highlightedSlot != null && selectedItem == null && highlightedSlot.GetComponent<SlotController>().isOccupied == true)
            {
                // If we are on a slot && don't have a selected item && there's an item present
                UpdateHighlightedSlotColor();
                ItemController.SetSelectedItem(GetItem(highlightedSlot));
            }
        }
    }

    void StoreItem(GameObject item)
    {

        SlotController highlightedSlotController = highlightedSlot.GetComponent<SlotController>();
        // Insert stored Item into highlighted slot.
        GameObject storedItem = highlightedSlotController.InsertItem(item);

        // Set parent to drop parent and lock to slot position.
        storedItem.transform.SetParent(dropParent);
        storedItem.GetComponent<RectTransform>().pivot = Vector2.zero;
        storedItem.transform.position = highlightedSlot.transform.position;

        // Make icon solid again.
        CanvasGroup canvasGroup = storedItem.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;

        // Add item to inventoryData
        inventoryData.AddItem(highlightedSlotController.gridPos, storedItem);
        // TODO - Update Overlay as well.
    }

    /// <summary>
    /// Swap out an item in the highlighted slot and return the item that we're swapping for to be put on the pointer.
    /// </summary>
    /// <param name="item">Item to put into the inventory</param>
    /// <returns>Item to put on mouse pointer</returns>
    GameObject SwapItem(GameObject item)
    {
        GameObject itemToReturn = GetItem(highlightedSlot);
        StoreItem(item);
        UpdateHighlightedSlotColor();
        return itemToReturn;
    }

    //Get Item from a slot
    GameObject GetItem(GameObject slot)
    {
        SlotController slotController = slot.GetComponent<SlotController>();
        GameObject itemToReturn = slotController.storedItemObject;
        slotController.storedItemObject = null;
        slotController.isOccupied = false;

        itemToReturn.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        itemToReturn.GetComponent<CanvasGroup>().alpha = 0.5f;
        itemToReturn.transform.position = Input.mousePosition;
        inventoryData.RemoveItem(slotController.gridPos);
        // TODO - Update Overlay
        return itemToReturn;
    }

    /// <summary>
    /// Update the color of the highlighted slot
    /// </summary>
    void UpdateHighlightedSlotColor()
    {
        if (!highlightedSlot)
        {
            return;
        }
        SlotController highlightedSlotController = highlightedSlot.GetComponent<SlotController>();
        if(highlightedSlotController.isOccupied == true)
        {
            // Blue if the highlighted slot is occupied
            highlightedSlotController.GetComponent<Image>().color = SlotColorHighlights.Blue2;
        }
        else
        {
            //Grey otherwise.
            highlightedSlotController.GetComponent<Image>().color = SlotColorHighlights.Gray;
        }
    }

    // For use to initially set grid on create.
    public void SetGrid(GameObject[,] grid)
    {
        this.grid = grid;
    }
    
    // Update highlighted slot from the SlotController of the currently hovered slot
    public void SetHighlightedSlot(GameObject slot)
    {
        highlightedSlot = slot;
    }
}
