using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class Panel : MonoBehaviour
{
    public bool visible;

    [SerializeField] private float fadeSpeed = 15.0f;

    private CanvasGroup cg;

    private void Awake()
    {
        cg = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        gameObject.SetActive(true);
    }

    private void Update()
    {
        cg.alpha = Mathf.MoveTowards(cg.alpha, visible ? 1 : 0, fadeSpeed * Time.unscaledDeltaTime);
    }

    public void SetVisible(bool value)
    {
        visible = value;
        cg.interactable = visible;
        cg.blocksRaycasts = visible;
    }

    public void SetVisible(bool value, bool fast = false)
    {
        visible = value;
        if (fast)
        {
            cg.alpha = visible ? 1 : 0;
        }
        cg.interactable = visible;
        cg.blocksRaycasts = visible;
    }
}
