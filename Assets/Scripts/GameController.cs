using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public static Vector3 playerPosition;
	public Text score;
    public GameObject uiGameover;
    public Camera mainCamera;
    public Camera secondCamera;
    
    // Start is called before the first frame update
    void Start()
    {
       PlayerPrefs.SetString("sign_1","Bienvenido al laberinto. Extrema las precauciones, no debes molestar a los osos, y no te vendría mal "+
        " un arma. Búscala hasta debajo de las piedras");
       PlayerPrefs.SetString("sign_2","Aprovecha tu visita, hay tesoros que te pueden hacer rico...,ah!, recuerda buscar un arma");
       PlayerPrefs.SetString("sign_3", "Nada es imposible...,sólo dificil. Deberas de poner todo tu empeño.");
        PlayerPrefs.SetString("sign_4", "La diferencia, aunque a algunos les resulte extraña, siempre aporta algo");
         PlayerPrefs.SetString("sign_5", "Esas piernas tan cortas no te servirán para saltar. Tienes que pensar algo para poder salir");
       PlayerPrefs.SetInt("score",0);
       PlayerPrefs.SetInt("isArmed",0);
       PlayerPrefs.SetInt("live",4);
       PlayerPrefs.SetInt("camera",1);
       PlayerPrefs.SetInt("endGame",0);
       playerPosition=GameObject.Find("Player").transform.position;
       uiGameover.SetActive(false);
       mainCamera.enabled=true;
        secondCamera.enabled=false; 

    }

    // Update is called once per frame
    void Update()
    {
        score.text=PlayerPrefs.GetInt("score")+"";
        if(PlayerPrefs.GetInt("live")==0){//GameOver
            uiGameover.SetActive(true);
        }

        //salida con scape
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
            
        }
         
         if(PlayerPrefs.GetInt("endGame")==1){
            StartCoroutine(loadScene("EndDemo"));
            
         }

        //manejo de camaras
        if(PlayerPrefs.GetInt("camera")==1){
            camera1();
        }else{
            camera2();
        }
    }

    
    void camera1(){
        mainCamera.enabled=true;
        secondCamera.enabled=false; 
    }
    void camera2(){
        mainCamera.enabled=false;
        secondCamera.enabled=true; 
    }

    IEnumerator loadScene(string sceneName){
        Animator transitionAnimator=GameObject.Find("Transparent").GetComponent<Animator>();
        transitionAnimator.Play("close");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
        transitionAnimator.Play("open");
    }
}
