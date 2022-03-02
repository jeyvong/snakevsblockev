using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyBlock : MonoBehaviour
{
    public TextMeshPro text;
    public PlayerScrypt _PlayerScrypt;
    public Game _GameScrypt;
    private Rigidbody selfRB;
    private new Renderer renderer;
    private Vector3 speed;
    private float _speed;
    [SerializeField] public int DMG;

    private void Awake() 
    {
        renderer = GetComponent<Renderer>();
        _GameScrypt = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
        _PlayerScrypt = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScrypt>();
        _speed = _GameScrypt.Speed_game;
        selfRB = GetComponent<Rigidbody>();
        speed = new Vector3 (_speed, 0f, 0f);
        text.text = DMG.ToString();
    }

    void Start()
    {
        if (_PlayerScrypt.health_ > 12  ) DMG = Random.Range(Mathf.FloorToInt(_PlayerScrypt.health_/7), _PlayerScrypt.health_);
        else DMG = Random.Range(1, 6);
        if (DMG>0) changeMaterialColor(DMG, _PlayerScrypt.health_, renderer);
    }

    void Update()
    {
        if (DMG>0) changeMaterialColor(DMG, _PlayerScrypt.health_, renderer);
        text.text = DMG.ToString();
        _speed = _GameScrypt.Speed_game;
        speed = new Vector3 (_speed, 0f, 0f);
    }

    private void FixedUpdate() 
    {
        selfRB.velocity = speed;
    }

    private void changeMaterialColor(int DMG, int player_health, Renderer renderer)
    {
        if (player_health > 0)
        {
            var byteC = 255 - Mathf.Round(DMG/player_health)*255;
            if (byteC < 0) byteC = 0; 
            renderer.material.color = new Color32(255, (byte)(byteC), 6, 255); //
        }
        else
        {
            renderer.material.color = new Color32(255, 0, 6, 255);
        }
    }
}
