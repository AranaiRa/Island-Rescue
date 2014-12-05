using UnityEngine;
using System.Collections;

public class ResourceNode : MonoBehaviour {

	public GameObject DisableDuringRuntime;
	public GameObject Pickup;

	// Use this for initialization
	void Start () {
		DisableDuringRuntime.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Activate() {
		Debug.Log ("activate call; "+(Pickup != null));
		if(Pickup != null){
			Instantiate(Pickup, transform.position, transform.rotation);
		}
	}
}
