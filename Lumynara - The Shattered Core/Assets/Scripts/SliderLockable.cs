using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderLockable : Slider
{
  public bool isLocked = false;

  public new void Update()
  {
    // Block slider updates via controller input when selected
    if (isLocked &&
        EventSystem.current != null &&
        EventSystem.current.currentSelectedGameObject == gameObject)
    {
      // Cancel navigation by resetting axis
      // This works if using Unity's new input system
      AxisEventData dummy = new AxisEventData(EventSystem.current);
      dummy.moveDir = MoveDirection.None;
      EventSystem.current.SetSelectedGameObject(null);
      EventSystem.current.SetSelectedGameObject(gameObject); // Re-set to re-highlight
    }
  }

  public override void OnMove(AxisEventData eventData)
  {
    if (isLocked)
    {
      if (eventData.moveDir == MoveDirection.Left || eventData.moveDir == MoveDirection.Right)
      {
        eventData.Use();
        return;
      }
    }

    base.OnMove(eventData);
  }

  public override void OnSelect(BaseEventData eventData)
  {
    base.OnSelect(eventData);
    DoStateTransition(SelectionState.Highlighted, false);
  }

  public override void OnDeselect(BaseEventData eventData)
  {
    base.OnDeselect(eventData);
    DoStateTransition(SelectionState.Normal, false);
  }
}
