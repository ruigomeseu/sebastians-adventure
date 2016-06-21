using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

	public float walkSpeed = 20.0f;
	public float runSpeed = 8f;
	public float sprintSpeed = 2.0f;
	public float flySpeed = 4.0f;

	public float turnSmoothing = 3.0f;
	public float aimTurnSmoothing = 15.0f;
	public float speedDampTime = 0.1f;

	public float jumpHeight = 5.0f;
	public float jumpCooldown = 1.0f;

	private float timeToNextJump = 0;

	private float speed;

	private Vector3 lastDirection;

	public Animator anim;
	private int speedFloat;
	private int jumpBool;
	private int hFloat;
	private int vFloat;
	private int aimBool;
	private int flyBool;
	private int groundedBool;
	private Transform cameraTransform;
	public int pickingBool;
	public int pushingBool;
    public int throwingBool;

	private float h;
	private float v;

	private bool aim;

	private bool run;
	public bool sprint;

	private bool isMoving;

	// fly
	private bool fly = false;
	private float distToGround;
	private float sprintFactor;

	public Vector3 currentDirection;

	private Stamina staminaScript;

	//rigidbody
  	private Rigidbody rigidbodyObject;

	public bool IsMovementActivated = true;

	private WalkingSoundsController _walkingSoundsController;

	void Awake()
	{
		_walkingSoundsController = GetComponent<WalkingSoundsController> ();
		anim = GetComponent<Animator> ();
		cameraTransform = Camera.main.transform;

		pickingBool = Animator.StringToHash("Picking");
		speedFloat = Animator.StringToHash("Speed");
		jumpBool = Animator.StringToHash("Jump");
		hFloat = Animator.StringToHash("H");
		vFloat = Animator.StringToHash("V");
		aimBool = Animator.StringToHash("Aim");
		// fly
		flyBool = Animator.StringToHash ("Fly");
		groundedBool = Animator.StringToHash("Grounded");

		pushingBool = Animator.StringToHash("Pushing");
        throwingBool = Animator.StringToHash("Throwing");

		distToGround = GetComponent<Collider>().bounds.extents.y;
		sprintFactor = sprintSpeed / runSpeed;

		//stamina
		staminaScript = GetComponent<Stamina>();


        //rigidbody
        rigidbodyObject = GetComponent<Rigidbody>();

    }

	bool IsGrounded() {
		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
	}

	bool AnimatorIsPlaying(){
		Debug.Log (anim.GetCurrentAnimatorStateInfo (0).normalizedTime);
		return			anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9;
	}


	void OnCollisionExit(Collision collisionInfo) {
		if(collisionInfo.collider.tag == "PushRock")
			anim.SetBool (pushingBool, false);
	}

	void Update()
	{
		// fly
		if (IsMovementActivated) {
			h = Input.GetAxis("Horizontal");
			v = Input.GetAxis("Vertical");
			run = Input.GetButton ("Run");
			sprint = Input.GetButton ("Sprint");
			isMoving = Mathf.Abs(h) > 0.1 || Mathf.Abs(v) > 0.1;


			if (Input.GetButton ("Fire1")) {
				GetComponent<ThrowObjectController> ().ThrowRock ();
			}
		} else {
			isMoving = false;
		}


	}

	void FixedUpdate()
	{
		anim.SetBool (aimBool, IsAiming());
		anim.SetFloat(hFloat, h);
		anim.SetFloat(vFloat, v);

		// Fly
		anim.SetBool (flyBool, fly);
		rigidbodyObject.useGravity = !fly;
		anim.SetBool (groundedBool, IsGrounded ());

		if (!IsMovementActivated) {
			h = 0; v = 0; run = false; sprint = false;
		}
		MovementManagement (h, v, run, sprint);
		JumpManagement ();

	}


	void JumpManagement()
	{
		if (rigidbodyObject.velocity.y < 10) // already jumped
		{
			anim.SetBool (jumpBool, false);
			if(timeToNextJump > 0)
				timeToNextJump -= Time.deltaTime;
		}
		if (Input.GetButtonDown ("Jump"))
		{
			anim.SetBool(jumpBool, true);
			if(speed > 0 && timeToNextJump <= 0 && !aim)
			{
				rigidbodyObject.velocity = new Vector3(0, jumpHeight, 0);
				timeToNextJump = jumpCooldown;
			}
		}
	}

	void MovementManagement(float horizontal, float vertical, bool running, bool sprinting)
	{
		float staminaRate = 0;
		if(isMoving && !anim.GetBool(throwingBool))
		{
			if(sprinting && staminaScript.getCurrentStamina() > 0)
			{
				speed = sprintSpeed;
				staminaRate = speed / 300f;
            }
			else
			{
				speed = walkSpeed;
				staminaRate = -0.005f;
			}

			anim.SetFloat(speedFloat, speed, speedDampTime, Time.deltaTime);
			currentDirection = Rotating (horizontal, vertical) * speed;
			currentDirection.y = GetComponent<Rigidbody> ().velocity.y;
			GetComponent<Rigidbody> ().velocity =  currentDirection ;
		}
		else
		{
			GetComponent<Rigidbody> ().velocity = new Vector3(-0, GetComponent<Rigidbody> ().velocity.y, 0);
			_walkingSoundsController.StopWalkingSound ();
			speed = 0f;
			anim.SetFloat(speedFloat, 0f);

			staminaRate = -0.01f;
		}

		staminaScript.changeStamina(staminaRate);
	}

	Vector3 Rotating (float horizontal, float vertical)
	{
		Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
		if (!fly)
			forward.y = 0.0f;
		forward = forward.normalized;

		Vector3 right = new Vector3(forward.z, 0, -forward.x);

		Vector3 targetDirection;

		float finalTurnSmoothing;

		if(IsAiming())
		{
			targetDirection = forward;
			finalTurnSmoothing = aimTurnSmoothing;
		}
		else
		{
			targetDirection = forward * vertical + right * horizontal;
			finalTurnSmoothing = turnSmoothing;
		}

		if((isMoving && targetDirection != Vector3.zero) || IsAiming())
		{
			Quaternion targetRotation = Quaternion.LookRotation (targetDirection, Vector3.up);
			// fly
			if (fly)
				targetRotation *= Quaternion.Euler (90, 0, 0);

			Quaternion newRotation = Quaternion.Slerp(rigidbodyObject.rotation, targetRotation, finalTurnSmoothing * Time.deltaTime);
			rigidbodyObject.MoveRotation (newRotation);
			lastDirection = targetDirection;
		}
		//idle - fly or grounded
		if(!(Mathf.Abs(h) > 0.9 || Mathf.Abs(v) > 0.9))
		{
			Repositioning();
		}

		return targetDirection;
	}

	private void Repositioning()
	{
		Vector3 repositioning = lastDirection;
		if(repositioning != Vector3.zero)
		{
			repositioning.y = 0;
			Quaternion targetRotation = Quaternion.LookRotation (repositioning, Vector3.up);
			Quaternion newRotation = Quaternion.Slerp(rigidbodyObject.rotation, targetRotation, turnSmoothing * Time.deltaTime);
			rigidbodyObject.MoveRotation (newRotation);
		}
	}

	public bool IsFlying()
	{
		return fly;
	}

	public bool IsAiming()
	{
		return aim && !fly;
	}

	public bool isSprinting()
	{
		return sprint && (isMoving);
	}
}
