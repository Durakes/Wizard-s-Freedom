using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform player;
    void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;

    }

    void Update()
    {
         // track the player moving to the right
        transform.position = player.transform.position + new Vector3(0, 0, -5);
    }
}
