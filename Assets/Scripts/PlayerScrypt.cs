using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScrypt : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    private Vector2 mouseTouvhLast;
    public int startHealth;
    [SerializeField]public int health_;
    public float speedLevel;
    public ParticleSystem particleBonus;
    public ParticleSystem particleDamage;
    public Rigidbody rbPlayer;
    public TextMeshPro textNumBalls;
    private Game _GameScrypt;
    public TailScrypt _TailScrypt;
    [SerializeField] float counter;
    public float counterTarget;
    [SerializeField] private float mouseXPos;
    [SerializeField] public Vector3 forseMove;


    private void Awake() 
    {
        MainCamera = Camera.main;
        textNumBalls.text = health_.ToString();
        _GameScrypt = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
        _TailScrypt = GetComponent<TailScrypt>();
        speedLevel = _GameScrypt.Speed_game;
    }

    private void Update() 
    {
        textNumBalls.text = health_.ToString();
        speedLevel = _GameScrypt.Speed_game;
    }

    private void FixedUpdate() 
    {
        if (!_GameScrypt.isPause && _GameScrypt.isStart && !_GameScrypt.isGameOver)
        {
            if (Input.mousePosition.x <= Screen.width && Input.mousePosition.x >= 0 && Input.mousePosition.y <= Screen.height && Input.mousePosition.y >= 0) 
            {
            mouseXPos = Input.mousePosition.x - (Screen.width/2);
            
            var multiplerDistance = Screen.width/1080;
            Vector3 mouseVector = new Vector3 (rbPlayer.transform.position.x, rbPlayer.transform.position.y, multiplerDistance*mouseXPos/90 * multiplerDistance);                                //
            var heading = mouseVector - this.transform.position;                                    //difference beetwen mouse x and player z position
            var distance = heading.magnitude;                                                       //distance
            var direction = heading / distance;                                                     //направление
            var newForse = new Vector3(0, 0, direction.z * distance * forseMove.z);                 //new forse with direction

            rbPlayer.MovePosition(transform.position + newForse * Time.deltaTime);                  
            rbPlayer.velocity = newForse;
            }
        }
    }
    private void OnCollisionEnter(Collision other) 
    {
        if (!_GameScrypt.isPause && !_GameScrypt.isGameOver && _GameScrypt.isStart)
        {
            if (other.gameObject.tag == "Bonus")
            {
                var points = other.gameObject.GetComponent<BonusElement>().Points;
                _TailScrypt.AddCircle(points);
                Destroy(other.gameObject);
                _GameScrypt.Score = _GameScrypt.Score + points;
                var part = Instantiate(particleBonus, transform);
                part.transform.position = transform.position;

            }

            if (other.gameObject.tag == "EnemyBlock")
            {
                Vector3 norm = other.contacts[0].normal.normalized;
                var dot = Vector3.Dot(norm, Vector3.right);
                if (dot > 0.75f)
                {
                    _GameScrypt.collision = true;
                }
            }
        }
    }

    private void OnCollisionStay(Collision other) 
    {
        if (!_GameScrypt.isPause && !_GameScrypt.isGameOver && _GameScrypt.isStart)
        {
            if (other.gameObject.tag == "EnemyBlock")
            {
                Vector3 norm = other.contacts[0].normal.normalized;
                var dot = Vector3.Dot(norm, Vector3.right);
                if (dot > 0.50f)
                {
                    _GameScrypt.collision = true;
                    counter++;
                    if (counter >= counterTarget)
                    {
                        _TailScrypt.RemoveCircle(1);
                        _GameScrypt.Score++;
                        var _EnemyBlock = other.transform.gameObject.GetComponent<EnemyBlock>();
                        _EnemyBlock.DMG --;

                        Color newPartColor = other.transform.gameObject.GetComponent<Renderer>().material.color;
                        var part = Instantiate(particleDamage, transform);
                        part.transform.position = transform.position;
                        part.GetComponent<ParticleSystemRenderer>().GetComponent<ParticleSystemRenderer>().material.color = newPartColor;

                        counter= 0;
                        if (_EnemyBlock.DMG == 0)
                        {
                            Destroy(other.gameObject);
                            OnCollisionExit(other);
                        }
                    }
                }
            }
        }
    }
    private void OnCollisionExit(Collision other) 
    {
        if (!_GameScrypt.isPause && !_GameScrypt.isGameOver && _GameScrypt.isStart)
        {
            if (other.gameObject.tag == "EnemyBlock")
            {
                _GameScrypt.collision = false;
            }
        }
    }
}
