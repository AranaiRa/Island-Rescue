using UnityEngine;
using System.Collections;

/// <summary>
/// Effect class to cause a point light to flicker, emulating candlelight.
/// </summary>
public class LightFlicker : MonoBehaviour {

	Light light;
	public float BaseIntensity, IntensityRange, FlickerTimeMin, FlickerTimeMax;
	float ticker, lerpTimer;
	Vector2 start, end;

	// Use this for initialization
	void Start () {
		light = GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void Update () {
		ticker -= Time.deltaTime;
		if (ticker <= 0) {
			ticker = Random.Range(FlickerTimeMin, FlickerTimeMax);
			lerpTimer = ticker;
			start = new Vector2(light.intensity,0);
			end = new Vector2(BaseIntensity+Random.Range(-IntensityRange, IntensityRange),0);
		}
		light.intensity = Vector2.Lerp (start, end, (lerpTimer - ticker) / lerpTimer).x;
	}
}
