using UnityEngine;
using System.Collections;

/// <summary>
/// This class provides utilities
/// </summary>
public class Utilities : MonoBehaviour {

    /// <summary>
    /// Returns true when the current current animation is playing.
    /// </summary>
    /// <param name="name">name of animation</param>
    /// <param name="animator">animator object</param>
    /// <returns></returns>
    public static bool IsAnimationPlaying(string name, Animator animator)
    {
        for (int i = 0; i < animator.layerCount; i++)
        {
            AnimatorClipInfo[] clipInfoArray = animator.GetCurrentAnimatorClipInfo(i);
            foreach (AnimatorClipInfo info in clipInfoArray) if (info.clip.name.Equals(name)) return true;
        }
        return false;
    }
}
