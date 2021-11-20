using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    internal event System.EventHandler<Vector3> OnEndDragAlly;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 initialAnchoredPosition;
    private Canvas canvas;

    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        initialAnchoredPosition = rectTransform.anchoredPosition;
        canvasGroup.alpha = .7f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        OnEndDragAlly?.Invoke(this, transform.position);
        SendImageBackToInitialPosition();
    }

    private void SendImageBackToInitialPosition()
    {
        rectTransform.anchoredPosition = initialAnchoredPosition;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position, new Vector2(.5f, .5f));
    }
}
