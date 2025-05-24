using UnityEngine;

[RequireComponent(typeof(Collider))]
public class KeyPickup : MonoBehaviour
{
  [SerializeField] private string playerTag = "Player";
  [SerializeField] private GameObject objectToShow;
  [SerializeField] private bool destroyKey = true;

  void Awake()
  {
    // make sure the key's collider is set as a trigger
    var col = GetComponent<Collider>();
    col.isTrigger = true;
  }

  void OnTriggerEnter(Collider other)
  {
    if (!other.CompareTag(playerTag)) return;

    // show the bridge (or whatever)
    if (objectToShow != null)
      objectToShow.SetActive(true);

    // hide or destroy this key
    if (destroyKey)
      Destroy(gameObject);
    else
      gameObject.SetActive(false);
  }
}
