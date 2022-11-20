using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DashState : StateMachineBehaviour
{
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //}
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponentInParent<Movement>().dash = true;
        animator.gameObject.GetComponentInParent<Movement>().dashDuration = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        animator.gameObject.GetComponentInParent<Movement>().dashDir = animator.gameObject.GetComponentInParent<Movement>().GetFixedDir(1);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponentInParent<Movement>().ResetDash();
    }

    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //}
}
