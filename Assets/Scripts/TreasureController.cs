using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureController : MonoBehaviour
{
	public GameObject tesoro;
	public int points=5;
    private bool isTake;

    void start(){
    	isTake=false;

    }

    void OnTriggerEnter2D(Collider2D other){
    	if(other.CompareTag("Player")){
    		if(tesoro!=null && !isTake){
    			GameObject treasure= Instantiate(tesoro,new Vector3(transform.position.x,transform.position.y +1.5f,0),Quaternion.identity);
    			float time=0.5f;
    			if (treasure.tag=="Arma"){
    				PlayerPrefs.SetInt("isArmed",1);
    				time=3f;
    			}
    			StartCoroutine(destroyTreasure(treasure,time));
    		}
    		
    	}
    }

IEnumerator destroyTreasure(GameObject treasure,float timeSound){
      treasure.GetComponent<AudioSource>().Play();
      yield return new WaitForSeconds(timeSound);
    	Destroy(treasure);
    	var sum=PlayerPrefs.GetInt("score") + points;
    	PlayerPrefs.SetInt("score",sum);
    	isTake=true;
      
   }
}
