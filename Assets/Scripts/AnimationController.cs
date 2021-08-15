using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private Logger logger;
    // Start is called before the first frame update
    private void Start()
    {
        logger = new Logger(new GameLogHandler());
        animator = GetComponent<Animator>();
        EventsController eventsController = GetComponent<EventsController>();
        eventsController.OnSpacePressed += EventsController_OnSpacePressed;
        eventsController.OnWalking += EventsController_OnWalking;
    } 

    private void EventsController_OnWalking(Vector2 XZ)
    {
        animator.SetFloat("Speed", Mathf.Abs(XZ.x) + Mathf.Abs(XZ.y));
      //  Debug.Log(Mathf.Abs(XZ.x));
    }

    private void EventsController_OnSpacePressed(object sender, System.EventArgs e)
    {
        if (GetComponent<PlayerPhysics>().IsGrounded())
        {
            animator.SetTrigger("Jump");
        }
    }

    private void Update()
    {
        if (GetComponent<PlayerPhysics>().IsGrounded())
        {
            animator.SetBool("grounded", true);
            logger.Log("Player is grounded");
            animator.ResetTrigger("Jump");
        }
        else
        {
            logger.Log("Player isn't grounded");
            animator.SetBool("grounded", false);
        }
    }
}
