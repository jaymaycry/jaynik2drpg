using UnityEngine;
using System.Collections;

/// <summary>
/// Behaviour of player character
/// </summary>
public class CharacterBehaviour : MonoBehaviour {
	public Animator animator;
	public float speed = 3f;
    public Rigidbody2D rigidbody;
    public BoxCollider2D collisionBox;

    private float lastHorizontalInput = 1f; // used for attack direction movement
    private RigitMovementManager rigitMovementManager;

	// Use this for initialization
	void Start () {
        rigitMovementManager = new RigitMovementManager(rigidbody, collisionBox, animator);
        rigitMovementManager.MovementSpeed = speed;
        
	}

    /// <summary>
    /// Put all status parameters here when they need to be updated
    /// </summary>
    private void updateStateBooleans()
    {
		animator.SetBool ("IsAttacking", Utilities.IsAnimationPlaying ("Character_attack1", animator)
		|| Utilities.IsAnimationPlaying ("Character_attack2", animator)
		|| Utilities.IsAnimationPlaying ("Character_attack3", animator)
		|| Utilities.IsAnimationPlaying ("Character_attack4", animator)
		|| Utilities.IsAnimationPlaying ("Character_hardattack01", animator)
		|| Utilities.IsAnimationPlaying ("Character_hardattack02", animator));
    }
	
	// Update is called once per frame
	void Update () {
        float horizontalInput = Input.GetAxis(Constants.Input_Horizontal);
        updateStateBooleans();

        if (animator.GetBool("IsAttacking")) animator.ResetTrigger("StartHardAttack");
        if (Utilities.IsStateActive("Character_hardattack02", animator)) animator.ResetTrigger("ExecuteHardAttack");    // Prohibit that next attack gets fired immediatly

        #region Movement
        if (Mathf.Abs(horizontalInput) > Constants.WalkingVelocityLimit) animator.SetBool("IsRunning", true);
        else animator.SetBool("IsRunning", false);

        rigitMovementManager.HandleJumps(); // handle jumping of character
        animator.SetBool("IsOnGround", rigitMovementManager.IsOnGround());
        animator.SetBool("IsFalling", rigitMovementManager.IsFalling());

        if (Mathf.Abs(horizontalInput) > Constants.Threshold && !animator.GetBool("IsAttacking"))
        {
			animator.SetBool ("IsWalking", true);
            if (animator.GetBool("IsRunning"))
            {
                animator.speed = (Mathf.Abs(horizontalInput) < Constants.MinimumRunnungAnimationSpeed)  // setup the animation speed properly
                ? Constants.MinimumRunnungAnimationSpeed : Mathf.Abs(horizontalInput);
            }
            else animator.speed = 1;

            rigitMovementManager.UpdateMovement();  // do actual character movement (physics based)

            if (horizontalInput < 0)	transform.rotation = Quaternion.Euler (0, 180, 0);
            else transform.rotation = Quaternion.Euler (0, 0, 0);

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

            if (!Utilities.IsStateActive("Character_attack1", animator) // only add force on attack 1
                && rigidbody.velocity.magnitude < Constants.Threshold)   // wait until body rests before add new force
            {
                if (lastHorizontalInput < 0) rigidbody.AddForce(Vector2.left * Constants.AttackForwardForce);
                else rigidbody.AddForce(Vector2.right * Constants.AttackForwardForce);
            }
		}
		if (Input.GetButtonDown(Constants.Input_Fire2))
		{
			animator.SetTrigger("StartHardAttack");
			animator.SetBool("IsAttacking", true);
		}
		if (Input.GetButtonUp(Constants.Input_Fire2))
		{
			animator.SetTrigger("ExecuteHardAttack");
            if (rigidbody.velocity.magnitude < Constants.Threshold)
            {
                if (lastHorizontalInput < 0) rigidbody.AddForce(Vector2.left * Constants.AttackForwardForce);
                else rigidbody.AddForce(Vector2.right * Constants.AttackForwardForce);
            }
		}

        // Dodge will be implemented with new combat system
        /*
        if (Input.GetKeyDown(Constants.Input_Space))
        {
            animator.SetTrigger("StartDodge");
        }*/
        #endregion

        if (Input.GetKeyDown("e"))
        {
            animator.SetTrigger("StartScratchHead");
        }
	}
}
