using UnityEngine;
using System.Collections;

public class ConstructionSite : MonoBehaviour {

	public string constructionName;
	public GameItem[] matTypes;
	public int[] matAmounts;


	// Use this for initialization
	void Start () {
		UpdateText ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other){
		UpdateText ();
	}
	
	void OnTriggerExit(Collider other){

	}

	void UpdateText(){
		string s = "This looks like a good place to build "+constructionName+".\nLooks like you'll need:";
		for(int i=0;i<matTypes.Length;i++){
			s += "\n"+matTypes[i]+" x "+matAmounts[i];
		}

		Debug.Log (s);

	}
}
