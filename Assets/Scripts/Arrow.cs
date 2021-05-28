using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject explosion; 
    public float speed=1.5f;

    // Update is called once per frame
    void Update()
    {
      
    	//transform.Translate(0,speed * Time.fixedDeltaTime,0);
    	//Destroy(gameObject,1.5f);
      
    }

    void OnTriggerEnter2D(Collider2D other){
   		if(other.CompareTag("Enemy") && other is CapsuleCollider2D){
   			Vector3 position=other.gameObject.transform.position;
        Destroy(other.gameObject);
   			Destroy(gameObject);

        var explo=Instantiate(explosion, position,Quaternion.Euler(new Vector3(0,0,0)));
        Destroy(explo,0.3f);
       
        //StartCoroutine(getExplosion(position));
   		}else if(other.CompareTag("Bush")){//chocando con otra cosa se destruye
        Destroy(gameObject);
      }
   }

  /*IEnumerator getExplosion(Vector3 position){
    var explo=Instantiate(explosion, position,Quaternion.Euler(new Vector3(0,0,0)));
    yield return new WaitForSeconds(0.1f);
    Debug.Log("explosion");
    Destroy(explo,0.2f);
   }*/
}
