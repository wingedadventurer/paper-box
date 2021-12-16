using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelInventory : MonoBehaviour
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
            AddItem();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            DeleteItem();
        }
    }

    private void AddItem()
    {
        GameObject go = Instantiate(prefabUIItem);
        go.transform.SetParent(goItemGrid.transform);
        UIItem uiItem = go.GetComponent<UIItem>();
        uiItem.panelInventory = this;
        uiItem.SetDataItem(dataItemTest);
        UpdateItemGridSize();
    }

    private void DeleteItem()
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
            game.CloseInventory();
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
