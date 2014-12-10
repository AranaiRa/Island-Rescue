using UnityEngine;
using System.Collections;

public class UIButtonFunctions : MonoBehaviour {
	
	public GameObject pauseMenu;	
	
	void Awake () {
		if(pauseMenu != null) pauseMenu.SetActive (false);
	}

	void FixedUpdate () {
		if(pauseMenu != null && Input.GetKeyDown(KeyCode.Escape)){
			print ("pause: " + Config.Paused);
			Config.Paused = !Config.Paused;
			pauseMenu.SetActive(Config.Paused);
		}
	}

	public void E_Unpause(){
		if(pauseMenu != null){
			Config.Paused = false;
			pauseMenu.SetActive(Config.Paused);
		}
	}

	// menu functions
	
	public void E_GoToTitle1(){
		GSM.SwitchToTitle ();
	}
	
	public void E_GoToIsland01(){
		GSM.SwitchToIsland01 ();
	}
	
	public void E_GoToIsland02(){
		GSM.SwitchToIsland02 ();
	}
	
	public void E_GoToIsland03(){
		GSM.SwitchToIsland03 ();
	}
	
	public void E_GoToCredits(){
		GSM.SwitchToCredits();
	}
	
	public void E_Quit(){
		GSM.Quit();
	}
}
