using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinControl : Movement {

    enum PlayerState{Moving, Still, Collecting, Pushing, Dying }

    private float currentSpeed = 10.0f;
    private float turningSpeed = 360.0f;
    private GameManagerControl theManager;
    private Vector3 newPosition;
    public LayerMask mask;

    // Use this for initialization
    void Start () {
        mask = LayerMask.GetMask("Player") |  LayerMask.GetMask("PickUp");
        
        mask = ~mask;
	}
	
	// Update is called once per frame
	void Update () {
        newPosition = transform.position;
        if (shouldMoveForward()) MoveForward();


        if (shouldTurnLeft()) TurnLeft();


        if (shouldMoveBackward()) MoveBackward();

        if (shouldTurnRight()) TurnRight();


        if (shouldStrafeLeft()) StrafeLeft();


        if (shouldStrafeRight()) StrafeRight();

        if (shouldPush()) push();

        if (isOK(newPosition))
        {
            transform.position = newPosition;
   
        }
     

        updateCameraPosition();
        //Debug.Log(playerPosition.ToString());

    }

    private bool isOK(Vector3 newPosition)
    {

        return  !Physics.CheckSphere(newPosition , 0.4f,mask);
    }

    private void updateCameraPosition()
    {
        Camera.main.transform.position = transform.position + 2.0f * Vector3.up - 3*  transform.forward;
        Vector3 focus = transform.position + 5 * transform.forward;
        Camera.main.transform.rotation = Quaternion.LookRotation((focus - Camera.main.transform.position).normalized);
    }

    private void push()
    {
        Ray r = new Ray(transform.position, transform.forward);
        RaycastHit info = new RaycastHit();

        Debug.DrawRay(transform.position, this.transform.forward, Color.blue);
        if (Physics.Raycast(r,out info, 4f))
        {
            IceBlockController iceBlock = info.collider.GetComponent<IceBlockController>();

            if (iceBlock) iceBlock.push(this.transform.position);
            Debug.Log("RayCast works");
        }
    }

    internal void IAm(GameManagerControl gameManagerControl)
    {
        theManager = gameManagerControl;
    }

    private bool shouldPush()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    private bool shouldStrafeRight()
    {
        return Input.GetKey(KeyCode.E);
    }

    private void StrafeRight()
    {
       // rigidBody.velocity = currentSpeed * transform.right;
      newPosition += currentSpeed * transform.right * Time.deltaTime;
    }

    private bool shouldStrafeLeft()
    {
        return Input.GetKey(KeyCode.Q);
    }

    private void StrafeLeft()
    {
        newPosition -= currentSpeed * transform.right * Time.deltaTime;
        //   rigidBody.velocity =-currentSpeed * transform.right;
    }

    private bool shouldTurnRight()
    {
        return Input.GetKey(KeyCode.D);
    }

    private void TurnRight()
    {
        transform.Rotate(Vector3.up, turningSpeed * Time.deltaTime);
    }

    private void MoveBackward()
    {
    }

    private bool shouldMoveBackward()
    {
        return Input.GetKey(KeyCode.S);
    }

    private void TurnLeft()
    {
        transform.Rotate(Vector3.up, -turningSpeed * Time.deltaTime);
    }

    private bool shouldTurnLeft()
    {
        return Input.GetKey(KeyCode.A);
    }

    private void MoveForward()
    {
        newPosition += currentSpeed * transform.forward * Time.deltaTime;
    }

    private bool shouldMoveForward()
    {
        return Input.GetKey(KeyCode.W);
    }



  

    public void OnCollisionEnter(Collision collision)
    {
        transform.position -= currentSpeed * transform.forward * Time.deltaTime;
        if (collision.gameObject.GetComponent<IceBlockController>())
        {
            
        }
        else
            Debug.Log("not a collision we wanted");
    }
}
