using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
   public GameObject arrow;
   public float coolDownTime=0.5f;
   public float speed=1.5f;
   private float currentCoolDownTime=0;
   private int playerPosition=0;
   private GameObject flecha;
   private float angle;
   public AudioClip audio;
    // Update is called once per frame
    void Update()
    {
    	playerPosition=PlayerController.state;
       if(Input.GetKey(KeyCode.Space) && currentCoolDownTime>coolDownTime) {
       		currentCoolDownTime=0;
       		if( PlayerPrefs.GetInt("isArmed")==1){
       			StartCoroutine(fire());
       			
       		}else{
       			//mensaje
       		}
       		
       }
       if(flecha!=null){
       		flecha.transform.Translate(0,speed * Time.fixedDeltaTime,0);
       		Destroy(flecha.gameObject,1.5f);
       }
       
       currentCoolDownTime+=Time.deltaTime;
    }

    
   IEnumerator fire(){
   		float angle=0;
   		if(playerPosition==0){
   			angle=180;
   		}else if(playerPosition==2){
   			angle=-90;
   		}else if(playerPosition==3){
   			angle=90;
   		}
   		yield return new WaitForSeconds(0.5f);
   		AudioSource.PlayClipAtPoint(audio,transform.position,1.0f);
 		flecha=(GameObject)Instantiate(arrow, transform.position,Quaternion.Euler(new Vector3(0,0,angle)));
   		
   }


}
