using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    public GameObject spiderPrefab;
    public GameObject gollemPrefab;
    public GameObject bossPrefab;
    private GameObject player;
    [SerializeField] Vector2[] spiderPositions;
    [SerializeField] Vector2 gollemPosition;
    [SerializeField] Vector2 bossPosision;
    private void Awake()
    {
        Instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateGameState(GameState.Phase1);
    }

    // Update is called once per frame
    void Update()
    {
        if(!player.GetComponent<Player>().isAlive)
        {
            State = GameState.LoseScreen;
        }
        switch(State)
        {
            case GameState.Phase1:
                if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                {
                    //! implementar fase 2
                    UpdateGameState(GameState.Phase3);
                }
                break;
            case GameState.Phase2:
                if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                {
                    UpdateGameState(GameState.Phase3);
                }
                break;
            case GameState.Phase3:
                if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                {
                    //! Implementar fase 4
                    UpdateGameState(GameState.Phase5);
                }
                break;
            case GameState.Phase4:
                if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                {
                    UpdateGameState(GameState.Phase5);
                }
                Debug.Log(GameObject.FindGameObjectsWithTag("Enemy").Length);
                break;
            case GameState.Phase5:
                if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                {
                    UpdateGameState(GameState.WinScreen);
                }
                Debug.Log(GameObject.FindGameObjectsWithTag("Enemy").Length);
                break; 
            case GameState.WinScreen:
                Invoke(nameof(WinScreen), 1.0f);
                break;       
            case GameState.LoseScreen:
                Invoke(nameof(LoseScreen), 5.0f);
                break;
            default:
            break;
        }
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
            case GameState.Phase3:
                InvokePhase3();
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

    void LoseScreen()
    {
        //! Implementar pantalla de GameOver
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void WinScreen()
    {
        Debug.Log("Ganaste");

        if(Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
