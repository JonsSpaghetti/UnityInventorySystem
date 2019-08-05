using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour, IPointerClickHandler
{
    public Sprite myIcon;
    public string myName;
    public ItemData myItem;
    public GameObject worldItemPrefab;

    private static GameObject selectedItem;
    public static bool isDragging = false;

    private float slotSize;
    Image myImage;


    Vector2Int gridPos;

    private void Awake()
    {
        myIcon = myItem.myInventoryIcon;
        myName = myItem.myName;
        myImage = GetComponent<Image>();
    }

    private void Start()
    {
        slotSize = InventoryGridCreator.Singleton.slotSize;
        myImage.sprite = myIcon;
        SetUpItemObject(myItem);
        //TODO - implement myItem.DisplayTooltip();

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("POINTER CLICK");
        CanvasGroup canvas = GetComponent<CanvasGroup>();
        canvas.blocksRaycasts = false;
        canvas.alpha = 0.5f;
        isDragging = true;
        

        SetSelectedItem(eventData.pointerCurrentRaycast.gameObject);
    }

    private void Update()
    {
        if (isDragging)
        {
            // Have to be very specific to use selectedItem, otherwise since the isDragging var is static, it will get ALL items on screen
            selectedItem.transform.position = Input.mousePosition;
        }
    }

    public void SetUpItemObject(ItemData item)
    {
        RectTransform rect = GetComponent<RectTransform>();
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);
    }

    public static void SetSelectedItem(GameObject obj)
    {
        selectedItem = obj;
        isDragging = true;
        // To avoid weird UI sorting layer issues, set dragparent when dragging.
        obj.transform.SetParent(GameObject.FindGameObjectWithTag("DragParent").transform);
        obj.GetComponent<RectTransform>().localScale = Vector3.one;
    }

    public static GameObject GetSelectedItem()
    {
        return selectedItem;
    }

    public static void ResetSelectedItem()
    {
        selectedItem = null;
        isDragging = false;
    }

    public void SpawnWorldItem(Vector3 location)
    {
        Instantiate(worldItemPrefab, location, new Quaternion());
    }

    public void DropItem(GameObject itemOwner)
    {
        // TODO - do this off a separate transform so that item owner drops in a specific location instead of right on top of themselves.
        SpawnWorldItem(itemOwner.transform.position);
        ResetSelectedItem();
        Destroy(gameObject);
    }
}
