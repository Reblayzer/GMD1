using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform player;

    private NavMeshAgent nav;
    private NavMeshPath path;
    private Vector3 homePosition;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        homePosition = transform.position;       // remember where we started
    }

    void Update()
    {
        if (player == null)
            return;

        // 1) compute a path from us to the player
        nav.CalculatePath(player.position, path);

        // 2) if it's fully traversable on our (platform-only) NavMesh, chase
        if (path.status == NavMeshPathStatus.PathComplete)
        {
            nav.SetDestination(player.position);
        }
        else
        {
            // 3) otherwise go back “home”
            // if we’re already at home, you can optionally call nav.ResetPath() here
            nav.SetDestination(homePosition);
        }
    }
}
