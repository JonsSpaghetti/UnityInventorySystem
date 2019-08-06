using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Controller for slots
public class SlotController : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public Text text;
    public Image image;
    public GameObject storedItemObject;
    public Transform dropParent;
    //public ItemClass storedItemClass;
    public bool isOccupied;

    private Color initialColor;

    protected virtual void Awake()
    {
        dropParent = GameObject.FindGameObjectWithTag("DropParent").transform;
        initialColor = image.color;
    }

    // At this point, we're calling the event ONLY on the highlighted slot anyway.
    public void OnPointerClick(PointerEventData eventData)
    {
        HandlePointerClick();
    }

    //Override this if there are different circumstances around handling trying to put an item into a slot
    public virtual void HandlePointerClick()
    {
        GameObject selectedItem = InventoryItemController.GetSelectedItem();
        if(selectedItem != null)
        {
            if (isOccupied)
            {
                //If occupied, swap
                InventoryItemController.SetSelectedItem(SwapItem(selectedItem));
            }
            else
            {
                //else, just store item
                StoreItem(selectedItem);
                InventoryItemController.ResetSelectedItem();
            }
        }
        else if(selectedItem == null && isOccupied == true)
        {
            // If we are on a slot && don't have a selected item && there's an item present
            InventoryItemController.SetSelectedItem(GetItem());
        }
    }

    public void StoreItem(GameObject item)
    {
        // Insert stored Item into highlighted slot.
        GameObject storedItem = InsertItem(item);

        // Set parent to drop parent and lock to slot position.
        // TODO - could probably do this in the inventoryitemcontroller
        storedItem.transform.SetParent(dropParent);
        storedItem.GetComponent<RectTransform>().pivot = Vector2.zero;
        storedItem.transform.position = transform.position;

        // Make icon solid again.
        CanvasGroup canvasGroup = storedItem.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;

        // Add item to inventoryData
        AddItemToInventoryData(storedItem);
        // TODO - Update Overlay as well.
    }

    /// <summary>
    /// Swap out an item in the highlighted slot and return the item that we're swapping for to be put on the pointer.
    /// </summary>
    /// <param name="item">Item to put into the inventory</param>
    /// <returns>Item to put on mouse pointer</returns>
    GameObject SwapItem(GameObject item)
    {
        GameObject itemToReturn = GetItem();
        StoreItem(item);
        return itemToReturn;
    }

    //Get Item from a slot
    GameObject GetItem()
    {
        GameObject itemToReturn = storedItemObject;
        storedItemObject = null;
        isOccupied = false;

        itemToReturn.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        itemToReturn.GetComponent<CanvasGroup>().alpha = 0.5f;
        itemToReturn.transform.position = Input.mousePosition;
        RemoveItemFromInventoryData(itemToReturn);
        // TODO - Update Overlay
        return itemToReturn;
    }

    //Override this to determine how derived slot controllers send data to inventory data
    protected virtual void AddItemToInventoryData(GameObject item)
    {
        throw new System.NotImplementedException();
    }

    //Override this to determine how derived slot controllers send data to inventory data
    protected virtual void RemoveItemFromInventoryData(GameObject item)
    {
        throw new System.NotImplementedException();
    }


    // When pointer enters, update the InventoryGridController with the highlighted slot.
    public void OnPointerEnter(PointerEventData eventData)
    {
        EnterHighlight();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeaveHighlight();
    }


    // Make highlighting virtual so we can override it if we need to like in Equips.
    public virtual void EnterHighlight()
    {
        // If we have a selected item, highlight
        if (InventoryItemController.GetSelectedItem() && !isOccupied)
        {
            image.color = SlotColorHighlights.Green;
        }
        else if (isOccupied)
        {
            image.color = SlotColorHighlights.Yellow; 
        }
    }

    public virtual void LeaveHighlight()
    {
        if (isOccupied)
        {
            image.color = SlotColorHighlights.Blue2;
        }
        else
        {
            image.color = initialColor;
        }
    }

    public GameObject InsertItem(GameObject item)
    {
        storedItemObject = item;
        isOccupied = true;
        return storedItemObject;
    }

}

public struct SlotColorHighlights
{
    public static Color32 Green
    { get { return new Color32(127, 223, 127, 255); } }
    public static Color32 Yellow
    { get { return new Color32(223, 223, 63, 255); } }
    public static Color32 Red
    { get { return new Color32(223, 127, 127, 255); } }
    public static Color32 Blue
    { get { return new Color32(159, 159, 223, 255); } }
    public static Color32 Blue2
    { get { return new Color32(191, 191, 223, 255); } }
    public static Color32 Gray
    { get { return new Color32(223, 223, 223, 255); } }
}

