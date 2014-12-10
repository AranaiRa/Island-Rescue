using UnityEngine;
using System.Collections;

public class Pathfinder : MonoBehaviour {

	public PathingNode 
		start,
		prev,
		next;
	float speed = 4.0f;
	float distCutoff = 0.2f;
	float distLast = float.MaxValue, distThis = float.MaxValue;
	Vector3 heading;

	void Start () {
		transform.position = start.transform.position;
		next = start.SelectNode (start);
		prev = start;
		CalculateHeading ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += Time.deltaTime * heading * speed;
		float dist = Vector3.Distance (transform.position, next.transform.position);
		if (dist < distCutoff) {
			PathingNode sel = next.SelectNode(prev);
			prev = next;
			next = sel;
			CalculateHeading();
		}
	}

	void CalculateHeading(){
		Vector3 dir = next.transform.position - prev.transform.position;
		heading = dir.normalized;
		Debug.Log ("heading=" + heading);
	}
}
