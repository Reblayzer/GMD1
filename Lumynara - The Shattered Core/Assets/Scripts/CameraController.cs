using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset = new Vector3(-10, 10, -10); // Isometric 45° offset
    [SerializeField] private Vector3 rotationEuler = new Vector3(30f, 45f, 0f); // Top-down diagonal look

    private void Start()
    {
        if (player != null)
        {
            transform.rotation = Quaternion.Euler(rotationEuler);
        }
    }

    private void LateUpdate()
    {
        if (player == null) return;

        transform.position = player.position + offset;
    }
}
