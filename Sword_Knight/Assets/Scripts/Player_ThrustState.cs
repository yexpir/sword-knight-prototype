using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ThrustState : StateMachineBehaviour
{

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<Sword>().Thrusting(true);
        animator.gameObject.GetComponentInParent<Movement>().anim.SetBool("Thrusting", true);
        //animator.gameObject.GetComponentInParent<Movement>().thrustDuration = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        animator.gameObject.GetComponentInParent<Movement>().thrustDir = animator.gameObject.GetComponentInParent<Movement>().GetFixedDir(0);
        animator.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
        animator.gameObject.GetComponentInParent<Movement>().AttackDir(animator.gameObject.GetComponentInParent<Movement>().attackCharge);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponentInParent<Movement>().anim.SetBool("Thrusting", false);
        //animator.gameObject.GetComponentInParent<Movement>().ResetThrustDash();
        animator.gameObject.GetComponentInParent<Movement>().attack = false;
        animator.gameObject.GetComponent<Sword>().Thrusting(false);

        //animator.gameObject.GetComponentInParent<Movement>().ResetAttack();
    }




    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}


    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

}
