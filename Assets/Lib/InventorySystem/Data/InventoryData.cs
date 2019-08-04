using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class InventoryData : MonoBehaviour
{
    public Entity playerCharacter;
    public InventoryGridController gridController;
    public List<GameObject> debugInventoryList = new List<GameObject>();

    private Dictionary<Vector2Int, GameObject> items = new Dictionary<Vector2Int, GameObject>();
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
     */

    private void Start()
    {
    }

    public void AddItem(Vector2Int gridPos, GameObject item)
    {
        items[gridPos] = item;

        //Debug only
        debugInventoryList.Add(item);
    }

    public void RemoveItem(Vector2Int gridPos)
    {
        var remove = items[gridPos];
        items.Remove(gridPos);
        debugInventoryList.Remove(remove);
    }
}
