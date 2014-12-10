using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

	bool full;

	void OnTriggerEnter(Collider other){
		if (!full && other.tag == "Animal") {
			print ("animal in trap");

			other.GetComponent<Boar>().StopMoving();
			full = true;
		}
	}
}
