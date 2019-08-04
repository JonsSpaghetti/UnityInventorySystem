using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Controller for slots
public class SlotController : MonoBehaviour, IPointerEnterHandler
{
    public InventoryGridController gridController;
    public Vector2Int gridPos;
    public Text text;

    public GameObject storedItemObject;
    //public ItemClass storedItemClass;
    public bool isOccupied;

    private void Awake()
    {
        gridController = FindObjectOfType<InventoryGridController>();
    }

    private void Start()
    {
        text.text = gridPos.x + "," + gridPos.y;
    }

    // When pointer enters, update the InventoryGridController with the highlighted slot.
    public void OnPointerEnter(PointerEventData eventData)
    {
        gridController.SetHighlightedSlot(this.gameObject);
    }

    public GameObject InsertItem(GameObject item)
    {
        storedItemObject = item;
        isOccupied = true;
        return storedItemObject;
    }
}

public struct SlotColorHighlights
{
    public static Color32 Green
    { get { return new Color32(127, 223, 127, 255); } }
    public static Color32 Yellow
    { get { return new Color32(223, 223, 63, 255); } }
    public static Color32 Red
    { get { return new Color32(223, 127, 127, 255); } }
    public static Color32 Blue
    { get { return new Color32(159, 159, 223, 255); } }
    public static Color32 Blue2
    { get { return new Color32(191, 191, 223, 255); } }
    public static Color32 Gray
    { get { return new Color32(223, 223, 223, 255); } }
}

