using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject prefabUIItem;
    public GameObject goContainer;
    public Text textItemName;
    public DataItem dataItemEmpty;
    public List<DataItem> initialItems = new List<DataItem>();
    [SerializeField] private UIItem uiItemEquipped;

    private Game game;

    public UnityEvent Equipped, Unequipped;

    public static Inventory instance;

    private void Awake()
    {
        instance = this;

        textItemName.text = "";

        uiItemEquipped.gameObject.SetActive(false);
    }

    private void Start()
    {
        game = Game.instance;

        foreach (DataItem data in initialItems)
        {
            AddItem(data);
        }
    }

    public void AddItem(DataItem data, int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            // add to current one if it exists
            foreach (UIItem uiItem in goContainer.transform.GetComponentsInChildren<UIItem>())
            {
                if (uiItem.GetData() == data)
                {
                    uiItem.AddOne();
                    return;
                }
            }

            // otherwise add new one
            GameObject go = Instantiate(prefabUIItem);
            go.transform.SetParent(goContainer.transform);
            UIItem uiItem2 = go.GetComponent<UIItem>();
            uiItem2.SetData(data);
            uiItem2.AddOne();

            SortItems();
        }
    }

    public bool DeleteItem(DataItem data)
    {
        foreach (UIItem uiItem in goContainer.transform.GetComponentsInChildren<UIItem>())
        {
            if (uiItem.GetData() == data)
            {
                // remove one
                uiItem.RemoveOne();

                // delete if no amount left
                if (uiItem.GetAmount() == 0)
                {
                    Destroy(uiItem.gameObject);
                    return true;
                }
                return false;
            }
        }

        return true;
    }

    public DataItem GetEquippedItem()
    {
        return uiItemEquipped.GetData();
    }

    public bool HasEquippedItem(DataItem data)
    {
        return uiItemEquipped.GetData() == data;
    }

    public void ConsumeEquippedItem()
    {
        if (DeleteItem(uiItemEquipped.GetData()))
        {
            ClearEquippedItem();
        }
        else
        {
            uiItemEquipped.RemoveOne();
        }
    }

    public void EquipItem(DataItem data)
    {
        uiItemEquipped.gameObject.SetActive(true);
        uiItemEquipped.SetData(data);
        uiItemEquipped.SetAmount(GetItemCount(data));
        Equipped.Invoke();
    }

    public void ClearEquippedItem()
    {
        uiItemEquipped.SetData(null);
        uiItemEquipped.gameObject.SetActive(false);
        Unequipped.Invoke();
    }

    private void UpdateItemGridSize()
    {
        //goItemGrid.GetComponent<GridLayoutGroup>().constraintCount = (int)(Mathf.Sqrt(goItemGrid.transform.childCount));
        //goItemGrid.GetComponent<GridLayoutGroup>().constraintCount = goItemGrid.transform.childCount / 8;
    }

    public void OnUIItemClick(DataItem data)
    {
        textItemName.text = "";
        EquipItem(data);
        game.OnInventoryItemSelected(data);
    }

    public void OnUIItemPointerEnter(DataItem dataItem)
    {
        textItemName.text = dataItem.name;
    }

    public void OnUIItemPointerExit()
    {
        textItemName.text = "";
    }

    public int GetItemCount(DataItem data)
    {
        foreach (UIItem uiItem in goContainer.transform.GetComponentsInChildren<UIItem>())
        {
            if (uiItem.GetData() == data)
            {

                return uiItem.GetAmount();
            }
        }

        return 0;
    }

    private void SortItems()
    {
        List<UIItem> uiItems = new List<UIItem>(goContainer.GetComponentsInChildren<UIItem>());
        uiItems.Sort(SortItemsByDisplayName);

        for (int i = 0; i < uiItems.Count; i++)
        {
            uiItems[i].transform.SetSiblingIndex(i);
        }
    }

    // custom sorter for items
    static int SortItemsByDisplayName(UIItem a, UIItem b)
    {
        return a.GetData().sortName.CompareTo(b.GetData().sortName);
    }
}
