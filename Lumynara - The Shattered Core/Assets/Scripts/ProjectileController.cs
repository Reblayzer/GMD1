using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private float life = 3;

    void Awake()
    {
        Destroy(gameObject, life);
    }

    void Update()
    {
        transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collistion)
    {
        Destroy(collistion.gameObject);
        Destroy(gameObject);
    }
 
}