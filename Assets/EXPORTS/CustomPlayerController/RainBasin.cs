
using UnityEngine;
using System.Collections;

public class RainBasin : MonoBehaviour {

	// full = 1.75
	// empty = .5
	// .25 decrements
	// 5 uses

	Vector3 target;
	bool canUse = true;
	bool empty = false;

	// Use this for initialization
	void Start () {
		target = transform.position;
	}

	void FixedUpdate(){
		if(!canUse){
			Vector3 pos = transform.position;
			pos.y -= .05f;
			transform.position = pos;
			if(pos.y <= target.y) canUse = true;
		}else{
			if(empty) Destroy(gameObject);
		}
	}

	public void Use(){
		target.y -= .25f;
		print ("use rain basin " + target.y);
		canUse = false;
		if(target.y <= .5f) empty = true;
	}
}
