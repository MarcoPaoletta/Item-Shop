using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private bool generateNewPosition;

    void OnTriggerEnter2D(Collider2D other)
    {
        generateNewPosition = true;
    }

    void Start()
    {
        CoinCollidedWithOtherInStart();
    }

    void CoinCollidedWithOtherInStart()
    {
        if(generateNewPosition)
        {
            transform.position = new Vector3(Random.Range(-8.5f, 8.5f), Random.Range(-4.5f, 4.5f));
        }
    }
}
