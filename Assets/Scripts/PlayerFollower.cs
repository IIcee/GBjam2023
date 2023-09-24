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
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
        Debug.Log(offset);
    }
}
