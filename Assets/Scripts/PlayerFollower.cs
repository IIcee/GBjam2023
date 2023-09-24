using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Script for the camera to follow the player.
*/
public class PlayerFollower : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] Vector3 offset;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
