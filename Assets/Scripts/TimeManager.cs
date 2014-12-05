using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour {

	public float 
		dayLength,
		currentTime;
	public Vector3 
		sunrise,
		noon,
		sunset;
	public Color[] colorInterp;
	public float[] interpSteps;

	private float
		dayStart,
		dayEnd,
		lerp;
	private Light light;

	// Use this for initialization
	void Start () {
		light = GetComponent<Light> ();
		NewDay ();
	}
	
	// Update is called once per frame
	void Update () {
		lerp = 1 - ((dayEnd - Time.time) / dayLength);

		if (lerp >= 1f) {
			NewDay ();
			//TODO: End of day shit
		}
		else {
			Vector3 rot;
			if (lerp <= 0.5f) {
				rot = Vector3.Lerp(sunrise, noon, lerp*2);
			}
			else {
				rot = Vector3.Lerp(noon, sunset, (lerp-0.5f)*2);
			}
			light.gameObject.transform.rotation = Quaternion.Euler (rot);
		}
	}

	void NewDay(){
		dayStart = Time.time;
		dayEnd = dayStart + dayLength;
		light.gameObject.transform.rotation = Quaternion.Euler (sunrise);
	}
}
