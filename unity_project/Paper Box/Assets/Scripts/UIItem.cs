using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    [HideInInspector] public UIInventory uiInventory;

    [Header("Ref")]
    public Image image;

    private DataItem dataItem;

    public void OnClick()
    {
        uiInventory.OnUIItemClick(dataItem);
    }

    public void OnPointerEnter()
    {
        uiInventory.OnUIItemPointerEnter(dataItem);
    }

    public void OnPointerExit()
    {
        uiInventory.OnUIItemPointerExit();
    }

    public void SetDataItem(DataItem dataItem2)
    {
        dataItem = dataItem2;
        image.sprite = dataItem.sprite;
        image.enabled = true;
    }
}
