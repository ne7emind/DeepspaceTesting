using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingIdleForLongf : StateMachineBehaviour
{
    [SerializeField] private float timeForCrouchTransition;

    private float secondsBeingIdle;
    private Logger logger;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        logger = new Logger(new GameLogHandler());
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {     
        secondsBeingIdle += Time.deltaTime;

        logger.Log("Player is being idle");

        if (secondsBeingIdle >= timeForCrouchTransition)
        {
            animator.SetTrigger("Crouch");
            logger.Log("Player is crouching");
        }
                         
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Crouch");
        secondsBeingIdle = 0;
    }

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
