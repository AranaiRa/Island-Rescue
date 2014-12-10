using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class controls spawning of interactable resources.
/// </summary>
public class ResourceManager : MonoBehaviour {

	List<ResourceNode> nodes = new List<ResourceNode>();
	public GameObject[] CollectablePrefabs;
	public float[] CollectableWeights;
	public string Tag = "ResourceNode";

	// Use this for initialization
	void Start () {
		GameObject[] go = GameObject.FindGameObjectsWithTag (Tag);
		for(int i=0;i<go.Length;i++){
			nodes.Add(go[i].GetComponent<ResourceNode>());
		}
		Populate ();
	}

	/// <summary>
	/// Fill the various nodes with gameitems associated in the editor.
	/// </summary>
	void Populate(){
		Shuffle ();
		float totalPercent = 0f;
		foreach (float f in CollectableWeights) {
			totalPercent += f;
		}
		if(totalPercent > 1f){
			Debug.Log ("WARNING: Node weights exceed 100%! Breaking process.");
		}
		else {
			int i = nodes.Count-1;
			for(int col=0; col<CollectablePrefabs.Length; col++){
				int numSpots = Mathf.CeilToInt(CollectableWeights[col] * (float)nodes.Count);
				int c = 0;
				while(c < numSpots){
					nodes[i].Pickup = CollectablePrefabs[col];
					nodes[i].Activate();
					c++;
					i--;
				}
			}
		}
	}

	/// <summary>
	/// Randomize the order of items.
	/// </summary>
	void Shuffle(){
		int n = nodes.Count;  
		while (n > 1) {  
			n--;
			int k = Random.Range(0,n + 1);  
			ResourceNode value = nodes[k];  
			nodes[k] = nodes[n];  
			nodes[n] = value;  
		}  
	}
}
