using UnityEngine;

/// <summary>
/// Holds Game Constants
/// </summary>
public class Constants : MonoBehaviour
{
    #region InputDirections
    public static readonly string Input_Horizontal = "Horizontal";
    public static readonly string Input_Vertical = "Vertical";
    #endregion

    #region InputKeys
    public static readonly string Input_Fire1 = "Fire1";
    public static readonly string Input_Space = "space";
    #endregion

    #region Math
    public static readonly float Threshold = 1e-1f;
    public static readonly float WalkingVelocityLimit = 0.35f;
    public static readonly float MinimumRunnungAnimationSpeed = 0.7f;
    #endregion
}
