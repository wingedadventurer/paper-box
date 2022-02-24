using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake : MonoBehaviour
{
    [SerializeField] private DataItem dataItemCandle;
    [SerializeField] private DataItem dataItemLighter;
    [SerializeField] private GameObject[] goCandles;
    [SerializeField] private GameObject[] goFires;
    [SerializeField] private Interactable[] iCandleSlots;
    [SerializeField] private Interactable[] iCandles;
    [SerializeField] private GameObject goCakeModel;
    [SerializeField] private GameObject goItemCherry;
    [SerializeField] private float dInflating;
    [SerializeField] private float scaleInflateMax;

    private int countLitCandles;
    private Inventory inventory;

    private bool inflating;
    private float tInflating;

    private void Start()
    {
        inventory = Inventory.instance;

        for (int i = 0; i < 6; i++)
        {
            goCandles[i].SetActive(false);
        }
        goItemCherry.SetActive(false);
    }

    private void Update()
    {
        if (inflating)
        {
            tInflating += Time.deltaTime;
            goCakeModel.transform.localScale = Vector3.one * Mathf.Lerp(1, scaleInflateMax, tInflating/dInflating);
            if (tInflating >= dInflating)
            {
                inflating = false;

                // explode the cake
                goCakeModel.SetActive(false);
                goItemCherry.SetActive(true);

                foreach (GameObject g in goFires)
                {
                    g.SetActive(false);
                }
            }
        }
    }

    public void OnCandleSlotInteracted(int index)
    {
        if (inventory.GetEquippedItem() == dataItemCandle)
        {
            inventory.ConsumeEquippedItem();
            goCandles[index].SetActive(true);
            iCandleSlots[index].gameObject.SetActive(false);

            AudioManager.instance.PlaySFX(AudioManager.instance.sfxPut);
        }
    }

    public void OnCandleInteracted(int index)
    {
        if (inventory.GetEquippedItem() == dataItemLighter)
        {
            iCandles[index].gameObject.SetActive(false);
            goFires[index].GetComponent<ParticleSystem>().Play();
            countLitCandles++;

            if (countLitCandles == 6)
            {
                inflating = true;
            }

            AudioManager.instance.PlaySFX(AudioManager.instance.sfxIgnite).volume = 0.6f;
        }
    }
}
