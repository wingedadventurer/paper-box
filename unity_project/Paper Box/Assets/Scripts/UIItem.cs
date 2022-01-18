using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Text textAmount;

    private DataItem data;
    private int amount;

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
        if (data)
        {
            image.enabled = true;
            image.sprite = data.sprite;
        }
        else
        {
            image.enabled = false;
        }
    }

    public DataItem GetData()
    {
        return data;
    }

    public void SetAmount(int n)
    {
        amount = n;
        textAmount.text = amount > 1 ? amount.ToString() : "";
    }

    public void AddOne()
    {
        amount++;
        textAmount.text = amount > 1 ? amount.ToString() : "";
    }

    public void RemoveOne()
    {
        amount--;
        if (amount < 0) { amount = 0; }
        textAmount.text = amount > 1 ? amount.ToString() : "";
    }

    public int GetAmount()
    {
        return amount;
    }
}
