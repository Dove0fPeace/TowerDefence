using System;
using _Imported;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickProtection : SingletonBase<ClickProtection>, IPointerClickHandler
{
    private Image _blocker;
    private Action<Vector2> _onClickAction;

    private void Start()
    {
        _blocker = GetComponent<Image>();
    }

    public void Activate(Action<Vector2> mouseAction)
    {
        _blocker.enabled = true;
        _onClickAction = mouseAction;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _blocker.enabled = false;
        _onClickAction(eventData.pressPosition);
        _onClickAction = null;
    }
}
