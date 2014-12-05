using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CraftPreview : MonoBehaviour {

	public byte 
		minAlpha = 50,
		maxAlpha = 150;
	public float phase;
	Image img, imgSub;

	void Start () {
		img = GetComponent<Image>();
		Transform[] transforms = this.GetComponentsInChildren<Transform>();
		imgSub = transforms [1].gameObject.GetComponent<Image>();

		Debug.Log (imgSub == img);

		this.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		float diff = (Time.time % phase) / phase;
		float rads = diff * Mathf.PI;

		float min = (minAlpha / 255f);
		float max = (maxAlpha / 255f);
		float range = max - min;

		Color c = img.color;
		float a = (Mathf.Sin (rads) * range) + (minAlpha / 255f);
		c.a = a;
		img.color = c;

		c = imgSub.color;
		c.a = a;
		imgSub.color = c;
	}

	public void SetImage(Sprite s){
		Debug.Log ("is imgsub a thing? " + (imgSub != null));
		imgSub.sprite = s;
		img.gameObject.SetActive (true);
	}

	public void Disable(){
		Debug.Log ("is img a thing? " + (imgSub != null));
		img.gameObject.SetActive (false);
	}
}
