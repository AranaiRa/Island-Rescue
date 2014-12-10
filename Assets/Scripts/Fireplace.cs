using UnityEngine;
using System.Collections;

/// <summary>
/// This class controls the fireplace object.
/// </summary>
public class Fireplace : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		//Informs the game that the player is near a fire.
		Config.inventory.SetNearFire(true);
	}
	
	void OnTriggerExit(Collider other){
		//Informs the game that the player is no longer near a fire.
		Config.inventory.SetNearFire(false);
	}
}
