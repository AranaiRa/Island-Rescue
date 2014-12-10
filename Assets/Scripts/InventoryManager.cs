using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryManager : MonoBehaviour {

	public CraftPreview preview; 
	public ItemSelector selector;
	public Image[] itemSlots;
	public Sprite 
		imgDefault, imgLog, imgBranch, imgTwig, imgFlint, 
		imgRock, imgMushroom, imgOnion, imgMeat, imgMeatCooked, 
		imgHide, imgRadio;
	bool isNearFire = false;
	GameItem[] itemsHeld = new GameItem[9];
	GameItem recipeItem;
	int recipeNum;
	float 
		inputDelay = 0.4f,
		inputTicker = 0;

	// Use this for initialization
	void Start () {
		foreach (Image i in itemSlots) {
			i.gameObject.SetActive(false);
		}

		Config.inventory = this;
		PickUp (GameItem.Radio);
	}
	
	// Update is called once per frame
	void Update () {
		inputTicker -= Time.deltaTime;
		if(inputTicker < 0) inputTicker = 0;

		if (Input.GetKeyDown (Config.Craft) && inputTicker <= 0) {
			if(recipeItem != GameItem.NONE){
				itemsHeld = new GameItem[9];
				for(int i=0;i<recipeNum;i++){ //Number of items to add
					itemsHeld[i] = recipeItem;
				}
				CheckRecipe();
			}
			UpdateItemSlots ();
			inputTicker = inputDelay;
		}

		GameItem held = GetHeldObject ();
		if (Input.GetKeyDown (Config.Use) && held != GameItem.NONE) {
			if(held == GameItem.Onion && Config.needs.hunger.value < 0.85f){
				Config.needs.hunger.value += 0.15f;
				Config.needs.thirst.value += 0.15f;
				SetItemSlot (selector.GetIndex(), GameItem.NONE, true);
			}
			else if(held == GameItem.Mushroom && Config.needs.hunger.value < 0.88f){
				Config.needs.hunger.value += 0.18f;
				itemsHeld[selector.GetIndex()] = GameItem.NONE;
				SetItemSlot (selector.GetIndex(), GameItem.NONE, true);
			}
			else if(held == GameItem.Meat && Config.needs.hunger.value < 0.68f){
				Config.needs.hunger.value += 0.32f;
				itemsHeld[selector.GetIndex()] = GameItem.NONE;
				SetItemSlot (selector.GetIndex(), GameItem.NONE, true);
			}
			else if(held == GameItem.MeatCooked && Config.needs.hunger.value < 0.75f){
				Config.needs.hunger.value += 0.50f;
				itemsHeld[selector.GetIndex()] = GameItem.NONE;
				SetItemSlot (selector.GetIndex(), GameItem.NONE, true);
			}
		}
	}

	public bool PickUp(GameItem gi){
		bool changed = false;
		if(gi != GameItem.NONE){
			for(int i=0;i<9;i++){
				if(itemsHeld[i] == GameItem.NONE) {
					itemsHeld[i] = gi;
					changed = true;
					SetItemSlot(i, gi);
					break;
				}
			}
			return changed;
		}
		else return false;
	}

	public void Drop(){
		GameItem gi = itemsHeld [selector.GetIndex ()];
		if (gi != GameItem.NONE) {
			SetItemSlot(selector.GetIndex(), GameItem.NONE);
		}
	}
	
	public void RemoveFirstInstanceOf(GameItem gi){
		if (gi != GameItem.NONE) {
			for(int i=0;i<9;i++){
				if (itemsHeld [i] == gi) {
					SetItemSlot (i, GameItem.NONE);
					break;
				}
			}
		}
	}

	public int InstancesOf(GameItem gi){
		int i = 0;
		foreach (GameItem item in itemsHeld) {
			if(item == gi) i++;
		}
		return i;
	}

	public int TotalItemsHeld(){
		int i = 0;
		foreach (GameItem item in itemsHeld) {
			if(item != GameItem.NONE) i++;
		}
		return i;
	}

	public GameItem GetHeldObject(){
		return itemsHeld[selector.GetIndex()];
	}

	void SetItemSlot(int index, GameItem item, bool inform = true) {
		index = Mathf.Clamp (index, 0, 8);
		Sprite s = GetImageFromGameItem(item);

		if(item != GameItem.NONE){
			itemSlots [index].sprite = s;
			itemSlots [index].gameObject.SetActive (true);
		}
		else{
			itemSlots [index].gameObject.SetActive (false);
		}

		itemsHeld [index] = item;
		if(inform) CheckRecipe ();
	}

	void UpdateItemSlots(){
		for (int i=0;i<9;i++) {
			SetItemSlot(i,itemsHeld[i],false);
		}
		CheckRecipe ();
	}

	void ClearItemSlot(int index){
		itemSlots [index].gameObject.SetActive (false);
	}

	public void SetNearFire(bool b){
		isNearFire = b;
		CheckRecipe ();
	}

	public bool GetNearFire(){
		return isNearFire;
	}

	void CheckRecipe(){
		GameItem recipe = GameItem.NONE;
		int num = 0;

		if (TotalItemsHeld() == 1) {

			if(InstancesOf(GameItem.Log) == 1){
				recipe = GameItem.Branch;
				num = 3;
			}

			else if(InstancesOf(GameItem.Branch) == 1){
				recipe = GameItem.Twig;
				num = 3;
			}

			else if(InstancesOf (GameItem.Meat) == 1 && isNearFire){
				recipe = GameItem.MeatCooked;
				num = 1;
			}
		}

		recipeItem = recipe;
		recipeNum = num;
		
		if (recipeItem != GameItem.NONE) {
			preview.SetImage(GetImageFromGameItem(recipeItem));
		}
		else{
			preview.Disable();
		}
	}

	public GameItem GetActiveItem(){
		return itemsHeld [selector.GetIndex ()];
	}
	
	Sprite GetImageFromGameItem(GameItem gi){
		Sprite s;
		switch (gi) {
		case GameItem.Log:
			s = imgLog;
			break;
		case GameItem.Branch:
			s = imgBranch;
			break;
		case GameItem.Twig:
			s = imgTwig;
			break;
		case GameItem.Flint:
			s = imgFlint;
			break;
		case GameItem.Rock:
			s = imgRock;
			break;
		case GameItem.Mushroom:
			s = imgMushroom;
			break;
		case GameItem.Onion:
			s = imgOnion;
			break;
		case GameItem.Meat:
			s = imgMeat;
			break;
		case GameItem.MeatCooked:
			s = imgMeatCooked;
			break;
		case GameItem.Hide:
			s = imgHide;
			break;
		case GameItem.Radio:
			s = imgRadio;
			break;
		default:
			s = imgDefault;
			break;
		}
		return s;
	}
}
