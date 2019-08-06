using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class EquipSlotData : ScriptableObject
{
    public EquipSlotType slotType; // What type of slot will this be?
    public ItemType allowedItemType; // What items can go in said slot?

    public Vector2Int slotSize; // what size to scale image to? Should we even do this?


}

