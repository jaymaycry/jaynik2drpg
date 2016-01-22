using UnityEngine;
using System.Collections;

/// <summary>
/// This class provides utilities
/// </summary>
public class Utilities : MonoBehaviour {

    /// <summary>
    /// Returns true when the current current animation is playing (Probably needs fixing once additional layers are used)
    /// </summary>
    /// <param name="name">name of animation</param>
    /// <param name="animator">animator object</param>
    /// <returns></returns>
    public static bool IsAnimationPlaying(string name, Animator animator)
    {
        return animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Equals(name);
    }
}
