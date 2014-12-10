using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConstructionSite : MonoBehaviour {

	public string constructionName;
	public GameItem[] matTypes;
	public int[] matAmounts;
	public GameObject messageWindow;
	public GameObject completedConstruction;
	public GameObject indicator;
	public Stockpile[] piles; 
	bool inBuildArea = false;

	// Use this for initialization
	void Start () {
		messageWindow.SetActive (false);
		completedConstruction.SetActive (false);
		indicator.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (Config.Deposit) && inBuildArea) {
			bool hasAllMats = true;
			// Check to see if all materials are held or in stockpiles
			for(int i=0;i<matTypes.Length;i++){
				GameItem gi = matTypes[i];
				Stockpile pile = new Stockpile();
				int amtNeeded = matAmounts[i];
				int amtHeld = Config.inventory.InstancesOf(gi);
				int amtPile = 0;
				for(int j=0;j<piles.Length;j++){
					if(piles[j].type == gi){
						amtPile = piles[j].GetNumItems();
						pile = piles[j];
						break;
					}
				}
				if(amtHeld + amtPile < amtNeeded){
					hasAllMats = false;
					break;
				}
			}
			if(hasAllMats){
				for(int i=0;i<matTypes.Length;i++){
					GameItem gi = matTypes[i];
					Stockpile pile = new Stockpile();

					int amtNeeded = matAmounts[i];
					int amtHeld = Config.inventory.InstancesOf(gi);
					int amtPile = 0;
					for(int j=0;j<piles.Length;j++){
						if(piles[j].type == gi){
							amtPile = piles[j].GetNumItems();
							pile = piles[j];
							break;
						}
					}

					while(amtHeld > 0 && amtNeeded > 0){
						Config.inventory.RemoveFirstInstanceOf(gi);
						amtHeld--;
						amtNeeded--;
					}
					while(amtPile > 0 && amtNeeded > 0){
						pile.RemoveItemFromPile();
						amtPile--;
						amtNeeded--;
					}
				}

				Construct ();
			}
		}
	}
	
	void OnTriggerEnter(Collider other){
		UpdateText ();
		inBuildArea = true;
	}
	
	void OnTriggerExit(Collider other){
		messageWindow.SetActive (false);
		inBuildArea = false;
	}

	void Construct(){
		if (constructionName == "a tent")
						Config.hasTent = true;
		completedConstruction.SetActive (true);
		indicator.SetActive (false);
		messageWindow.SetActive (false);
		this.gameObject.SetActive (false);
	}

	void UpdateText(){
		string s = "<size=18><b>Construction Site</b></size>\n";
		s += "This looks like a good place to build "+constructionName+".\nLooks like you'll need: ";

		for(int i=0;i<matTypes.Length;i++){
			if(i > 0) s += ", ";
			s += GameItemStrings.Get(matTypes[i])+" x"+matAmounts[i];
		}

		Text t = messageWindow.transform.GetChild (0).GetComponent<Text> ();
		t.text = s;
		messageWindow.SetActive (true);
	}
}
