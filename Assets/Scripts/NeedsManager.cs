using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// This class manages the three need gauges the player must keep track of.
/// </summary>
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
		if(!Config.Paused){
			GaugeDecay (hunger, hungerDecay);
			GaugeDecay (thirst, thirstDecay);

			//Decay warmth slower if the player is carrying animal hides.
			int numHides = Config.inventory.InstancesOf (GameItem.Hide);
			if (Config.inventory.GetNearFire()) {
				warmth.value += Time.deltaTime * (0.08f + (0.04f * numHides));
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

			if(hunger.value <= 0) GSM.SwitchToLose();
			if(thirst.value <= 0) GSM.SwitchToLose();
			if(warmth.value <= 0) GSM.SwitchToLose();
		}
	}

	/// <summary>
	/// Decay the amount in the specified gauge based on time.
	/// </summary>
	/// <param name="s">Which gauge.</param>
	/// <param name="decay">Decay value.</param>
	void GaugeDecay(Slider s, float decay){
		float f;
		
		f = s.value;
		f -= Time.deltaTime / decay;
		if(f < 0) f = 0;
		if(f > 1) f = 1;
		
		s.value = f;
	}

	/// <summary>
	/// Causes the specified gauge to blink if it's below a certain threshold.
	/// </summary>
	/// <param name="s">Which gauge.</param>
	void GaugeBlink(Slider s){
		float tf = Time.time;

		//Fast blink
		if (s.value < 0.10f) {
			if(tf % 1f < 0.5f)
				s.transform.parent.GetComponent<Image>().color = containerWarningColor;
			else
				s.transform.parent.GetComponent<Image>().color = containerDefaultColor;
		}
		//Medium blink
		else if (s.value < 0.25f) {
			if(tf % 1.5f < 0.5f)
				s.transform.parent.GetComponent<Image>().color = containerWarningColor;
			else
				s.transform.parent.GetComponent<Image>().color = containerDefaultColor;
		}
		//Slow blink
		else if (s.value < 0.5f) {
			if(tf % 2f < 0.5f)
				s.transform.parent.GetComponent<Image>().color = containerWarningColor;
			else
				s.transform.parent.GetComponent<Image>().color = containerDefaultColor;
		}
		//No blink
		else
			s.transform.parent.GetComponent<Image>().color = containerDefaultColor;
	}
}
