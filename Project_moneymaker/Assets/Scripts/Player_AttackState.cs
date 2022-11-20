using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AttackState : StateMachineBehaviour
{

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<Sword>().Attacking(true);
        animator.gameObject.GetComponentInParent<Movement>().AttackDir(animator.gameObject.GetComponentInParent<Movement>().attackCharge);

        animator.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
        //animator.gameObject.GetComponentInParent<Movement>().attack = true;
        //animator.gameObject.GetComponentInParent<Movement>().Attack(animator.gameObject.GetComponentInParent<Movement>().attackCharge);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<Sword>().Attacking(false);
        animator.gameObject.GetComponentInParent<Movement>().attack = false;
        animator.gameObject.GetComponentInParent<Movement>().ResetAttack();
    }
}
