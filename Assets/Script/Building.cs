using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Building : MonoBehaviour
{
    public GameObject[] blocks;
    public float[] randomX;
    private int _counterChangePosition;

    public Player player;
    public GameObject[] hurdle;
    
    private int _randomXNumber;

    public void Start()
    {
        InvokeRepeating("SpawnHurdle",20f,20f);
        _counterChangePosition = 6;
        _randomXNumber = 0;
    }

    public void SpawnHurdle()
    {
        var randomBlock = Random.Range(0, 3);
        var newBlock = Instantiate(hurdle[randomBlock], new Vector3(randomX[_randomXNumber], 9.76f, -1.25f),
            Quaternion.identity);
        newBlock.transform.parent = this.gameObject.transform;
    }
    
    public void MoveDown()
    {
        if (!player.death)
        {
            player.points++;
            _counterChangePosition--;
            var randomBlock = Random.Range(0, 6);
            transform.DOMove(new Vector3(transform.position.x, transform.position.y - 1.4f, transform.position.z), 0.1f,
                false);

            if (_counterChangePosition == 0)
            {
                _randomXNumber = Random.Range(0, 2);
                _counterChangePosition = 6;
            }

            var newBlock = Instantiate(blocks[randomBlock], new Vector3(randomX[_randomXNumber], 9.76f, 0),
                Quaternion.identity);
            newBlock.transform.parent = this.gameObject.transform;
        }
    }
}
