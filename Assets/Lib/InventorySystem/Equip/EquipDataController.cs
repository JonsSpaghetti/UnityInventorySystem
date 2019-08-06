using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EquipDataController : MonoBehaviour
{
    // Make sure to pass an instance of this class to the playercharacter somehow. They'll need to call the AddItem method in order to actually interact with this.
    public List<GameObject> debugEquipList = new List<GameObject>();

    private Dictionary<EquipSlotType, GameObject> equips = new Dictionary<EquipSlotType, GameObject>();

    public void AddItemToSlot(EquipSlotType slot, GameObject item)
    {
        equips[slot] = item;
    }

    public void RemoveItemFromSlot(EquipSlotType slot, GameObject item)
    {
        equips[slot] = null;
    }
}
