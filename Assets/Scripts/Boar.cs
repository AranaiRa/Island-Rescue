using UnityEngine;
using System.Collections;

public class Boar : MonoBehaviour {

	public bool canMove = true;

	public float minRotationY = -90f;
	public float maxRotationY = 90f;
	public float walkSpeed = 16f;

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

		transform.position = start.transform.position;
		next = start.SelectNode (start);
		prev = start;
		CalculateHeading ();
		FixRotation ();
	}

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
	
	// Update is called once per frame
	void Update () {
		if(canMove){
			Vector3 move = heading * walkSpeed * Time.deltaTime;
			CalcSpeedY ();
			collisionFlags = controller.Move(new Vector3(move.x,speedY,move.z) * Time.deltaTime * walkSpeed);

			distLast = distThis;
			/*distThis = Vector2.Distance (
				new Vector2(transform.position.x, transform.position.z), 
				new Vector2(next.transform.position.x, next.transform.position.z));*/
			distThis = Vector3.Distance(transform.position, next.transform.position);

			Debug.Log ("distThis:"+distThis+"  distLast:"+distLast);
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

	void FixRotation(){
		Vector3 diff = next.transform.position - prev.transform.position;
		transform.rotation = Quaternion.LookRotation(diff.normalized);
	}
	
	void CalcSpeedY(){
		if(IsGrounded()){
			speedY = 0;
		}else{
			speedY -= gravity * Time.deltaTime;
		}
	}
	
	bool IsGrounded(){
		return ((collisionFlags & CollisionFlags.CollidedBelow) > 0); // really if it's 1
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.GetComponent<Collectable>() != null){
			if(other.gameObject.GetComponent<Collectable>().type == GameItem.Trap)
				StopMoving();
		}
	}
	
	void CalculateHeading(){
		Vector3 dir = next.transform.position - prev.transform.position;
		heading = dir.normalized;
	}
}
