using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HandleSelectable : Selectable, IPointerDownHandler, IPointerUpHandler
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!IsInteractable()) return;

        base.OnPointerDown(eventData);
        DoStateTransition(SelectionState.Pressed, false);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (!IsInteractable()) return;

        base.OnPointerUp(eventData);
        DoStateTransition(SelectionState.Normal, false);

        if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject == gameObject)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void RefreshState()
    {
        if (!IsInteractable())
        {
            DoStateTransition(SelectionState.Disabled, true);
        }
        else
        {
            DoStateTransition(SelectionState.Normal, true);
        }
    }
}
