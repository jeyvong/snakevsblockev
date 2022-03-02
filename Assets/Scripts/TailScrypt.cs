using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailScrypt : MonoBehaviour
{
    public GameObject player;
    public GameObject player_tail;
    private PlayerScrypt _PlayerScrypt;
    private Transform playerTransform;
    public float speed;
    [SerializeField]public Vector3 offset;
    [SerializeField] public int numberOfBalls;
    [SerializeField] private List<Transform> transformCirclesTail;
    [SerializeField] private List<Vector3> positionsCirclesTail;
    public GameObject bubble_pop_sound_sourse;
    public GameObject eat_pop_sound_sourse;
    private AudioSource audioBubblePop;
    private AudioSource audioEat;
    private AudioSource audioOnGame;

    private void Awake() 
    {
        _PlayerScrypt = player.GetComponent<PlayerScrypt>();
        playerTransform = player.GetComponent<Transform>();

    }
    void Start()
    {
        if (_PlayerScrypt.startHealth <= 0) numberOfBalls = 5;
        else numberOfBalls = _PlayerScrypt.startHealth;
        positionsCirclesTail.Add(player.transform.position);
        AddCircle(numberOfBalls);
        audioBubblePop = GameObject.FindGameObjectWithTag("BubblePopAudioSource").GetComponent<AudioSource>();
        audioEat = GameObject.FindGameObjectWithTag("EatPopAudiosource").GetComponent<AudioSource>();
        audioOnGame = GameObject.FindGameObjectWithTag("OnGameAudioSource").GetComponent<AudioSource>();
        if (!audioOnGame.isPlaying) audioOnGame.Play();

    }


    private void FixedUpdate() 
    {
        positionsCirclesTail[0] = playerTransform.position;
        numberOfBalls = _PlayerScrypt.health_;

        for (int i = 0; i <= numberOfBalls-1; i++)
        {
            Vector3 newTarget = new Vector3 (positionsCirclesTail[i+1].x, positionsCirclesTail[i+1].y, positionsCirclesTail[i].z);
            transformCirclesTail[i].position = Vector3.Lerp(transformCirclesTail[i].position, newTarget, speed);
            positionsCirclesTail[i+1] = transformCirclesTail[i].position;
            
        }
        

    }
    private void Update() 
    {
        if (Input.GetKeyDown("up"))
        {
            AddCircle(1);
        }
        if (Input.GetKeyDown("down"))
        {
            RemoveCircle(1);
        }
        
    }

    public void RemoveCircle(int numCircle)
    {
        if (transformCirclesTail.Count > 0)
        {
            for (int i = 0; i <= numCircle-1; i++)
            {
                Destroy(transformCirclesTail[transformCirclesTail.Count-1].gameObject);
                transformCirclesTail.RemoveAt(transformCirclesTail.Count-1);
                positionsCirclesTail.RemoveAt(positionsCirclesTail.Count-1);
                if (!audioBubblePop.isPlaying) audioBubblePop.Play();
            }
            _PlayerScrypt.health_ -= numCircle;  
        }  
    }

    public void AddCircle(int numCircle) 
    {
        _PlayerScrypt.health_ += numCircle;

        if (transformCirclesTail.Count > 0)
        {
            if (!audioEat.isPlaying) audioEat.Play();
        }

        for (int i = 0; i <= numCircle-1; i++)
        {
            
            Vector3 position = positionsCirclesTail[positionsCirclesTail.Count-1] + offset;
            var newCircle = Instantiate(player_tail, position, Quaternion.identity);
            transformCirclesTail.Add(newCircle.transform);
            positionsCirclesTail.Add(newCircle.transform.position);
        }

        
    }
}
