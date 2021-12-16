using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    [HideInInspector] public PanelInventory panelInventory;

    [Header("Ref")]
    public Image image;

    private DataItem dataItem;

    public void OnClick()
    {
        panelInventory.OnUIItemClick(dataItem);
    }

    public void OnPointerEnter()
    {
        panelInventory.OnUIItemPointerEnter(dataItem);
    }

    public void OnPointerExit()
    {
        panelInventory.OnUIItemPointerExit();
    }

    public void SetDataItem(DataItem dataItem2)
    {
        dataItem = dataItem2;
        image.sprite = dataItem.sprite;
        image.enabled = true;
    }
}
