using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform background, handle;
    public float handleLimit = 0.6f;
    private Vector2 inputVector;
    private int _fingerId = -1;

    public Vector2 Direction => inputVector;
    public int FingerId => _fingerId;
    public bool IsActive => _fingerId != -1;

    public void OnPointerDown(PointerEventData eventData)
    {
        _fingerId = eventData.pointerId;
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerId != _fingerId) return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(background, eventData.position, eventData.pressEventCamera, out var pos);
        pos = Vector2.ClampMagnitude(pos, background.sizeDelta.x * 0.5f * handleLimit);
        handle.anchoredPosition = pos;
        inputVector = pos / (background.sizeDelta.x * 0.5f * handleLimit);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId == _fingerId)
        {
            _fingerId = -1;
            inputVector = Vector2.zero;
            handle.anchoredPosition = Vector2.zero;
        }
    }
}