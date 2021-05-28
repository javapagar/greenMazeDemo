using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int points=0;
    void OnTriggerEnter2D(Collider2D other)
    {
    	if(other.CompareTag("Player")){
            Destroy(gameObject);
            var sum=PlayerPrefs.GetInt("score") + points;
    		PlayerPrefs.SetInt("score", sum);
    		
    	}
    }
}
