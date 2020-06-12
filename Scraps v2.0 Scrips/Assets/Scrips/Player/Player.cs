using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] Rigidbody rigidbody;

	[SerializeField] float moveSpeed = 3.0f;
	[SerializeField] float jumpPower = 5.0f;

	public Interactable selectedInteractable;
	public OwnerType ownerType;

	private float fallMultiplier = 1.0f;
	private float fastFallMultiplier = 2.0f;

	private bool canMove = true;
	private bool grounded = true;
	private bool canDoubleJump = true;

	private Vector3 velocity;

	public void SetPlayerActive(bool state)
	{
		canMove = state;

		if (canMove == false) {
			rigidbody.velocity = Vector3.zero;
		}
	}

	public void Move(float move)
	{
		velocity = new Vector3(move, 0, 0);
	}

	public void Jump()
	{
		if (canMove == false) return;

		if (Grounded())
		{
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpPower, 0);
			canDoubleJump = true;
		}
		else if (canDoubleJump)
		{
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpPower, 0);
			canDoubleJump = false;
		}
	}

	public bool Grounded()
	{
		if (rigidbody.velocity.y == 0)
		{
			canDoubleJump = true;
			return true;
		}

		return false;
	}

	// Update is called once per frame
	void Update()
	{
		if (canMove == false) return;

		// Move
		rigidbody.MovePosition(rigidbody.position + (velocity * moveSpeed * Time.deltaTime));

		// Change direction
		if (velocity.x != 0)
		{
			float direction = velocity.x > 0 ? 180 : 0;
			transform.eulerAngles = new Vector3(0, direction, 0);
		}

		// Interact
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			if (selectedInteractable != null) {
				selectedInteractable.Interact(this);
			}
		}
	}

	private void FixedUpdate()
	{
		if (canMove == false) return;

		// If were falling..
		// If our normal fall Multiplier
		if (rigidbody.velocity.y < 0)
		{
			rigidbody.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.fixedDeltaTime;
		}

		// But if were still asending
		// And we let go of the space bar..
		// Use our fast fall Multiplier
		else if (rigidbody.velocity.y > 0 && !Input.GetButton("Jump"))
		{
			rigidbody.velocity += Vector3.up * Physics.gravity.y * fastFallMultiplier * Time.fixedDeltaTime;
		}
	}
}
