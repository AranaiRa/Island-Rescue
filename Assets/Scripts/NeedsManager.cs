﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NeedsManager : MonoBehaviour {

	public Slider 
		hunger, thirst, warmth;
	public float 
		hungerDecay, thirstDecay, warmthDecay;
	public Color
		containerDefaultColor,
		containerWarningColor;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		GaugeDecay (hunger, hungerDecay);
		GaugeDecay (thirst, thirstDecay);
		GaugeDecay (warmth, warmthDecay);

		GaugeBlink (hunger);
		GaugeBlink (thirst);
		GaugeBlink (warmth);
	}

	void GaugeDecay(Slider s, float decay){
		float f;
		
		f = s.value;
		f -= Time.deltaTime * (decay / 60);
		if(f < 0) f = 0;
		
		s.value = f;
	}

	void GaugeBlink(Slider s){
		float tf = Time.time;

		if (s.value < 0.10f) {
			if(tf % 1f < 0.5f)
				s.transform.parent.GetComponent<Image>().color = containerWarningColor;
			else
				s.transform.parent.GetComponent<Image>().color = containerDefaultColor;
		}
		else if (s.value < 0.25f) {
			if(tf % 1.5f < 0.5f)
				s.transform.parent.GetComponent<Image>().color = containerWarningColor;
			else
				s.transform.parent.GetComponent<Image>().color = containerDefaultColor;
		}
		else if (s.value < 0.5f) {
			if(tf % 2f < 0.5f)
				s.transform.parent.GetComponent<Image>().color = containerWarningColor;
			else
				s.transform.parent.GetComponent<Image>().color = containerDefaultColor;
		}
	}
}
