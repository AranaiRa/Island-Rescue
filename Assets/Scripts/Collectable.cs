using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour {
	public GameItem type = GameItem.NONE;
	public GameObject secondaryModel;
	public GameItem harvestTool = GameItem.NONE;
	bool harvested = false;

	public void Remove(){
		if(secondaryModel == null){
			if(harvestTool == Config.inventory.GetHeldObject() || harvestTool == GameItem.NONE)
				Destroy (this.gameObject);
		}
		else{
			if(harvestTool == Config.inventory.GetHeldObject() || harvestTool == GameItem.NONE){
				type = GameItem.NONE;
				harvested = true;
				secondaryModel.SetActive(true);
				Destroy(this.gameObject);
			}
		}
	}
}
