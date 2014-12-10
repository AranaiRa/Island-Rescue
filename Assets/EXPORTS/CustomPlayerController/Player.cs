using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	bool inRangeOfWater = false;
	RainBasin rainBasin = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetKeyDown("e")){
			if(inRangeOfWater){
				// drink
				if(rainBasin != null){
					rainBasin.Use ();
				}
			}
		}
	}
	
	void OnTriggerEnter(Collider other){
		if(other.tag == "Water"){
			inRangeOfWater = true;
			rainBasin = other.GetComponent<RainBasin>();
			print ("in range of water");
		}
	}
	
	void OnTriggerExit(Collider other){
		if(other.tag == "Water"){
			inRangeOfWater = false;
			rainBasin = null;
		}
	}
}
