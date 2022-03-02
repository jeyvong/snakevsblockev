using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BonusElement : MonoBehaviour
{
    public TextMeshPro text;
    public GameObject player;
    private Rigidbody selfRB;
    public Vector3 speed;
    public float _speed;
    [SerializeField] public int Points;

    void Start()
    {
        selfRB = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        _speed = player.GetComponent<PlayerScrypt>().speedLevel;
        speed = new Vector3 (_speed, 0f, 0f);
        Points = Random.Range(1, 5);
        text.text = Points.ToString();
    }
    private void Update() 
    {
        _speed = player.GetComponent<PlayerScrypt>().speedLevel;
        speed = new Vector3 (_speed, 0f, 0f);
    }

    private void FixedUpdate() 
    {
        selfRB.velocity = speed;
    }

}
