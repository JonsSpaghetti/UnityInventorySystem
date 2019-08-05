using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class ToggleInventory : Command
{
    GameObject inventoryUI;
    public ToggleInventory(Entity entity) : base(entity)
    {
        inventoryUI = GameObject.FindGameObjectWithTag("InventoryUI");
    }

    public override void Execute()
    {
        bool active = inventoryUI.activeInHierarchy;
        inventoryUI.SetActive(!active);
    }
}
