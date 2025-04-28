using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ToggleSelectable : Toggle, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    private bool isHovered = false;

    protected override void Awake()
    {
        base.Awake();
        onValueChanged.AddListener(OnToggleChanged);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        onValueChanged.RemoveListener(OnToggleChanged);
    }

    private void OnToggleChanged(bool isOn)
    {
        UpdateVisualState();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        DoStateTransition(SelectionState.Pressed, false);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        isHovered = false;
        UpdateVisualState();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        isHovered = true;
        DoStateTransition(SelectionState.Highlighted, false);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        isHovered = false;
        UpdateVisualState();
    }

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        if (state == SelectionState.Normal || state == SelectionState.Selected)
        {
            if (isOn)
            {
                base.DoStateTransition(SelectionState.Normal, instant);
            }
            else
            {
                base.DoStateTransition(SelectionState.Pressed, instant);
            }
        }
        else
        {
            base.DoStateTransition(state, instant);
        }
    }

    private void UpdateVisualState()
    {
        if (!IsInteractable())
        {
            base.DoStateTransition(SelectionState.Disabled, false);
            return;
        }

        if (isHovered)
        {
            base.DoStateTransition(SelectionState.Highlighted, false);
        }
        else
        {
            if (isOn)
            {
                base.DoStateTransition(SelectionState.Normal, false);
            }
            else
            {
                base.DoStateTransition(SelectionState.Pressed, false);
            }
        }
    }
}
