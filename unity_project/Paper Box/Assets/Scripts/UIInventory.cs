using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [Header("Ref")]
    public GameObject prefabUIItem;
    public GameObject goItemGrid;
    public Text textItemName;

    [Header("Temp")]
    public DataItem dataItemTest;

    private Game game;

    private void Start()
    {
        game = FindObjectOfType<Game>();
        textItemName.text = "";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            AddItem(dataItemTest);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            DeleteLastItem();
        }
    }

    public void AddItem(DataItem data)
    {
        GameObject go = Instantiate(prefabUIItem);
        go.transform.SetParent(goItemGrid.transform);
        UIItem uiItem = go.GetComponent<UIItem>();
        uiItem.uiInventory = this;
        uiItem.SetDataItem(data);
        UpdateItemGridSize();
    }

    public void DeleteItem(DataItem data)
    {
        for (int i = 0; i < goItemGrid.transform.childCount; i++)
        {
            UIItem uiItem = goItemGrid.transform.GetChild(i).GetComponent<UIItem>();
            if (uiItem.GetDataItem() == data)
            {
                Destroy(uiItem.gameObject);
                break;
            }
        }
    }

    private void DeleteLastItem()
    {
        if (goItemGrid.transform.childCount > 0)
        {
            Destroy(goItemGrid.transform.GetChild(goItemGrid.transform.childCount - 1).gameObject);
            UpdateItemGridSize();
        }
    }

    private void UpdateItemGridSize()
    {
        //goItemGrid.GetComponent<GridLayoutGroup>().constraintCount = (int)(Mathf.Sqrt(goItemGrid.transform.childCount));
        //goItemGrid.GetComponent<GridLayoutGroup>().constraintCount = goItemGrid.transform.childCount / 8;
    }

    public void OnUIItemClick(DataItem dataItem)
    {
        if (game)
        {
            textItemName.text = "";
            game.OnInventoryItemSelected(dataItem);
        }
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
