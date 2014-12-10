using UnityEngine;
using System.Collections;

public static class GSM {
	
	public static void SwitchToTitle(){
		Application.LoadLevel ("Title");
	}
	
	public static void SwitchToIsland01(){
		Application.LoadLevel ("Island01");
	}
	
	public static void SwitchToIsland02(){
		Application.LoadLevel ("Island02");
	}
	
	public static void SwitchToIsland03(){
		Application.LoadLevel ("Island03");
	}
	
	public static void SwitchToInstructions(){
		Application.LoadLevel ("Instructions");
	}
	
	public static void SwitchToCredits(){
		Application.LoadLevel ("Credits");
	}
	
	public static void SwitchToLose(){
		Application.LoadLevel ("Lose");
	}
	
	public static void SwitchToWin(){
		Application.LoadLevel ("Win");
	}

	public static void Quit(){
		Application.Quit ();
	}
	
}
