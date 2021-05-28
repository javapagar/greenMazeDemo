using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonEvents : MonoBehaviour
{
	public Animator transitionAnimator;
     public void LoadMaze(){
     	PlayerPrefs.DeleteAll();
     	StartCoroutine(loadScene("Maze"));
    	
    }

    public void LoadIntro(){
     	PlayerPrefs.DeleteAll();
     	StartCoroutine(loadScene("Intro"));
    	
    }

    public void LoadEnd(){
     	StartCoroutine(loadScene("EndDemo"));
    	
    }

    public void Continue(){
    	PlayerPrefs.SetInt("live",4);
    }

    public void quitBye(){
    	Application.Quit();
    }
    IEnumerator loadScene(string sceneName){
    	transitionAnimator.Play("close");
    	yield return new WaitForSeconds(1f);
    	SceneManager.LoadScene(sceneName);
    	transitionAnimator.Play("open");
    }
}
