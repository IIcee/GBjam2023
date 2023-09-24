using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Resets scene when player collides with this gameobject.
To use, put it on an empy game object with a box collider under the level.
*/
public class OutOfBounds : MonoBehaviour
{
    MainManager mainManager;

    private void Start(){
        mainManager = MainManager.Instance;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(mainManager.ResetScene());
        }
        Debug.Log("trigger");
    }
}
