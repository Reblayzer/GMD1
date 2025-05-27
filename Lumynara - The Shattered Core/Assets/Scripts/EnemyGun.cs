using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class EnemyGun : MonoBehaviour
{
    [Header("Gun Settings")]
    public Transform projectileSpawnPoint;
    public GameObject projectilePrefab;
    public float projectileSpeed = 3f;
    public float fireInterval = 1f;

    [Header("Audio")]
    public AudioClip shootSound;
    private AudioSource _audioSource;

    [Header("Detection")]
    public string playerTag = "Player";

    // internal state
    private Transform target;
    private Coroutine firingRoutine;

    private void Awake()
    {
        // Create or cache an AudioSource
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
            _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.playOnAwake = false;
    }

    void OnTriggerEnter(Collider other)
    {
        // earlyexit all non-Player hits
        if (!other.CompareTag(playerTag))
            return;

        Debug.Log($"EnemyGun saw player: {other.name}", this);
        target = other.transform;
        if (firingRoutine == null)
            firingRoutine = StartCoroutine(FireLoop());
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            // stop firing
            if (firingRoutine != null)
            {
                StopCoroutine(firingRoutine);
                firingRoutine = null;
            }
            target = null;
        }
    }

    IEnumerator FireLoop()
    {
        while (true)
        {
            FireProjectile();
            yield return new WaitForSeconds(fireInterval);
        }
    }

    void FireProjectile()
    {
        if (projectilePrefab == null || projectileSpawnPoint == null) return;

        // Spawn projectile
        var proj = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        if (proj.TryGetComponent<Rigidbody>(out var rb))
            rb.linearVelocity = projectileSpawnPoint.forward * projectileSpeed;

        // Play shoot SFX
        if (shootSound != null)
            _audioSource.PlayOneShot(shootSound);
    }

    void Update()
    {
        // if we have a target, face them smoothly on the Y axis
        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            dir.y = 0; // keep only horizontal
            if (dir.sqrMagnitude > 0.001f)
            {
                Quaternion want = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    want,
                    Time.deltaTime * 5f   // tweak “5f” for faster/slower turning
                );
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        var bc = GetComponent<BoxCollider>();
        if (bc != null && bc.isTrigger)
        {
            Gizmos.color = Color.yellow;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(bc.center, bc.size);
        }
    }
}
