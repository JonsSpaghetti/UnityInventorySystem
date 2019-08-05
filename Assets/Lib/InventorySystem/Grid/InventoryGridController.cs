using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// This class controlls the grid.
public class InventoryGridController : MonoBehaviour, IPointerExitHandler
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
    public InventoryDataController inventoryData;

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

    // On start after everything has been set up, it should set itself to inactive to await keypress triggering open.
    private void Start()
    {
        transform.parent.gameObject.SetActive(false);
    }

    private void Update()
    {
        UpdateGridColors();
        HandleInput();
    }

    void HandleInput()
    {
        // When someone lets go of the mouse on a slot & they have an item on their cursor, do something..
        if (Input.GetMouseButtonUp(0))
        {
            GameObject selectedItem = InventoryItemController.GetSelectedItem();
            Debug.Log($"highlighted: {highlightedSlot}, selected: {selectedItem}");
            if(highlightedSlot != null && selectedItem != null)
            {
                if (highlightedSlot.GetComponent<SlotController>().isOccupied)
                {
                    //If occupied, swap
                    InventoryItemController.SetSelectedItem(SwapItem(selectedItem));
                }
                else
                {
                    //else, just store item
                    StoreItem(highlightedSlot, selectedItem);
                    InventoryItemController.ResetSelectedItem();
                }
            }
            else if(highlightedSlot != null && selectedItem == null && highlightedSlot.GetComponent<SlotController>().isOccupied == true)
            {
                // If we are on a slot && don't have a selected item && there's an item present
                UpdateHighlightedSlotColor();
                InventoryItemController.SetSelectedItem(GetItem(highlightedSlot));
            }
        }
    }

    void StoreItem(GameObject slot, GameObject item)
    {

        SlotController slotController = slot.GetComponent<SlotController>();
        // Insert stored Item into highlighted slot.
        GameObject storedItem = slotController.InsertItem(item);

        // Set parent to drop parent and lock to slot position.
        storedItem.transform.SetParent(dropParent);
        storedItem.GetComponent<RectTransform>().pivot = Vector2.zero;
        storedItem.transform.position = slot.transform.position;

        // Make icon solid again.
        CanvasGroup canvasGroup = storedItem.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;

        // Add item to inventoryData
        inventoryData.AddItem(slotController.gridPos, storedItem);
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
        StoreItem(highlightedSlot, item);
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

    // Update grid colors every frame  so we are NEVER wrong
    void UpdateGridColors()
    { 
        foreach(GameObject slot in grid)
        {
            SlotController slotController = slot.GetComponent<SlotController>();
            if (slotController.isOccupied)
            {
                slotController.GetComponent<Image>().color = SlotColorHighlights.Blue2;
            }
            else if (slotController.isOccupied == false)
            {
                slotController.GetComponent<Image>().color = Color.white;
            }
        }
        UpdateHighlightedSlotColor();
    }

    /// <summary>
    /// Update the color of the highlighted slot
    /// </summary>
    void UpdateHighlightedSlotColor()
    {
        if (!highlightedSlot || InventoryItemController.GetSelectedItem() == null)
        {
            return;
        }
        SlotController highlightedSlotController = highlightedSlot.GetComponent<SlotController>();
        if(highlightedSlotController.isOccupied == true)
        {
            // Blue if the highlighted slot is occupied
            highlightedSlotController.GetComponent<Image>().color = SlotColorHighlights.Yellow;
        }
        else
        {
            //Green otherwise.
            highlightedSlotController.GetComponent<Image>().color = SlotColorHighlights.Green;
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

    //Returns true if pick up was successful, otherwise false.
    public bool PickUpWorldItem(GameObject item)
    {
        highlightedSlot = null;
        Vector2Int firstOpenSlot = FirstOpenSlot(); 
        if(firstOpenSlot.x < 0)
        {
            return false;
        }
        else
        {
            // This prevents us from accidentally duping the item upon picking it up.
            item.GetComponent<CanvasGroup>().blocksRaycasts = false;
            StoreItem(grid[firstOpenSlot.x, firstOpenSlot.y], item);
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
            SlotController slotController = slot.GetComponent<SlotController>();
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

    public void OnPointerExit(PointerEventData eventData)
    {
        highlightedSlot = null;
    }
}
