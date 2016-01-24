using UnityEngine;
using System.Collections;

/// <summary>
/// Manages Physical movement of entity
/// </summary>
public class RigitMovementManager{

    /// <summary>
    /// Handles changes of rigid body movement.
    /// </summary>
    /// <param name="newMovement"></param>
    /// <param name="oldMovement"></param>
    public delegate void MovementChangedHandler(RigitMovement newMovement, RigitMovement oldMovement);

    /// <summary>
    /// Fires when movement (caused by user input) changes. 
    /// </summary>
    public event MovementChangedHandler MovementChanged;

    /// <summary>
    /// Gets or sets the movement speed.
    /// </summary>
    public float MovementSpeed { get; set; }

    /// <summary>
    /// Holds the current direction of movement (not physical).
    /// </summary>
    public RigitMovement CurrentMovement
    {
        get { return _currentMovement; }
    }
    private RigitMovement _currentMovement;

    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collisionBox;
    private Animator _animator;

    public RigitMovementManager(Rigidbody2D rigidbody, BoxCollider2D collisionBox, Animator animator)
    {
        _rigidbody = rigidbody;
        _collisionBox = collisionBox;
        _animator = animator;
        _currentMovement = RigitMovement.None;
    }

    /// <summary>
    /// Updates movement acording to input.
    /// </summary>
    public void UpdateMovement()
    {
        float horizontalInput = Input.GetAxis(Constants.Input_Horizontal);
        RigitMovement previousMovement = _currentMovement;

        if (Mathf.Abs(horizontalInput) > Constants.Threshold)
        {
            if (horizontalInput > 0)
            {
                _rigidbody.transform.Translate(Vector3.right * Mathf.Abs(horizontalInput) * Time.deltaTime * MovementSpeed);
                _currentMovement = RigitMovement.PositiveX;
            }
            else
            {
                _rigidbody.transform.Translate(Vector3.left * Mathf.Abs(horizontalInput) * Time.deltaTime * MovementSpeed);
                _currentMovement = RigitMovement.NegativeX;
            }
        }
        else _currentMovement = RigitMovement.None;

        if (!previousMovement.Equals(_currentMovement)) if (MovementChanged != null) MovementChanged(_currentMovement, previousMovement);
    }

    /// <summary>
    /// Handles jumps with physics engine.
    /// </summary>
    public void HandleJumps()
    {
        if (Input.GetButtonDown(Constants.Input_Jump) && IsOnGround())
        {
            _rigidbody.AddForce(Vector2.up*Constants.JumpForce);
        }
    }

    /// <summary>
    /// Returns true if collision box touches collider at its bottom.
    /// </summary>
    /// <returns></returns>
    public bool IsOnGround()
    {
       /* return Physics2D.Raycast(_rigidbody.position, Vector2.down, _collisionBox.size.y)
            || Physics2D.Raycast(_rigidbody.position + Vector2.right * _collisionBox.size.x/2, Vector2.down, _collisionBox.size.y);*/
        //return Physics2D.Raycast(_rigidbody.position, Vector2.down, _collisionBox.size.y);
        return Physics2D.Raycast(_rigidbody.position, Vector2.down, _collisionBox.size.y)
            || Physics2D.Raycast(_rigidbody.position + Vector2.right * _collisionBox.size.x / 2, Vector2.down, _collisionBox.size.y)
            || Physics2D.Raycast(_rigidbody.position - Vector2.right * _collisionBox.size.x / 2, Vector2.down, _collisionBox.size.y);
    }

    /// <summary>
    /// Returns if body is falling.
    /// </summary>
    /// <returns></returns>
    public bool IsFalling()
    {
        return !IsOnGround() && _rigidbody.velocity.y < Constants.Threshold;
    }
}

public enum RigitMovement
{
    PositiveX,
    NegativeX,
    PositiveY,
    NegativeY,
    PositiveZ,
    NegativeZ,
    None
}
