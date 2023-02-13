using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private float TimeBeforeEnd = 10f;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private int health = 10;

    [SerializeField] private UnityEvent afterMatch;
    
    private float timeBeforeEndingTheGame = 0;
    
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        timeBeforeEndingTheGame = TimeBeforeEnd;
        PerformAftermatch();
    }

    private void PerformAftermatch()
    {
        if (afterMatch.GetPersistentEventCount() > 0)
        {
            Debug.Log("Event triggered for ending");
            afterMatch.Invoke();
        }
    }

    public void GameOver()
    {
        if (timeBeforeEndingTheGame > 0)
        {
            // Give more time
        }
        LoadManager.Instance.GameOverForPlayer();
    }
    
    public int GetScore()
    {
        return 10;
    }
}
