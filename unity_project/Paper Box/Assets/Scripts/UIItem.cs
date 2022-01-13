using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    [SerializeField] private Image image;
    private DataItem data;

    public void OnClick()
    {
        Inventory.instance.OnUIItemClick(data);
    }

    public void OnPointerEnter()
    {
        Inventory.instance.OnUIItemPointerEnter(data);
    }

    public void OnPointerExit()
    {
        Inventory.instance.OnUIItemPointerExit();
    }

    public void SetData(DataItem d)
    {
        data = d;
        image.sprite = data.sprite;
        image.enabled = true;
    }

    public DataItem GetData()
    {
        return data;
    }
}
