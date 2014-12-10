using UnityEngine;
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
		Config.needs = this;
	}
	
	// Update is called once per frame
	void Update () {

		GaugeDecay (hunger, hungerDecay);
		GaugeDecay (thirst, thirstDecay);
		
		int numHides = Config.inventory.InstancesOf (GameItem.Hide);
		if (Config.inventory.GetNearFire()) {
			warmth.value += Time.deltaTime * (0.08f + (0.5f * numHides));
		}
		else {
			if(numHides > 0)
				GaugeDecay (warmth, warmthDecay / 2);
			else
				GaugeDecay (warmth, warmthDecay);
		}

		GaugeBlink (hunger);
		GaugeBlink (thirst);
		GaugeBlink (warmth);
	}

	void GaugeDecay(Slider s, float decay){
		float f;
		
		f = s.value;
		f -= Time.deltaTime / decay;
		if(f < 0) f = 0;
		if(f > 1) f = 1;
		
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
		else
			s.transform.parent.GetComponent<Image>().color = containerDefaultColor;
	}
}
