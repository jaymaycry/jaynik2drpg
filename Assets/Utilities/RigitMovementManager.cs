using UnityEngine;
using System.Collections;

/// <summary>
/// Manages Physical movement of entity
/// </summary>
public class RigitMovementManager{

    public delegate void MovementChangedHandler(RigitMovement newMovement, RigitMovement oldMovement);
    public event MovementChangedHandler MovementChanged;

    public float MovementSpeed { get; set; }

    public RigitMovement CurrentMovement
    {
        get { return _currentMovement; }
    }
    private RigitMovement _currentMovement;

    private Rigidbody2D _collisionBox;
    private Animator _animator;

    public RigitMovementManager(Rigidbody2D collisionBox, Animator animator)
    {
        _collisionBox = collisionBox;
        _animator = animator;
        _currentMovement = RigitMovement.None;
    }

    public void UpdateMovement()
    {
        float horizontalInput = Input.GetAxis(Constants.Input_Horizontal);
        RigitMovement previousMovement = _currentMovement;

        if (Mathf.Abs(horizontalInput) > Constants.Threshold)
        {
            if (horizontalInput > 0)
            {
                _collisionBox.transform.Translate(Vector3.right * Mathf.Abs(horizontalInput) * Time.deltaTime * MovementSpeed);
                _currentMovement = RigitMovement.PositiveX;
            }
            else
            {
                _collisionBox.transform.Translate(Vector3.left * Mathf.Abs(horizontalInput) * Time.deltaTime * MovementSpeed);
                _currentMovement = RigitMovement.NegativeX;
            }
        }
        else _currentMovement = RigitMovement.None;

        if (!previousMovement.Equals(_currentMovement)) if (MovementChanged != null) MovementChanged(_currentMovement, previousMovement);
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
