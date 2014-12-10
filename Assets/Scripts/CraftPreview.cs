using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// This class controls the UI element that informs the player of what their inventory can craft into.
/// </summary>
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
		//Alpha fade in/out effect
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

	/// <summary>
	/// Sets the image to be displayed, and activates the component.
	/// </summary>
	/// <param name="s">The image.</param>
	public void SetImage(Sprite s){
		imgSub.sprite = s;
		img.gameObject.SetActive (true);
	}

	/// <summary>
	/// Disable the UI component.
	/// </summary>
	public void Disable(){
		img.gameObject.SetActive (false);
	}
}
