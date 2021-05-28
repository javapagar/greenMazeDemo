using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
	public SpriteRenderer sR_enemy;
	private Rigidbody2D rB_enemy;
	private Vector2 moveVector;
	public float walkSpeed=2.5f;
	public Animator enemyAnimator;
    static public bool isKiller=false;
    private Vector3 initPosition;

    // Start is called before the first frame update
    void Start()
    {
        initPosition=transform.position;
        rB_enemy=this.gameObject.GetComponent<Rigidbody2D>();
        enemyAnimator.SetBool("attack", false);
    }

    // Update is called once per frame
    void Update()
    {
    	if(!isKiller){
            var xDistance=rB_enemy.position.x - PlayerController.currentPosition.x;
    	    var yDistance=rB_enemy.position.y - PlayerController.currentPosition.y;
    	    var direction=PlayerController.currentPosition-rB_enemy.position;
            direction.Normalize();
    	   var auxPosition=rB_enemy.position;
            if( xDistance >0.2 && enemyAnimator.GetBool("attack")){
                enemyAnimator.SetBool("vertical", false);
                enemyAnimator.SetBool("horizontal",true);
                enemyAnimator.SetInteger("previousMove", 2);
                sR_enemy.flipX=true;
                //rB_enemy.MovePosition(rB_enemy.position + new Vector2(-1,0) * walkSpeed * Time.fixedDeltaTime);
             
            }else if (xDistance<-0.2  && enemyAnimator.GetBool("attack")){
                enemyAnimator.SetBool("vertical", false);
                enemyAnimator.SetBool("horizontal",true);
                enemyAnimator.SetInteger("previousMove", 3);
                sR_enemy.flipX=false;
                //rB_enemy.MovePosition(rB_enemy.position + new Vector2(1,0) * walkSpeed * Time.fixedDeltaTime);
                
            }
            
            if(yDistance >0.2  && enemyAnimator.GetBool("attack")){
                enemyAnimator.SetBool("vertical", true);
                enemyAnimator.SetBool("horizontal",false);
                enemyAnimator.SetInteger("previousMove", 1);
                enemyAnimator.SetFloat("directionY", -1);
                //rB_enemy.MovePosition(rB_enemy.position + new Vector2(0,-1) * walkSpeed * Time.fixedDeltaTime);
            }   else if( yDistance < -0.2  && enemyAnimator.GetBool("attack")){
                enemyAnimator.SetBool("vertical", true);
                enemyAnimator.SetBool("horizontal",false);
                enemyAnimator.SetInteger("previousMove", 0);
                enemyAnimator.SetFloat("directionY", 1);
                //rB_enemy.MovePosition(rB_enemy.position + new Vector2(0,1) * walkSpeed * Time.fixedDeltaTime);
            }
            if(enemyAnimator.GetBool("attack"))rB_enemy.MovePosition(rB_enemy.position + (direction * walkSpeed * Time.fixedDeltaTime));
            

        }else{
           StartCoroutine(reSpawn());
        }
    }

    IEnumerator reSpawn(){
        yield return new WaitForSeconds(1);
        isKiller=false;
        enemyAnimator.SetBool("attack", false);
        enemyAnimator.SetBool("vertical", false);
        enemyAnimator.SetBool("horizontal", false);
        transform.position=initPosition;
    }
    

    void OnTriggerEnter2D(Collider2D other){
    	if(other.CompareTag("Player")){
    		enemyAnimator.SetBool("attack", true);
    	}
    }

    
}
