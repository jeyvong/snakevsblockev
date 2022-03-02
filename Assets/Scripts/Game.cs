using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Game : MonoBehaviour
{
    public float temp_speed_start;
    public float velocity_speed_start;
    [SerializeField] public bool isStart;           //game is begining
    [SerializeField] public bool isPause;           //
    [SerializeField] public bool isGameOver;        //
    [SerializeField] public float Speed_game;       //speed of moving objects
    [SerializeField] public float temp_speed;       //speed of level
    [SerializeField] public float velocity_speed;   //addition speed's part
    [SerializeField] public int Score;            //Score of game;
    [SerializeField] private Canvas GameCanvas;     //Game overlay
    [SerializeField] public GameObject GameCanvasGameOver;     //Game overlay
    [SerializeField] public float standart_targetCounterToRespawnBlocks;
    [SerializeField] public bool collision;         
    [SerializeField] public int Difficult;      //level of difficult
    [SerializeField] public GameObject player;  //player. From Canvas scrypt
    [SerializeField] public bool ToggleInfinityGameIsOn;
    [SerializeField] public PlayerScrypt playerScrypt;
    [SerializeField] public GameObject respawnAndMoveObject;  //player. From Canvas scrypt
    [SerializeField] public int level;
    [SerializeField] public int TargetScoreToLvl;
    [SerializeField] public TextMeshProUGUI textNextLevel;
    public GameObject ButtonNextLevel;
    public GameObject CanvasNextLevel;
    public GameObject CanvasGameDone;
    public GameObject ButtonReplay;
    private AudioSource audioOnGame;


    private void Awake() 
    {
        temp_speed = temp_speed_start;
        velocity_speed = velocity_speed_start;
        audioOnGame = GameObject.FindGameObjectWithTag("OnGameAudioSource").GetComponent<AudioSource>();
        CanvasNextLevel.SetActive(false);
        //ToggleInfinityGameIsOn = Toggle.GetComponent<Toggle>().isOn;
    }

    void Update()
    {
        if (!isPause && isStart && !isGameOver)
        {
            if (!collision && !isPause && isStart) 
            {
                changeLevelSpeed();
            }
            if (collision) 
            {
                Speed_game = 0;
            }
        }
        if (isPause && isStart && !isGameOver) Speed_game = 0;

        if (player)
        {
            if (playerScrypt == null) playerScrypt = player.GetComponent<PlayerScrypt>();
            if (playerScrypt.health_ <= 0 && isStart && !isPause && !isGameOver && Score > 1)
             {
                if (!collision) return;
                Speed_game = 0;
                isStart = false;
                isGameOver = true;
                GameCanvasGameOver.SetActive(true);
                audioOnGame.Stop();

                for (int i = 0; i < GameObject.FindGameObjectsWithTag("EnemyBlock").Length; i++)
                {
                    Destroy(GameObject.FindGameObjectsWithTag("EnemyBlock")[i]);
                }
                for (int i = 0; i < GameObject.FindGameObjectsWithTag("Bonus").Length; i++)
                {
                    Destroy(GameObject.FindGameObjectsWithTag("Bonus")[i]);
                }
                for (int i = 0; i < GameObject.FindGameObjectsWithTag("Barrier").Length; i++)
                {
                    Destroy(GameObject.FindGameObjectsWithTag("Barrier")[i]);
                }
                Destroy(player);
                Destroy(respawnAndMoveObject);
                Debug.Log("!");
             }
        }

    if (!ToggleInfinityGameIsOn)
    {
        TargetScoreToLvl = level*10;

        if (Score <= TargetScoreToLvl) textNextLevel.text = "LEVEL: " + level + "\n" + "Left: " + (TargetScoreToLvl - Score);
        else if (Score > TargetScoreToLvl) textNextLevel.text = "LEVEL: " + level + "\n" + "Left: 0";

        if (Score >= TargetScoreToLvl)
        {
                if (level < 3)
                {
                    CanvasNextLevel.SetActive(true);
                    isPause = true;
                    ButtonNextLevel.SetActive(true);
                    audioOnGame.Stop();
                }
                else
                {
                    isPause = true;
                    audioOnGame.Stop();
                    CanvasGameDone.SetActive(true);
                    ButtonReplay.SetActive(true);
                }
            
        }
    }
    }

    enum Activities
    {
        isPlay,
        isPause
    }

    private void changeLevelSpeed()
    {
        temp_speed += velocity_speed;
        Speed_game = temp_speed;
        
    }

    public GameObject FindMyObject(string name, out GameObject obj)
    {
        GameObject[] all = GameObject.FindObjectsOfType<GameObject> ();
        foreach (GameObject _obj in all)
            {
                if (_obj.name == name) 
                {
                    obj = _obj;
                    return obj;
                }
            }
        return obj = null;
    }

}
