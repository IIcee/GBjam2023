using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victim : MonoBehaviour
{
    [SerializeField] private Collider2D player;
    [SerializeField] private Levels levels;
    private bool IsBufferOver;

    private MainManager mainManager;

    private void Start()
    {
        IsBufferOver = true;
        mainManager = MainManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (IsBufferOver)
        {
            IsBufferOver = false;
            Invoke(nameof(ResetBuffer), 2.0f);
            StartCoroutine(mainManager.LoadVictoryScene());

            //Levels scripts aren't fully implemented yet
            //levels.VictimSaved();
        }
    }

    void ResetBuffer()
    {
        IsBufferOver = true;
    }
}
