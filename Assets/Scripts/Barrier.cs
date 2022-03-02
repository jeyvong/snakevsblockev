using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public Game _GameScrypt;
    private Rigidbody selfRB;
    private Vector3 speed;
    private float _speed;

    private void Start() 
    {
        selfRB = GetComponent<Rigidbody>();
        _GameScrypt = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
    }
    void Update()
    {
        _speed = _GameScrypt.Speed_game;
        speed = new Vector3 (_speed, 0f, 0f);
    }

    private void FixedUpdate() 
    {
        selfRB.velocity = speed;
    }
    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.tag == "Player")
        {
            var playerRb = other.gameObject.GetComponent<Rigidbody>();
            playerRb.AddForce(new Vector3(0,0,20), ForceMode.Force);
            Debug.Log(other.gameObject.tag + "2");
        }
    }
}
