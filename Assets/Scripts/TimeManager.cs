﻿using UnityEngine;
using System.Collections;

/// <summary>
/// This class controls the passage of time.
/// </summary>
public class TimeManager : MonoBehaviour {

	public GameObject player;
	public GameObject camp;
	public NeedsManager needs;
	public float 
		dayLength,
		currentTime;
	public Vector3 
		sunrise,
		noon,
		sunset;
	public Color[] interpColor;
	public float[] interpSteps;
	public float[] interpIntensity;

	private float
		dayStart,
		dayEnd,
		lerp,
		colorTargetLast,
		colorTargetNext;
	private int
		lastColorIndex;
	private Light light;

	// Use this for initialization
	void Start () {
		light = GetComponent<Light> ();
		NewDay ();
		needs.hunger.value = 1f;
		needs.thirst.value = 1f;
		needs.warmth.value = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		if(!Config.Paused){
			lerp = 1 - ((dayEnd - Time.time) / dayLength);

			if (lerp >= 1f) {
				NewDay ();
			}
			else {
				//Direction
				Vector3 rot;
				if (lerp <= 0.5f) {
					rot = Vector3.Lerp(sunrise, noon, lerp*2);
				}
				else {
					rot = Vector3.Lerp(noon, sunset, (lerp-0.5f)*2);
				}
				light.gameObject.transform.rotation = Quaternion.Euler (rot);

				//Colors & Intensity
				CheckColors ();
				float clerp = (lerp - colorTargetLast) / (colorTargetNext - colorTargetLast);
				light.color = Color.Lerp(interpColor[lastColorIndex], interpColor[lastColorIndex+1], clerp);


				light.intensity = Vector2.Lerp(new Vector2(interpIntensity[lastColorIndex], 0), new Vector2(interpIntensity[lastColorIndex+1], 0), clerp).x;
			}
		}
		else{
			dayEnd += Time.deltaTime;
		}
	}

	/// <summary>
	/// Convenience method to set the two color interpolation targets.
	/// </summary>
	void CheckColors(){
		for(int i=1;i<interpSteps.Length-1;i++){
			if(lerp < interpSteps[i]){
				colorTargetLast = interpSteps [i-1];
				colorTargetNext = interpSteps [i];
				lastColorIndex = i-1;
				break;
			}
		}
	}

	/// <summary>
	/// Controls the logic that happens at the start of a new day.
	/// </summary>
	void NewDay(){
		dayStart = Time.time;
		dayEnd = dayStart + dayLength;
		light.gameObject.transform.rotation = Quaternion.Euler (sunrise);
		//If the player has a tent built, move the player to camp and decay needs less severely.
		if (Config.hasTent) {
			player.transform.position = camp.transform.position;
			needs.hunger.value -= 0.25f;
			if(needs.hunger.value < 0.1f) needs.hunger.value = 0.1f;

			needs.thirst.value -= 0.25f;
			if(needs.thirst.value < 0.1f) needs.thirst.value = 0.1f;
			
			needs.warmth.value -= 0.25f / Config.inventory.InstancesOf(GameItem.Hide);
			if(needs.warmth.value < 0.1f) needs.warmth.value = 0.1f;
		}
		else{
			needs.hunger.value -= 0.45f;
			if(needs.hunger.value < 0.1f) needs.hunger.value = 0.1f;
			
			needs.thirst.value -= 0.45f;
			if(needs.thirst.value < 0.1f) needs.thirst.value = 0.1f;
			
			needs.warmth.value -= 0.60f / Config.inventory.InstancesOf(GameItem.Hide);
			if(needs.warmth.value < 0.1f) needs.warmth.value = 0.1f;
		}
		//If the player has survived three days, win.
		Config.numDaysSurvived++;
		if (Config.numDaysSurvived >= 3)
			GSM.SwitchToWin();
	}
}
