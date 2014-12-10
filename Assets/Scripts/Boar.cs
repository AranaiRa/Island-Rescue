using UnityEngine;
using System.Collections;

/// <summary>
/// This class controls the boar entities.
/// </summary>
public class Boar : MonoBehaviour {

	public bool canMove = true;

	public float minRotationY = -90f;
	public float maxRotationY = 90f;
	public float walkSpeed = 16f;

	//Pathfinding info
	public PathingNode 
		start,
		prev,
		next;
	float distCutoff = 0.8f;
	float distLast = float.MaxValue, distThis = float.MaxValue;
	Vector3 heading;
	
	float speedY;
	float gravity = 3f;

	Animator animator;

	CharacterController controller;
	CollisionFlags collisionFlags;
	float rotationX = 0;
	float rotationY = 0;
	
	// Use this for initialization
	void Awake () {
		animator = GetComponent<Animator> ();
		controller = GetComponent<CharacterController> ();

		if(!canMove) StopMoving();
		else {
			transform.position = start.transform.position;
			next = start.SelectNode (start);
			prev = start;
			CalculateHeading ();
			FixRotation ();
		}

	}

	/// <summary>
	/// Stops the boar from moving and animating, sets it on its side.
	/// </summary>
	public void StopMoving(){
		canMove = false;
		animator.SetBool ("canMove", false);
		Vector3 rotation = this.transform.rotation.eulerAngles;
		Vector3 position = this.transform.position;
		rotation.z += 90;
		position.y += .5f;
		this.transform.rotation = Quaternion.Euler (rotation);
		this.transform.position = position;
	}
	
	void Update () {
		if(canMove && !Config.Paused){
			//Movement
			Vector3 move = heading * walkSpeed * Time.deltaTime;
			CalcSpeedY ();
			collisionFlags = controller.Move(new Vector3(move.x,speedY,move.z) * Time.deltaTime * walkSpeed);

			distLast = distThis;
			distThis = Vector3.Distance(transform.position, next.transform.position);

			//If the boar is within the cutoff distance or is moving away from the target, select a new node.
			if ((distThis < distCutoff) || (distLast < distThis)) {
				distThis = float.MaxValue;
				PathingNode sel = next.SelectNode(prev);
				prev = next;
				next = sel;
				CalculateHeading();
				FixRotation ();
			}
		}
	}

	/// <summary>
	/// Re-orients the boar to face its next pathfinding node.
	/// </summary>
	void FixRotation(){
		Vector3 diff = next.transform.position - prev.transform.position;
		transform.rotation = Quaternion.LookRotation(diff.normalized);
	}

	/// <summary>
	/// Calculates gravity.
	/// </summary>
	void CalcSpeedY(){
		if(IsGrounded()){
			speedY = 0;
		}else{
			speedY -= gravity * Time.deltaTime;
		}
	}

	/// <summary>
	/// Determines whether this instance is grounded.
	/// </summary>
	/// <returns><c>true</c> if this instance is grounded; otherwise, <c>false</c>.</returns>
	bool IsGrounded(){
		return ((collisionFlags & CollisionFlags.CollidedBelow) > 0); // really if it's 1
	}

	/// <summary>
	/// Raises the trigger enter event.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter(Collider other){
		if(other.gameObject.GetComponent<Collectable>() != null){
			if(other.gameObject.GetComponent<Collectable>().type == GameItem.Trap)
				StopMoving();
		}
	}

	/// <summary>
	/// Calculates the direction the boar is going to start moving in next.
	/// </summary>
	void CalculateHeading(){
		Vector3 dir = next.transform.position - prev.transform.position;
		heading = dir.normalized;
	}
}
