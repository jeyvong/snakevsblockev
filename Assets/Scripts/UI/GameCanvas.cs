using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameCanvas : MonoBehaviour
{
    public Game _GameScrypt;
    public TextMeshProUGUI _TextNumScore;
    public TextMeshProUGUI _TextNumScore_GameOver;

    private void Awake()
    {
        _GameScrypt = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
    }

    void Update()
    {
        _TextNumScore.text = _GameScrypt.Score.ToString();
        _TextNumScore_GameOver.text = "Score: " + _TextNumScore.text;
    }
}
