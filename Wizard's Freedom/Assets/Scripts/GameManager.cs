using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    public GameObject spiderPrefab;
    public GameObject gollemPrefab;
    public GameObject bossPrefab;
    [SerializeField] Vector2[] spiderPositions;
    [SerializeField] Vector2 gollemPosition;
    [SerializeField] Vector2 bossPosision;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateGameState(GameState.Phase1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum GameState
    {
        Phase1,
        Phase2,
        Phase3,
        Phase4,
        Phase5,
        WinScreen,
        LoseScreen,
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch(newState)
        {
            case GameState.Phase1:
                InvokePhase1();
                break;
            default:
                break;
        }
    }

    void InvokePhase1()
    {
        GameObject[] spiders = new GameObject[5];
        for(int i = 0; i < 5; i++)
        {
            spiders[i] = Instantiate(spiderPrefab, (Vector3)spiderPositions[i], Quaternion.identity);
        }
    }

    void InvokePhase2()
    {
        //Invocar a los esqueletos.
    }

    void InvokePhase3()
    {
        GameObject gollem = Instantiate(gollemPrefab, (Vector3)gollemPosition, Quaternion.identity);
    }

    void InvokePhase4()
    {
        GameObject[] spiders = new GameObject[5];
        for(int i = 0; i < 5; i++)
        {
            spiders[i] = Instantiate(spiderPrefab, (Vector3)spiderPositions[i], Quaternion.identity);
        }
    }

    void InvokePhase5()
    {
        GameObject boss = Instantiate(bossPrefab, (Vector3)bossPosision, Quaternion.identity);
    }
}
