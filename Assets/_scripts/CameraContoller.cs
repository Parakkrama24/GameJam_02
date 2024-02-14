using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoller : MonoBehaviour
{
    private GameObject player;
    private float initialZPos;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        initialZPos=transform.position.z;
    }

    private void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, initialZPos);
    }
}
