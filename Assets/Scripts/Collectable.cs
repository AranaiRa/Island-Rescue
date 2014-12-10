using UnityEngine;
using System.Collections;

/// <summary>
/// This class controls collectables that can be picked up.
/// </summary>
public class Collectable : MonoBehaviour {
	public GameItem type = GameItem.NONE;
	//Model to swap to once picked up, if any.
	public GameObject secondaryModel;
	//What you need in your main inventory slot to pick up, if any
	public GameItem harvestTool = GameItem.NONE;
	bool harvested = false;

	/// <summary>
	/// Remove this instance and add its associated item to your inventory.
	/// </summary>
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
