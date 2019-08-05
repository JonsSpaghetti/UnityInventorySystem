using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WorldItemController : MonoBehaviour
{
    public Sprite myIcon;
    public string myName;
    public ItemData myItem;
    public GameObject inventoryItemPrefab;
    private SpriteRenderer mySpriteRenderer;

    private void Awake()
    {
        myIcon = myItem.myWorldSprite;
        myName = myItem.myName;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        SetUpItemObject(myItem);
        //TODO - implement myItem.DisplayTooltip();

    }
 
    public void SetUpItemObject(ItemData item)
    {
        mySpriteRenderer.sprite = myItem.myWorldSprite;
    }

    public GameObject ConvertToInventoryItem()
    {
        return Instantiate(inventoryItemPrefab);
    }

}
