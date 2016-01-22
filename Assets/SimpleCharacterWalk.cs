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

        if (Mathf.Abs(horizontalInput) > Constants.Threshold)
        {
			animator.SetBool ("Walking", true);
            animator.speed = Mathf.Abs(horizontalInput);

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
			animator.SetBool ("Walking", false);
            animator.speed = 1;
		}

		if (Input.GetButton (Constants.Input_Fire1)) {
			animator.SetTrigger ("Attacking");
		}

        if (Input.GetKeyDown(Constants.Input_Space))
        {
            animator.SetTrigger("Dodge");
            transform.Translate(Vector3.left * 1f);
        }
        if (Input.GetKeyDown("e"))
        {
            animator.SetTrigger("ScratchHead");
        }

	}
}
