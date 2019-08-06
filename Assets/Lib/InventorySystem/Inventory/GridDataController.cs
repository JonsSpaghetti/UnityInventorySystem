using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GridDataController : MonoBehaviour
{
    // Make sure to pass an instance of this class to the playercharacter somehow. They'll need to call the AddItem method in order to actually interact with this.
    public InventoryGridController gridController;
    public List<GameObject> debugInventoryList = new List<GameObject>();

    private Dictionary<ItemType, List<GameObject>> nonGridItems = new Dictionary<ItemType, List<GameObject>>();
    private Dictionary<Vector2Int, GameObject> gridItems = new Dictionary<Vector2Int, GameObject>();
    //List of requirements
    /* TODO 
     * 1. hold items
     * 2. Player equips
     * 3. Item types
     * 4. Tooltips
     * 5. Data
     * 6. Persist
     * 7. weight/d2 grid system or similar
     * 
     * Things we need to do
     * 1. Data representation of items to allow for easy content creation
     *  - This would include things like icon
     *  - Name
     *  - Stats
     *  - Type of weapon
     *  - Rarity
     *  - This is already created via SOs in the Item class and subclasses
     * 2. Data representation of the inventory
     *  - Orderable list so that we can keep track of items and their orders
     *  - List should contain objects so that we can attach scripts and implement drag/drop handlers
     * 3. Visual representatino of the inventory
     *  - Should be the UI
     *  - Should take items and allow changing of the order in the orderable list
     *  - Might have to generate slots dynamically and then assign handlers/indexes to each
     *  - Alternatively each slot could have an index and hold a reference to the item in the orderable list - just display info/icon.  drop into new slot = reassign index
     * 4. Drop items on ground (zone outside of window or something like that)
     * 5. No grid stuff - every item fits in a single hole
     * 6. Maybe some kind of csv or other db representation of data/stats
     * 7. Two scripts per item - one ItemData, ItemBehavior
     * 
     * New TODO - 8/5
     * 1. Create generic player Equip Script
     * 2. Allow passing in Item Type that can be equipped
     * 3. Have a Controller class to handle dropping items into each slot etc.
     * 4. Have a Data Controller class to pull all controllers together and summarize
     * 5. Give reference to data controller to player
     * 6. Need to allow for things like level check class check etc. (just allow restrictions to be run)
     * 7. If we fail restriction check, highlight red, disallow drop, pass = green, allow etc.
     * 
     * BIG TODO
     * - Maybe rewrite this so that all slots inherit from a generic slot controller that takes care of things like highlighting etc.
     * - Have individual functionality for the slots that are for equips etc.
     */

    // For use with grid
    public void AddItemToGrid(Vector2Int gridPos, GameObject item)
    {
        gridItems[gridPos] = item;

        //Debug only
        debugInventoryList.Add(item);
    }

    // For generic use

    public void AddItem(GameObject item)
    {
        ItemType itemType = item.GetComponent<ItemData>().itemType;
        
        // new the list if it's null
        nonGridItems[itemType] = nonGridItems[itemType] ?? new List<GameObject>();
        nonGridItems[itemType].Add(item);
        //Debug only
        debugInventoryList.Add(item);
    }

    public void RemoveItemFromGrid(Vector2Int gridPos)
    {
        var remove = gridItems[gridPos];
        gridItems.Remove(gridPos);
        debugInventoryList.Remove(remove);
    }
    public void RemoveItem(GameObject item)
    {
        ItemType itemType = item.GetComponent<InventoryItemController>().myItemData.itemType;
        nonGridItems[itemType].Remove(item);
        debugInventoryList.Remove(item);
    }

    // TODO - transform this into an event - scriptable object model where we register/send events on collision.
    // TODO - That way we can pick up a world item and send notifications out to all the things that care.
    public bool PickUpWorldItem(GameObject item)
    {
        return gridController.PickUpWorldItem(item);
    }
}
