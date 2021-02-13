using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	public CharacterController2D controller;
	public Animator animator;

	public float runSpeed = 40f;

	public float horizontalMove = 0f;
	bool jump = false;
	
	// Update is called once per frame
	void Update()
	{
		
		

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
		} else {
			float move = Input.GetAxisRaw("Horizontal");
			if (move == 0)
			{
				if (animator.enabled)
				{
					if (animator.GetCurrentAnimatorStateInfo(0).IsName("crash_run"))
					{
						animator.Rebind();
					}
				}
			}
		}
	}

	
	public void OnLanding()
	{
		
		
	}

	void FixedUpdate()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
		jump = false;
	}
}