using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HandleSelectable : Selectable,
                                ISelectHandler,
                                IDeselectHandler,
                                IMoveHandler
{
    private Slider _slider;
    private bool _locked;

    protected override void Awake()
    {
        base.Awake();
        _slider = GetComponentInParent<Slider>();
    }

    public override void OnSelect(BaseEventData eventData)
    {
        if (!IsInteractable() || _locked) return;
        DoStateTransition(SelectionState.Highlighted, false);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        if (!IsInteractable() || _locked) return;
        DoStateTransition(SelectionState.Normal, false);
    }

    public override void OnMove(AxisEventData eventData)
    {
        if (_slider == null || _locked)
        {
            // if locked, swallow only L/R
            if (_locked &&
                (eventData.moveDir == MoveDirection.Left ||
                 eventData.moveDir == MoveDirection.Right))
            {
                eventData.Use();
                return;
            }

            base.OnMove(eventData);
            return;
        }

        // normal slider left/right:
        float step = _slider.wholeNumbers
                     ? 1f
                     : _slider.maxValue / 20f;

        if (eventData.moveDir == MoveDirection.Left)
        {
            _slider.value = Mathf.Max(_slider.minValue,
                                      _slider.value - step);
            eventData.Use();
        }
        else if (eventData.moveDir == MoveDirection.Right)
        {
            _slider.value = Mathf.Min(_slider.maxValue,
                                      _slider.value + step);
            eventData.Use();
        }
        else
        {
            base.OnMove(eventData);
        }
    }

    public void SetLocked(bool locked)
    {
        _locked = locked;
        RefreshState();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        RefreshState();
    }

    private void RefreshState()
    {
        if (!IsInteractable() || _locked)
            DoStateTransition(SelectionState.Disabled, true);
        else
            DoStateTransition(SelectionState.Normal, true);
    }
}
