using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject prefabUIItem;
    public GameObject goContainer;
    public Text textItemName;
    public DataItem dataItemEmpty;
    public List<DataItem> initialItems = new List<DataItem>();

    private DataItem dataItemEquipped;
    [SerializeField] private Image imageEquippedItem;

    private Game game;

    public static Inventory instance;

    private void Awake()
    {
        instance = this;

        textItemName.text = "";

        imageEquippedItem.enabled = false;
    }

    private void Start()
    {
        game = Game.instance;

        foreach (DataItem data in initialItems)
        {
            AddItem(data);
        }
    }

    public void AddItem(DataItem data)
    {
        GameObject go = Instantiate(prefabUIItem);
        go.transform.SetParent(goContainer.transform);
        UIItem uiItem = go.GetComponent<UIItem>();
        uiItem.SetData(data);
        UpdateItemGridSize();
    }

    public void DeleteItem(DataItem data)
    {
        foreach (UIItem uiItem in goContainer.transform.GetComponentsInChildren<UIItem>())
        {
            if (uiItem.GetData() == data)
            {
                Destroy(uiItem.gameObject);
                return;
            }
        }
    }

    public DataItem GetEquippedItem()
    {
        return dataItemEquipped;
    }

    public void ConsumeEquippedItem()
    {
        Inventory.instance.DeleteItem(dataItemEquipped);
        ClearEquippedItem();
    }

    public void EquipItem(DataItem data)
    {
        dataItemEquipped = data;
        imageEquippedItem.sprite = data.sprite;
        imageEquippedItem.enabled = true;
    }

    public void ClearEquippedItem()
    {
        dataItemEquipped = null;
        imageEquippedItem.sprite = null;
        imageEquippedItem.enabled = false;
        //crosshair.SetEquipped(false); // TODO: use events
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
}
