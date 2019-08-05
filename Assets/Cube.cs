using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Cube : Entity
{
    public InventoryDataController myInventoryData;
    private void Awake()
    {
        speed = 5f;
    }

    // Pickups in world will be triggers or you'll click on them or something else to pick them up... At that time, call myInventoryData.PickUpWorldItem and pass the collision object
    // Can do this in a separate collision handling script, but should check result of pickup world item before doing anything else.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"DING DING DING");
        myInventoryData.PickUpWorldItem(collision.gameObject.GetComponent<WorldItemController>().ConvertToInventoryItem());
        Destroy(collision.gameObject);
    }

}
