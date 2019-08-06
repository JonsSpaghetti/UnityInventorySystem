JK this has apparently turned into inventorypalooza.

# Overview

Everying is in InventoryScene2 - that should give a good idea of the capabilities of the system.
The project is broken down as follows:
An item can exist in two places
1. The inventory UI
2. The world

The inventory UI and the world will have separate prefabs associated with them, but when dropping/picking up items, will spawn their counterpart in the appropriate location.

The inventory UI is further broken down into the following pieces:
1. Equipment UI/Data
2. Grid UI/Data
3. Whatever else I decide to do

For each of these pieces, there's generally some kind of controller for the actual UI and a data class as well to organize things and pass data to the player.

The player should have a reference to each Data class.  There will be some wiring that needs to be done (make sure that all of the gridcontroller/griddata hookups are completed) but generally, we should be able to take the prefab for InventoryUI and drop it into a scene with minimal mess.

From there, slot sprites and other UI elements can be adjusted to achieve the desired look.

## Inventory API
There are a few things that are important to note.
There are a number of overrideable methods that will allow fine tuning of slot behavior.  For example, most slots will highlight green when attempting to move an item into them since there are no restrictions on many of the inventory slots we might have.  However, for equip slots, you'll see that if the equipment type/level etc. doesn't match, the slot will highlight red and will not allow you to drop the item into it.
* When making a new type of slot, inherit from SlotController.  
  * HandlePointerClick can be overriden and will be called when someone clicks on the slot.  Handling cases where you have an item on your cursor/slot is occupied etc. is a good idea.
  * EnterHighlight and ExitHighlight can be overridden and will be called when mouse enters/exits the slot.  Add logic to determine what colors you want these highlights to be again given that there could be an item occupying the slot/on the cursor.
  * AddItemTo/RemoveItemFromInventoryData must be overridden or you'll get an exception and will be called upon successful item exchange (addition or removal) in order to sync the data class that the player watches.
  * Awake should probably be overridden since you're going to create a specific data class for your slot type right?
  
By and large, the rest of inventory is pretty self explanatory.  InventoryGridCreator creats slots from a slot prefab to create the Grid.  Each grid element has a GridSlotController component and the InventoryGridController has a reference to the whole grid so it can do things like find first open slot.

There's no specific equip controller yet that aggregates things, but the EquipDataController does have a Dictionary of all the types of equipments you could have as well as what is currently equipped so that seemed less necessary.

## Item API
Items are implemented similarly with both some kind of Data class as well as a Controller class for behaviour.
The Inventory/WorldItemController classes control how these things behave in their respective environments.  Note that things like the selectedItem (currently on the cursor) are static as there will only ever be one.

All Item classes will inherit from the ItemData class which in turn inherits from ScriptableObject.  This way, we can crank out new items without storing in code or having a large file.  So instead of a big db, we just have a shitload of assets.  Yay.
To create a new item prefab, really the only thing that needs to be done is add the ItemData class to the ItemController component as well as the counterpart prefab.  Once you've done that, you're good to go.
  



