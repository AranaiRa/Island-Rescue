using UnityEngine;
using System.Collections;

/// <summary>
/// This class controls a placeholder object that gets replaced with a collectable prefab.
/// </summary>
public class ResourceNode : MonoBehaviour {

	public GameObject DisableDuringRuntime;
	public GameObject Pickup;

	// Use this for initialization
	void Start () {
		//Disable the model used to view the node in editor mode.
		DisableDuringRuntime.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Add the gameitem to the world.
	/// </summary>
	public void Activate() {
		if(Pickup != null){
			Instantiate(Pickup, transform.position, transform.rotation);
		}
	}
}
