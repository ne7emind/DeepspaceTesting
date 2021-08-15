using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EventsController : MonoBehaviour
{
    public event EventHandler OnSpacePressed; 
    public event OnMoving OnWalking;

    public delegate void OnMoving(Vector2 XZ);
    
    private void FixedUpdate()
    {   
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        OnWalking?.Invoke(new Vector2(horizontal, vertical));

        if (Input.GetKey(KeyCode.Space))
        { 
            OnSpacePressed?.Invoke(this, EventArgs.Empty);         
        } 
        
    }

}
