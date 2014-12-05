using UnityEngine;
using System.Collections;

public class Collector : MonoBehaviour {
	
	public InventoryManager inventory;
	float range = 2.5f;
	RaycastHit hit;
	int layermask;

	// Use this for initialization
	void Start () {
		layermask = 1 << 2;
		layermask = ~layermask;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (Config.Use) && (inventory.GetActiveItem () == GameItem.NONE)) {
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
	}
}
