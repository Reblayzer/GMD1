using System.Collections;
using UnityEngine;

public class FinalCameraTrigger : MonoBehaviour
{
  public Transform orbo;
  public CameraController cameraController;
  public DialogueController dialogueController;

  public MonoBehaviour orboMovementScript;
  public Vector3 finalCameraPosition = new Vector3(-11.98f, 5f, -13.28f);
  public Vector3 finalCameraRotation = new Vector3(1f, 0f, 0f);
  public float transitionDuration = 3f;

  private bool triggered = false;

  private void OnTriggerEnter(Collider other)
  {
    if (triggered || other.transform != orbo) return;

    triggered = true;

    // Lock movement
    if (orboMovementScript != null)
      orboMovementScript.enabled = false;

    // Lock physics
    Rigidbody rb = orbo.GetComponent<Rigidbody>();
    if (rb != null)
    {
      rb.linearVelocity = Vector3.zero;
      rb.useGravity = false;
      rb.isKinematic = true;
    }

    // Move camera
    cameraController.StartCameraTransition(finalCameraPosition, finalCameraRotation, transitionDuration);

    StartCoroutine(DelayedShardTransfer());

    IEnumerator DelayedShardTransfer()
    {
      yield return new WaitForSeconds(transitionDuration + 0.5f);

      CollisionManager cm = FindFirstObjectByType<CollisionManager>();
      if (cm != null && dialogueController != null)
        dialogueController.StartDialogueSequence(cm, 0.05f);
    }
  }
}
