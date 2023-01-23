using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector2 cameraLocalPos;

    void Update()
    {
        transform.position = new Vector3(player.position.x, 0, -10);
    }
}