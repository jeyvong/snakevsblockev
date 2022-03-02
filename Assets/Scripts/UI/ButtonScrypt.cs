using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonScrypt : MonoBehaviour
{
    public GameObject[] buttons;
    public RectTransform button;
    public GameObject canvas;
    public GameObject GameCanvas;     
    public GameObject GameCanvasGameOver;
    public GameObject CanvasNextLevel;
    public GameObject CanvasGameDone;
    public GameObject player;
    public GameObject respawnBlocks;
    public GameObject easyButton;
    public GameObject mediumButton;
    public GameObject hardButton;
    public GameObject ButtonNextLevel;
    public GameObject ButtonReplay;
    public GameObject OnGameMusic;
    public GameObject MenuMusic;
    public GameObject infinityToggle;
    public TextMeshProUGUI textNextLevel;
    public Game _GameScrypt;
    public TailScrypt _TailScrypt;
    private Vector2 tarpetPoint;
    public AudioSource OnGameMusicAudioSource;
    private float sinMultiplerY;
    private float sinMultiplerX;
    

    private void Awake() {
        OnGameMusicAudioSource = OnGameMusic.GetComponent<AudioSource>();
        _GameScrypt = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
        _GameScrypt.ButtonNextLevel = ButtonNextLevel;
        _GameScrypt.ButtonReplay = ButtonReplay;
        _GameScrypt.textNextLevel = textNextLevel;
    }
    private void Start() 
    {
        //MenuMusic.GetComponent<AudioSource>().Play();
    }

    public void StartGame()
    {
        _GameScrypt.ToggleInfinityGameIsOn = infinityToggle.GetComponent<Toggle>().isOn;
        canvas.SetActive(false);
        GameCanvas.SetActive(true);
        GameCanvasGameOver.SetActive(false);
        ButtonNextLevel.SetActive(false);
        ButtonReplay.SetActive(false);

        

        if (!_GameScrypt.ToggleInfinityGameIsOn && _GameScrypt.level == 0) _GameScrypt.level = 1;
        if (!_GameScrypt.ToggleInfinityGameIsOn) textNextLevel.gameObject.SetActive(true);
        Vector3 position = new Vector3(-1.875f, 0.60f, 0.375f);
        var _player = Instantiate(player, position, Quaternion.identity);
        _GameScrypt.player = _player;
        _TailScrypt = _player.GetComponent<TailScrypt>();
        position = new Vector3(-65.125f, 0f, 0f);
        var _respawnAndMoveObject = Instantiate(respawnBlocks, position, Quaternion.identity);
        _GameScrypt.respawnAndMoveObject = _respawnAndMoveObject;
        _GameScrypt.Score = 0;
        _GameScrypt.isStart = true;
        _GameScrypt.collision = false;
        _GameScrypt.isGameOver = false;
        _GameScrypt.temp_speed = _GameScrypt.temp_speed_start;
        _GameScrypt.velocity_speed = _GameScrypt.velocity_speed_start;
        MenuMusic.GetComponent<AudioSource>().Stop();

        
    }

    public void NextLevel()
    {
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
                _TailScrypt.RemoveCircle(_TailScrypt.numberOfBalls);
                Destroy(_GameScrypt.player);
                Destroy(_GameScrypt.respawnAndMoveObject);
                Debug.Log("!");
                _GameScrypt.level++;
                _GameScrypt.isGameOver = true;
                StartGame();
                textNextLevel.text = "LEVEL " + _GameScrypt.level;
                CanvasNextLevel.SetActive(false);
                _GameScrypt.isPause = false;

    }
    public void ResetGame()
    {
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
        _TailScrypt.RemoveCircle(_TailScrypt.numberOfBalls);
        Destroy(_GameScrypt.player);
        Destroy(_GameScrypt.respawnAndMoveObject);
        Debug.Log("!");
        _GameScrypt.isGameOver = true;
        _GameScrypt.level = 0;
        CanvasGameDone.SetActive(false);
        ButtonReplay.SetActive(false);
        
        StartGame();
        _GameScrypt.isPause = false;
    }



   

}
