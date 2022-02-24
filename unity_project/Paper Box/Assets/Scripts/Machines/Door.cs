using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Door : MonoBehaviour
{
    [Header("Setup")]
    public GameObject prefabKey;

    [Header("Colors")]
    [SerializeField] private Material[] matColors = new Material[6];
    [SerializeField] private MeshRenderer mrLock;

    [Header("Ref")]
    public Animation anim;
    public Interactable inLock;
    public GameObject goKeyPos;

    private DataItem dataKey;
    private GameObject goKey;

    private void Start()
    {
        if (prefabKey)
        {
            dataKey = prefabKey.GetComponent<Item>().data;
            inLock.requestedItems.Add(dataKey);

            Material[] mats = mrLock.sharedMaterials;
            //int n = int.Parse(prefabKey.name.Where(char.IsDigit).ToArray();
            int n = int.Parse(string.Join("", prefabKey.name.ToCharArray().Where(char.IsDigit))) - 1;
            mats[2] = matColors[n / 5];
            mrLock.sharedMaterials = mats;
        }
    }

    public void Open()
    {
        Inventory.instance.ConsumeEquippedItem();
        inLock.gameObject.SetActive(false);
        anim.Play();

        AudioManager.instance.PlaySFX(AudioManager.instance.sfxKeyInsert);

        Invoke("TurnKeySFX", 0.8f);
        Invoke("PlaceEnd", 1.75f);

        // spawn and add key
        goKey = Instantiate(prefabKey);
        goKey.transform.SetParent(goKeyPos.transform, true);
        goKey.transform.localPosition = Vector3.zero;
        goKey.transform.localEulerAngles = new Vector3(0, 0, -90);
        goKey.GetComponent<Item>().SetInteractable(false);
    }

    private void TurnKeySFX()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.sfxKeyTurn);
    }

    private void PlaceEnd()
    {
        // make key takeable again
        goKey.GetComponent<Item>().SetInteractable(true);
    }
}
