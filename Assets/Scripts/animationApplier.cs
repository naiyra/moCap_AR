using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationApplier : MonoBehaviour
{
   

    private Animator animator;
    public AnimationClip anim;
    public void runAnimation()
    {

        animator = GetComponent<Animator>();
        if (anim)
        {

            AnimatorOverrideController aoc = new AnimatorOverrideController(animator.runtimeAnimatorController);
            var anims = new List<KeyValuePair<AnimationClip, AnimationClip>>();
            foreach (var a in aoc.animationClips)
                anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(a, anim));
            aoc.ApplyOverrides(anims);
            animator.runtimeAnimatorController = aoc;
            //animator.Play(anim.name);

        }

    }
}
