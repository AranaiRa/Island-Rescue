using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class controls nodes for AI pathfinding.
/// </summary>
public class PathingNode : MonoBehaviour {

	public PathingNode[] attachedNodes;
	public GameObject disableAtRuntime;
	LineRenderer lines;
	static bool DebugShowPaths = false;

	// Use this for initialization
	void Start () {
		lines = GetComponent<LineRenderer> ();
		//Use a LineRenderer to show path connections if in debug mode.
		if(DebugShowPaths){
			for (int i=0; i<attachedNodes.Length; i++) {
				lines.SetPosition(i+i, attachedNodes[i].transform.position);
				lines.SetPosition(i+i+1, transform.position);
			}
		}
		else{
			disableAtRuntime.SetActive (false);
			lines.enabled = false;
		}
	}

	/// <summary>
	/// Choose a node attached to this one.
	/// </summary>
	/// <returns>The node.</returns>
	/// <param name="notThisOne">Never selects this node.</param>
	public PathingNode SelectNode(PathingNode notThisOne){
		List<PathingNode> nodes = new List<PathingNode> ();

		for (int i=0; i<attachedNodes.Length; i++) {

			if(!attachedNodes[i].Equals(notThisOne))
				nodes.Add(attachedNodes[i]);
		}

		return nodes[Random.Range (0, nodes.Count)];
	}
}
