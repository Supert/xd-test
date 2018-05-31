using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    private RectTransform rect;

    private const float MOVE_SPEED = 500f;

    public Vector2 TargetPosition { get; set; }

    public event Action OnClick;

    public void Initialize(int index, Vector2 initialPosition)
    {
        rect = GetComponent<RectTransform>();

        Text text = transform.GetChild(0).GetComponent<Text>();
        text.text = (index + 1).ToString();
        TargetPosition = initialPosition;
        rect.anchoredPosition = TargetPosition;
        gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClick != null)
            OnClick();
    }

    public void Update()
    {
        if (rect.anchoredPosition != TargetPosition)
        {
            rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition, TargetPosition, Time.deltaTime * MOVE_SPEED);
        }
    }
}
