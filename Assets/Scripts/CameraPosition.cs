using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{

    [SerializeField] private Transform player;

    void Start() {}

    void Update()
    {
        transform.position = new Vector3(player.position.x, 0, -10);
    }
}