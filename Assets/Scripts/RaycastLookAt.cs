using UnityEngine;
using System.Collections;

public class RaycastLookAt : MonoBehaviour {

	float range = 2.5f;
	RaycastHit hit;
	GameObject pickedUpObject;

	void FixedUpdate () {
		if (Input.GetKeyDown(Config.Use)) {
			Debug.Log ("Use hit");
			if(Physics.Raycast(transform.position, transform.forward, out hit, range)){
				Debug.DrawRay(transform.position, transform.forward, Color.red, 0.4f);
				if(hit.transform.tag == "Collectable"){
					Debug.Log (hit.transform.gameObject.name);
					pickedUpObject = hit.transform.gameObject;
					Destroy(pickedUpObject);
				}
			}
		}
	}
}
