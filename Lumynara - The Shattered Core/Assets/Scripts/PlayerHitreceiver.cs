using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerHitReceiver : MonoBehaviour
{
  [Tooltip("Drag in your GameUIManager here")]
  [SerializeField] private LevelUIManager uiManager;

  [Tooltip("Must match your projectile prefab’s tag")]
  [SerializeField] private string projectileTag = "Projectile";
  [SerializeField] private string enemyTag = "Enemy";

  void OnCollisionEnter(Collision collision)
  {
    if (collision.collider.CompareTag(projectileTag) || collision.collider.CompareTag(enemyTag))
    {
      uiManager.ShowHit();
      Destroy(gameObject);
    }
  }
}
