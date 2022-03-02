using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLvl : MonoBehaviour
{
    public GameObject bonus;
    public GameObject barrier;
    public GameObject enemyBlock;
    [SerializeField] float counter;
    public float counterTarget;
    [SerializeField] public float counterTargetMinus; 
    private Vector3 respawn;
    private Vector3 respawnBarrierOffset;
    [SerializeField] private List<int> linears;
    private Game _GameScrypt;

    private void Awake() {
        _GameScrypt = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
        respawnBarrierOffset = new Vector3 (2, 0, 0); 
        addLinears();
    }

    private void Update() 
    {
        if (!_GameScrypt.isPause && _GameScrypt.isStart)
        {
            if (counter >= counterTarget)
            {
                var chooseBlock = Random.Range(1, 4);
                switch (chooseBlock)
                {
                    case 1: 
                        respawn = new Vector3 (-65, 0.5f, Random.Range(-5,5.375f));
                        Instantiate(bonus, respawn, Quaternion.identity);
                    break;
                    case 2:
                        addLinears();
                        var chooseNumbsBlocks = Random.Range(3,7);   
                        for (int i = 0; i < chooseNumbsBlocks; i++)
                        {
                            var chooseLinea = Random.Range(0, linears.Count);
                            respawn = new Vector3 (-65, 0, linears[chooseLinea]);
                            Instantiate(enemyBlock, respawn, Quaternion.identity);
                            linears.RemoveAt(chooseLinea);
                        }
                    break;
                    case 3:
                        addLinears();                                                           //refresh list with coordinates
                        chooseNumbsBlocks = Random.Range(3,7);                                  //gen. random num of blocks
                        for (int i = 0; i < chooseNumbsBlocks; i++)                 
                        {
                            var chooseLinea = Random.Range(0, linears.Count);                   //choose linea of block
                            respawn = new Vector3 (-65, 0, linears[chooseLinea]);               //set Vector3
                            var chooseBlockOrBonus = Random.Range(0,7);                         
                            if (chooseBlockOrBonus >= 6) 
                            {
                                respawn = new Vector3 (-65, 0.5f, linears[chooseLinea]);
                                Instantiate(bonus, respawn, Quaternion.identity);
                            }
                            else 
                            {
                                respawn = new Vector3 (-65, 0, linears[chooseLinea]);
                                Instantiate(enemyBlock, respawn, Quaternion.identity);
                                if (chooseLinea != 0)
                                {
                                    var numBarrierLong = Random.Range(2, 7);
                                    for (int j = 0; j < numBarrierLong; j++)
                                    {
                                        Vector3 addionOffset = new Vector3 (j, 0, 0);
                                        Instantiate(barrier, respawn+respawnBarrierOffset+addionOffset, Quaternion.identity);
                                    }
                                }
                            }
                            linears.RemoveAt(chooseLinea);                                      //delete coordinate from list
                        }
                    break;
                }
                counter = 0;
            }
            ChangeSpeedRespawn();
        }
    }
    private void FixedUpdate() 
    {
       if (_GameScrypt.Speed_game != 0) counter+= Time.deltaTime;
    }

        public void addLinears()
    {
        linears.Clear();
        linears.Add(-5);
        linears.Add(-3);
        linears.Add(-1);
        linears.Add(1);
        linears.Add(3);
        linears.Add(5);
    }

    public void ChangeSpeedRespawn()
    {
        if (counterTarget > 0.8f & _GameScrypt.Speed_game!=0)
        {
            counterTargetMinus = _GameScrypt.velocity_speed/(counterTarget*10);
            counterTarget -= counterTargetMinus;
        }
    }

}
