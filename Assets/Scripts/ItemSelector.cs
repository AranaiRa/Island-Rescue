using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemSelector : MonoBehaviour {
	private Vector3[] slots = 
	{
		new Vector3 (20, 190, 0),
		new Vector3 (20, 150, 0),
		new Vector3 (20, 110, 0),
		new Vector3 (20, 70, 0),
		new Vector3 (25, 25, 0),
		new Vector3 (70, 20, 0),
		new Vector3 (110, 20, 0),
		new Vector3 (150, 20, 0),
		new Vector3 (190, 20, 0)
	};
	private float 
		transitionTime = 0.4f,
		tTicker;
	private bool 
		transitioning = false,
		left = false;
	public RectTransform[] panels;
	private int index = 4;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (transitioning) {
			tTicker -= Time.deltaTime;
			float pd = 1 - (tTicker / transitionTime);

			for(int i=0;i<panels.Length;i++) {
				panels[i].position = Vector3.Lerp(panels[i].position, slots[i], pd);
				if(i==4){
					float v = 40 + (pd * 10);
					if(v > 50) v = 50;
					panels[i].sizeDelta = new Vector2(v, v);
				}
				else if(i==3 || i==5){
					float v = 50 - (pd * 10);
					if(v > 40) v = 40;
					panels[i].sizeDelta = new Vector2(v, v);
				}
			}

			if(tTicker <= 0)
				transitioning = false;
		}
		else {
			if(Input.GetKey(Config.NextItem)){
				transitioning = true;
				left = false;
				tTicker = transitionTime;
				ShiftItems ();
			}
			else if(Input.GetKey(Config.PrevItem)){
				transitioning = true;
				left = true;
				tTicker = transitionTime;
				ShiftItems ();
			}
		}
	}

	void ShiftItems() {

		RectTransform[] temp = (RectTransform[])panels.Clone();

		if (left) {
			panels[0] = temp[8];
			panels[1] = temp[0];
			panels[2] = temp[1];
			panels[3] = temp[2];
			panels[4] = temp[3];
			panels[5] = temp[4];
			panels[6] = temp[5];
			panels[7] = temp[6];
			panels[8] = temp[7];
			index --;
			if(index < 0) index = 8;
		}
		else {
			panels[0] = temp[1];
			panels[1] = temp[2];
			panels[2] = temp[3];
			panels[3] = temp[4];
			panels[4] = temp[5];
			panels[5] = temp[6];
			panels[6] = temp[7];
			panels[7] = temp[8];
			panels[8] = temp[0];
			index ++;
			if(index > 8) index = 0;
		}

		tTicker = transitionTime;
	}

	public int GetIndex(){
		return index;
	}
}
