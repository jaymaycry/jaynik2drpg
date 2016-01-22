using UnityEngine;
using System.Collections;

/// <summary>
/// Behaviour of player character
/// </summary>
public class SimpleCharacterWalk : MonoBehaviour {
	public Animator animator;
	public float speed = 3f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float horizontalInput = Input.GetAxis(Constants.Input_Horizontal);
        animator.ResetTrigger("StartAttack");   // TEMPORARY UNTIL RUN ATTACK IS IMPLEMENTED

        if (Mathf.Abs(horizontalInput) > Constants.WalkingVelocityLimit) animator.SetBool("IsRunning", true);
        else animator.SetBool("IsRunning", false);

        if (Mathf.Abs(horizontalInput) > Constants.Threshold)
        {
			animator.SetBool ("IsWalking", true);
            if (animator.GetBool("IsRunning")) animator.speed = (Mathf.Abs(horizontalInput) < Constants.MinimumRunnungAnimationSpeed)  // setup the animation speed properly
                ? Constants.MinimumRunnungAnimationSpeed : Mathf.Abs(horizontalInput);
            else animator.speed = 1;

            transform.Translate(Vector3.right * Mathf.Abs(horizontalInput) * Time.deltaTime * speed);

            if (horizontalInput < 0)
            {
				transform.rotation = Quaternion.Euler (0, 180, 0);
			}
            if (horizontalInput > 0)
            {
				transform.rotation = Quaternion.Euler (0, 0, 0);
			}
		} else {
			animator.SetBool ("IsWalking", false);
            animator.speed = 1;
		}

        if (Input.GetButtonDown(Constants.Input_Fire1))
        {
            animator.SetTrigger("StartAttack");
		}

        if (Input.GetKeyDown(Constants.Input_Space))
        {
            animator.SetTrigger("StartAttack");
        }
        if (Input.GetKeyDown("e"))
        {
            animator.SetTrigger("StartScratchHead");
        }

	}
}
