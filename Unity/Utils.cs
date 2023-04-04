using System;
using UnityEngine;

public class Utils
{
    // For 2D games where Y height determines depth
    public static void PosToSortingOrder(SpriteRenderer sr, Vector2 position, bool centerPivot = true)
    {
        sr.sortingOrder = (int)(-10 * position.y) + (centerPivot ? (int)(10 * sr.sprite.pivot.y / 16) : 0);
    }

    public static bool IsBetween(float num, float min, float max)
    {
        return (num >= min) && (num <= max);
    }

    public static int PosNeg()
    {
        return 1 - 2 * UnityEngine.Random.Range(0, 2);
    }

    // For use with EzTimer.
    public void AddTimedFunction(GameObject source, Action function, float time, bool loop = false, string id = "fn")
    {
        EzTimer timer;

        if(source.TryGetComponent(out EzTimer t))
        {
            timer = t;
        } 
        else
        {
            timer = source.AddComponent<EzTimer>();
        }

        timer.SetTimer(id, function, time, loop);
        timer.destroyOnFinish = loop;
    }

    // Grabs the length of a specified animation in an animator.
    public static float GetClipLength(Animator anim, string name)
    {
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if(clip.name == name) return clip.length;
        }
        return 0;
    }
}
