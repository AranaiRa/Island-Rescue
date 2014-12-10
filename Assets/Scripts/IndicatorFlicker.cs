using UnityEngine;
using System.Collections;

public class IndicatorFlicker : MonoBehaviour {

	public float min = 0.15f;
	public float max = 0.75f;
	public float rate = 0.00625f;
	bool up = true;
	Material mat;

	// Use this for initialization
	void Start () {
		mat = this.renderer.material;
	}
	
	// Update is called once per frame
	void Update () {
		Color c = mat.GetColor ("_TintColor");
		if (up) {
			c.a += rate;
			if(c.a > max){
				c.a = max;
				up = false;
			}
		}
		else {
			c.a -= rate;
			if(c.a < min){
				c.a = min;
				up = true;
			}
		}
		mat.SetColor ("_TintColor", c);
	}
}
