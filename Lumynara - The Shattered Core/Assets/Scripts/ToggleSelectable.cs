using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Toggle))]
public class ToggleSelectable : Toggle,
                                ISelectHandler,
                                IDeselectHandler
{
    protected override void Awake()
    {
        base.Awake();
        // Re-apply correct tint every time the value changes
        onValueChanged.AddListener(_ => UpdateVisualState(false));
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        // Ensure correct tint at startup
        UpdateVisualState(true);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        // Show Highlighted when focused
        DoStateTransition(SelectionState.Highlighted, false);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        // Revert back to Normal/Selected
        UpdateVisualState(false);
    }

    // Applies Normal / Selected / Disabled tints
    // depending on isOn & interactable.
    private void UpdateVisualState(bool instant)
    {
        if (!IsInteractable())
            DoStateTransition(SelectionState.Disabled, instant);
        else if (isOn)
            DoStateTransition(SelectionState.Selected, instant);
        else
            DoStateTransition(SelectionState.Normal, instant);
    }
}
