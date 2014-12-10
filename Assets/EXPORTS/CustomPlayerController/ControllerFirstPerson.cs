using UnityEngine;
using System.Collections;

public class ControllerFirstPerson : MonoBehaviour {

	public Camera cam;
	public float sensitivityX = .1f;
	public float sensitivityY = .1f;
	
	public float minRotationY = -90f;
	public float maxRotationY = 90f;
	public float walkSpeed = 3f;
	
	float speedY;
	float gravity = 3f;

	CharacterController controller;
	CollisionFlags collisionFlags;
	float rotationX = 0;
	float rotationY = 0;

	// Use this for initialization
	void Awake () {
		if(cam == null && Camera.main) cam = Camera.main;
		controller = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		CalcSpeedY ();
		GetMouseInput ();

		transform.eulerAngles = new Vector3 (0, rotationX, 0);
		cam.transform.localEulerAngles = new Vector3 (rotationY, 0, 0);
		
		float forward = Input.GetAxis ("Vertical");
		float right = Input.GetAxis ("Horizontal");

		Vector3 move = forward * transform.forward + right * transform.right;
		if(move.magnitude > 1) move = move.normalized;

		move.y += speedY;

		collisionFlags = controller.Move(move * Time.deltaTime * walkSpeed);
	}

	void GetMouseInput(){
		rotationX += Input.GetAxis ("Mouse X") * sensitivityX;
		rotationY -= Input.GetAxis ("Mouse Y") * sensitivityY;
		
		rotationY = Mathf.Clamp (rotationY, minRotationY, maxRotationY); // clamp range between -90 and 90 degrees
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
}
