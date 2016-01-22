using UnityEngine;
using System.Collections;

/// <summary>
/// Behaviour of player character
/// </summary>
public class SimpleCharacterWalk : MonoBehaviour {
	public Animator animator;
    public Motion Character_attack_1;
	public float speed = 3f;
    private float lastHorizontalInput = 1f; // used for attack direction movement

	// Use this for initialization
	void Start () {
	
	}

    /// <summary>
    /// Put all status parameters here when they need to be updated
    /// </summary>
    private void updateStateBooleans()
    {
        animator.SetBool("IsAttacking", Utilities.IsAnimationPlaying("Character_attack1", animator));
    }
	
	// Update is called once per frame
	void Update () {
        float horizontalInput = Input.GetAxis(Constants.Input_Horizontal);
        updateStateBooleans();

        if (animator.GetBool("IsAttacking")) animator.ResetTrigger("StartAttack");

        #region Movement
        if (Mathf.Abs(horizontalInput) > Constants.WalkingVelocityLimit) animator.SetBool("IsRunning", true);
        else animator.SetBool("IsRunning", false);

        if (Mathf.Abs(horizontalInput) > Constants.Threshold && !animator.GetBool("IsAttacking"))
        {
			animator.SetBool ("IsWalking", true);
            if (animator.GetBool("IsRunning"))
            {
                animator.speed = (Mathf.Abs(horizontalInput) < Constants.MinimumRunnungAnimationSpeed)  // setup the animation speed properly
                ? Constants.MinimumRunnungAnimationSpeed : Mathf.Abs(horizontalInput);
            }
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
            lastHorizontalInput = horizontalInput;
		} else {
			animator.SetBool ("IsWalking", false);
            animator.speed = 1;
        }
        #endregion

        #region Combat
        if (Input.GetButtonDown(Constants.Input_Fire1))
        {
            animator.SetTrigger("StartAttack");
            animator.SetBool("IsAttacking", true);        
		}
        if (animator.GetBool("IsAttacking"))
        {
            transform.Translate(Vector3.right * Constants.AttackMovement * Time.deltaTime * speed);

            if (lastHorizontalInput < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if (lastHorizontalInput > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        if (Input.GetKeyDown(Constants.Input_Space))
        {
            animator.SetTrigger("StartDodge");
        }
        #endregion

        if (Input.GetKeyDown("e"))
        {
            animator.SetTrigger("StartScratchHead");
        }
	}
}
