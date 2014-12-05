﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Stockpile : MonoBehaviour {

	public GameItem type;
	public GameObject[] visualObjects;
	public int[] visualThresholds;
	private int numItems;
	public GameObject rootCanvas;
	public GameObject messageWindow;

	private float
		addMatCD = 1.5f, 
		addMatTicker;
	private bool isInZone = false;

	// Use this for initialization
	void Start () {
		messageWindow.SetActive (false);
		foreach(GameObject go in visualObjects){
			go.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Add item to stockpile. Item removal cannot be done manually at this time.
		if (Input.GetKey (Config.Deposit) && addMatTicker == 0 && isInZone) {
			addMatTicker = addMatCD;
			if(Config.inventory.InstancesOf(type) > 0){
				AddItemToPile();
				Config.inventory.RemoveFirstInstanceOf(type);
			}

			Debug.Log (visualThresholds[visualThresholds.Length-1]);
			if(numItems <= visualThresholds[visualThresholds.Length-1])
			for(int i=0;i<visualThresholds.Length;i++){
				if(visualThresholds[i]==numItems){
					visualObjects[i].SetActive(true);
					break;
				}
			}
		}

		addMatTicker -= Time.deltaTime;
		if(addMatTicker < 0) addMatTicker = 0;
	}

	void OnTriggerEnter(Collider other){
		messageWindow.SetActive (true);
		isInZone = true;
		UpdateText ();
	}

	void OnTriggerExit(Collider other){
		messageWindow.SetActive (false);
		isInZone = false;
	}

	void AddItemToPile(){
		numItems ++;

		if(numItems <= visualThresholds[visualThresholds.Length-1])
		for(int i=0;i<visualThresholds.Length;i++){
			if(visualThresholds[i]==numItems){
				visualObjects[i].SetActive(true);
				break;
			}
		}

		UpdateText();
	}

	bool RemoveItemFromPile(){
		bool op = false;
		if (numItems > 0) {
			numItems --;

			if(numItems <= visualThresholds[visualThresholds.Length-1])
			for(int i=0;i<visualThresholds.Length;i++){
				if(visualThresholds[i]==numItems){
					visualObjects[i].SetActive(true);
					break;
				}
			}
			
			UpdateText();

			op = true;
		}
		return op;
	}
	
	void UpdateText() {
		if(messageWindow != null){
			string s = "<b><size=18>"+GameItemStrings.Get(type) + " Stockpile";
			
			if (numItems == 0) s += " (Empty)";
			else s += " ("+numItems+")";
			
			s += "</size></b>\nPress <i>TAB</i> to deposit.";
			
			messageWindow.GetComponentInChildren<Text>().text = s;
		}
	}
}
