using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;    
    [SerializeField] private float extraGravity;
    [SerializeField] private float turnSmoothTime;
    [SerializeField] private Transform thirdCamera;

    private float turnSmoothVelocity;
    private Logger logger;
    private Rigidbody rBody;
    private BoxCollider bCollider;

    // Start is called before the first frame update
    private void Start()
    {
        logger = new Logger(new GameLogHandler());
        logger.Log("level has been loaded");

        bCollider = GetComponent<BoxCollider>();
        rBody = GetComponent<Rigidbody>();
        
        EventsController eventsController = GetComponent<EventsController>();
        eventsController.OnSpacePressed += EventsController_OnSpacePressed;
        eventsController.OnWalking += EventsController_OnWalking;       
       
    }
   
    private void EventsController_OnWalking(Vector2 XZ)
    {
        Vector3 direction = new Vector3(XZ.x, 0, XZ.y).normalized;

        if (direction.magnitude >= .1f)
        {
            logger.Log("Player is walking");
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + thirdCamera.eulerAngles.y;
            float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);

            Vector3 moveInDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
           

            rBody.MovePosition(transform.position + moveInDirection.normalized * movementSpeed * Time.deltaTime);
        }
    
    }

    private void EventsController_OnSpacePressed(object sender, System.EventArgs e)
    {
        if (IsGrounded())
        {
            rBody.AddForce(Vector3.up * jumpForce * Time.fixedDeltaTime, ForceMode.Impulse);
           
                       
            logger.Log("Player has jumped");
        }

    }
     
    public bool IsGrounded()
    {
        float extraHeight = .5f;
        return Physics.Raycast(bCollider.bounds.center, Vector3.down, bCollider.bounds.extents.y + extraHeight);
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        rBody.velocity = new Vector3(rBody.velocity.x, rBody.velocity.y - extraGravity, rBody.velocity.z);      
    }

}
