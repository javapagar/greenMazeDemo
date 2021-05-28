using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadSign : MonoBehaviour
{
	public string texto="";
	public Text textoInfo;
    public GameObject sign;
    void OnTriggerEnter2D(Collider2D other){
    	if(other.CompareTag("Player")){
            sign.SetActive(true);
    		textoInfo.text=PlayerPrefs.GetString(texto);
            
    	}
    }

    void OnTriggerExit2D(Collider2D other){
    	if(other.CompareTag("Player")){
            sign.SetActive(false);
    		textoInfo.text="";
    	}
    }
}
