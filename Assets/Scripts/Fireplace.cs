using UnityEngine;
using System.Collections;

public class Fireplace : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		Config.inventory.SetNearFire(true);
	}
	
	void OnTriggerExit(Collider other){
		Config.inventory.SetNearFire(false);
	}
}
