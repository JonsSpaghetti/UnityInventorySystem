using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

class EquipSlotController : SlotController
{
    public ItemType allowedType; // Type of item this equipment slot can take
    public EquipSlotType slotType;
    public EquipDataController equipData;

    protected override void Awake()
    {
        base.Awake();
        equipData = FindObjectOfType<EquipDataController>();
    }

    //Override this to determine how derived slot controllers send data to their own data classes/controllers
    protected override void AddItemToInventoryData(GameObject item)
    {
        equipData.AddItemToSlot(slotType, item);
    } 

    //Override this to determine how derived slot controllers send data to inventory data
    protected override void RemoveItemFromInventoryData(GameObject item)
    {
        equipData.RemoveItemFromSlot(slotType, item);
    }

    //Returns true if can or false if can't equip
    public bool CanEquip(GameObject item)
    {
        ItemData itemData = item.GetComponent<InventoryItemController>().myItemData;
        if (itemData.itemType != allowedType)
        {
            return false;
        }
        // TODO - implement player level checking etc.
        //else if(itemData.itemLevel > playerLevel)
        //{
        //    return false;
        //}
        else
        {
            return true;
        }
    }

    public override void HandlePointerClick()
    {
        GameObject selectedItem = InventoryItemController.GetSelectedItem();
        if (selectedItem == null || CanEquip(selectedItem))
        {
            base.HandlePointerClick();
        }
        else
        {
            Debug.Log("You can't do that you idiot");
        }
    }

    public override void EnterHighlight()
    {
        GameObject selectedItem = InventoryItemController.GetSelectedItem();
        // If we have a selected item, and we can equip, highlight green
        if (selectedItem && !isOccupied && CanEquip(selectedItem))
        {
            image.color = SlotColorHighlights.Green;
        }
        // If we have a selected item and are occupied and can equip, highlight yellow
        else if (selectedItem && isOccupied && CanEquip(selectedItem))
        {
            image.color = SlotColorHighlights.Yellow;
        }
        // else we have a selected item and cannot equip, highlight red
        else if(selectedItem && !CanEquip(selectedItem)) 
        {
            image.color = SlotColorHighlights.Red;
        }
    }
}
