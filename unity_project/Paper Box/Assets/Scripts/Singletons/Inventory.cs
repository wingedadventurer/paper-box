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

    private Game game;

    public static Inventory instance;

    private void Awake()
    {
        instance = this;

        textItemName.text = "";
    }

    private void Start()
    {
        game = Game.instance;
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

    private void UpdateItemGridSize()
    {
        //goItemGrid.GetComponent<GridLayoutGroup>().constraintCount = (int)(Mathf.Sqrt(goItemGrid.transform.childCount));
        //goItemGrid.GetComponent<GridLayoutGroup>().constraintCount = goItemGrid.transform.childCount / 8;
    }

    public void OnUIItemClick(DataItem dataItem)
    {
        textItemName.text = "";
        game.OnInventoryItemSelected(dataItem);
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
