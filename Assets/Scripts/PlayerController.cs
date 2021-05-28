using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public Rigidbody2D player;
	public SpriteRenderer playerRenderer;
	public Animator animator;
  public float walkSpeed=1f;
	private Vector2 moveVector;
	static public int state=0;
	static public Vector2 currentPosition;
  public static bool isAttack=false;
  private Vector3 initPosition;
     void start(){
        //GameController.playerPosition=transform.position;
      initPosition=player.position;
     }
    // Update is called once per frame
    void Update()
    {
      
    	state=animator.GetInteger("previousMove");
    	//Movimiento respondiendo al teclado
    	if(!animator.GetBool("attack")){
    		if(moveVector.y ==0)moveVector.x=Input.GetAxisRaw("Horizontal");
       		if(moveVector.x==0)moveVector.y=Input.GetAxisRaw("Vertical");
       		if(!animator.GetBool("die")) FixedUpdate();
       		currentPosition=player.position;
       		
    	}
    	
       	
       	if(Input.GetKey(KeyCode.Space)){
       		if(PlayerPrefs.GetInt("isArmed")==1){
            StartCoroutine(attack());
          }
       	}

       	if(moveVector.x != 0){
       		animator.SetBool("vertical", false);
       		animator.SetBool("horizontal", true);
       		
       		//animator.SetFloat("directionX",moveVector.x);
       		if(moveVector.x >0){
       			animator.SetInteger("previousMove", 2);
       			playerRenderer.flipX=false;
       		}else{
       			animator.SetInteger("previousMove", 3);
       			playerRenderer.flipX=true;
       		}
       	}else{
       		animator.SetBool("horizontal", false);
       		//animator.SetFloat("directionX",0);
       	}

       	if(moveVector.y != 0){
       		animator.SetBool("vertical", true);
       		animator.SetBool("horizontal", false);
       		animator.SetFloat("directionY",moveVector.y);
       		if(moveVector.y < 0){
       			animator.SetInteger("previousMove", 0);
       		}else{
       			animator.SetInteger("previousMove", 1);
       		}       		
       	}else{
			animator.SetBool("vertical", false);
			animator.SetFloat("directionY",0);

       	}
      	

    }

    void FixedUpdate(){
      player.MovePosition(player.position + moveVector * walkSpeed * Time.fixedDeltaTime);

    }
   IEnumerator attack(){
   	animator.SetBool("attack",true);
    isAttack=true;
   	yield return new WaitForSeconds(0.5f);
   	animator.SetBool("attack",false);
    isAttack=false;
   }

   void OnTriggerEnter2D(Collider2D other){
   		if(other.CompareTag("Enemy") && other is CapsuleCollider2D){
        StartCoroutine(die());
        
   		}else if(other.CompareTag("Coin")){
        GetComponents<AudioSource>()[1].Play();
      }else if(other.CompareTag("ReStartArea")){
        GameController.playerPosition=other.transform.position;
        
      }else if(other.CompareTag("KeyFloor")){
       StartCoroutine(openDoor());
      }else if(other.CompareTag("EndDemo")){
        PlayerPrefs.SetInt("endGame",1);
      }
   }

   IEnumerator die(){
      animator.SetBool("die",true);
      EnemyMove.isKiller=true;
      GetComponent<AudioSource>().Play();
      yield return new WaitForSeconds(1);
      var loseLive=PlayerPrefs.GetInt("live")-1;
      PlayerPrefs.SetInt("live",loseLive);
      if(loseLive>0){
        animator.SetBool("die",false);
        var spriteHeart=GameObject.Find("Live"+loseLive);
        Destroy(spriteHeart);
        transform.position=GameController.playerPosition;//ResPawn
      }
      //Destroy(gameObject);
   }

   IEnumerator openDoor(){
        GetComponents<AudioSource>()[2].Play();
        PlayerPrefs.SetInt("camera",2);
        yield return new WaitForSeconds(1);
        GameObject.Find("EndDoor").SetActive(false);
        yield return new WaitForSeconds(1);
        PlayerPrefs.SetInt("camera",1);
       
  }
}
