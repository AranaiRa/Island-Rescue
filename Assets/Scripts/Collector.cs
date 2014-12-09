﻿using UnityEngine;
using System.Collections;

public class Collector : MonoBehaviour {
	
	public InventoryManager inventory;
	float range = 3.5f;
	RaycastHit hit;
	int layermask;

	// Use this for initialization
	void Start () {
		layermask = 1 << 2;
		layermask = ~layermask;
	}
	
	// Update is called once per frame
	void Update () {
		GameItem activeItem = inventory.GetActiveItem ();
		if (Input.GetKeyDown (Config.Use) && (activeItem == GameItem.NONE)) {
			Debug.DrawRay(transform.position, transform.forward * range, Color.red, 0.4f);
			if(Physics.Raycast(transform.position, transform.forward, out hit, range, layermask)){
				Collectable c;
				c = hit.transform.gameObject.GetComponent<Collectable>();
				Debug.Log ("hit "+hit.transform.gameObject.name);
				if(c != null){
					if(inventory.TotalItemsHeld() < 9){
						inventory.PickUp(c.type);
						c.Remove();
					}
				}

			}
		}
		if (Input.GetKeyDown (Config.Drop) && (activeItem != GameItem.NONE)) {

			Vector3 pos = transform.position + (transform.forward * 1.5f);
			string target = "Collectable - "+GameItemStrings.Get(activeItem);
			GameObject go = (GameObject)Instantiate(Resources.Load (target, typeof(GameObject)), pos, transform.rotation);
			if(activeItem != GameItem.Log && activeItem != GameItem.Branch){
				go.AddComponent<Rigidbody>();
				go.GetComponent<Rigidbody>().angularDrag = 15f;
			}

			inventory.Drop();
		}
	}
}
