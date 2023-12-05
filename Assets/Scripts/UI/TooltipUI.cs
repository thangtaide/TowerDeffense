using LTA.DesignPattern;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance;
    [SerializeField] RectTransform canvasRectTransform;
    RectTransform rectTransform;
    TextMeshProUGUI textMeshPro;
    RectTransform backgroundRectTransform;
    TooltipTimer tooltipTimer = null;
    private void Awake()
    {
        Instance = this;
        rectTransform = GetComponent<RectTransform>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        Hide();
    }
    private void Update()
    {
        HandleFollowMouse();
        if(tooltipTimer != null )
        {
            tooltipTimer.timer -= Time.deltaTime;
            if(tooltipTimer.timer < 0)
            {
                tooltipTimer = null;
                Hide();
            }
        }
    }
    void HandleFollowMouse()
    {
        Vector2 anchorPosition = Input.mousePosition * canvasRectTransform.localScale.x;
        if (anchorPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            anchorPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }
        if (anchorPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            anchorPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }

        rectTransform.anchoredPosition = anchorPosition;
    }
    private void SetText(string text)
    {
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate(true);

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(8, 8);
        backgroundRectTransform.sizeDelta = padding+textSize;
    }
    public void Show(string text, TooltipTimer tooltipTimer = null)
    {
        HandleFollowMouse();
            this.tooltipTimer = tooltipTimer;
        SetText(text);
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public class TooltipTimer
    {
        public float timer;
    }
}
