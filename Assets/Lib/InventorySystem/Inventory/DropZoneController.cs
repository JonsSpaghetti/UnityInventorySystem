using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZoneController : MonoBehaviour, IPointerClickHandler // IPointerEnterHandler
{
    // Inventory Owner must be set otherwise everything blows up
    public GameObject inventoryOwner;

    public void OnPointerClick(PointerEventData eventData)
    {
       Debug.Log("DING DING DING DING");
        GameObject item = InventoryItemController.GetSelectedItem();
        if(item)
        {
            // If an item is on the cursor and we drop it in the dropzone...
            item.GetComponent<InventoryItemController>().DropItem(inventoryOwner);
        }
    }

}
