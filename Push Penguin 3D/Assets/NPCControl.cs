using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCControl : MonoBehaviour, IDestoryable {

    /// <summary>
    /// waiting = can move/waiting for destination;
    /// moving = has destination/moving towards it;
    /// still = can't move;
    /// following = moving towards the player's last known location;
    /// attacking = moving towards the player;
    /// </summary>
    private enum Status { waiting, moving, still, following, attacking };
    private Status status;

    private enum Direction { left = -1, right = 1 };
    private bool canFollow;

    private Vector3 destination, destinationWhileFollowing;
    private static Transform NPCParent;
    private Transform raycastTarget;

    private int moveSpeed = 3;
    private const float STOPPING_DISTANCE = 0.06f, WAIT_TIME = 0.25f;

	private static GameObject player;
	//private RaycastHit h;
    
    void Start () {
        //If there is no NPC parent, create and assign NPC parent
        if (NPCParent == null)
            NPCParent = new GameObject("NPC Parent").transform;

        //transform.SetParent(NPCParent);
        status = Status.waiting;
        raycastTarget = transform.GetChild(0);
        destinationWhileFollowing = Vector3.down;
        canFollow = true;
        //player = GameObject.Find("").GetComponent<GameManagerControl>().player[0];
		if (player == null)
			player = GameObject.FindObjectOfType<PenguinControl> ().gameObject;
    }
	
	void Update ()
    {
        //transform.forward is not working
        //print(Vector3.Dot(transform.forward, player.transform.position) + "\t" + transform.forward);
        //Debug.DrawRay(transform.position, transform.forward * 2, Color.red);

        switch (status)
        {
            #region Waiting
            case Status.waiting:
                RaycastHit hit;

                //Raycast, if the raycast hits something (ice block or wall)
				Physics.Raycast(transform.position, transform.forward, out hit);
				if (hit.rigidbody != null)
                {
					//if (!hit.rigidbody.gameObject.name.Equals(gameObject.name))
					//{
                    	//set destionation to 1 unit 'in front' of raycast his position
						destination = hit.rigidbody.position - (hit.rigidbody.position - transform.position).normalized;
                    	status = Status.moving;
					//}
					//print(hit.rigidbody.gameObject.name);
                }
				else if (hit.collider != null) {
					if (hit.collider.gameObject == player)	
						print("Hit player");
				}
                break;
            #endregion
            #region Moving
            case Status.moving:
                //If NPC is more than STOPPING_DISTANCE away from destination
                if (!HasArrived(destination))
                    Move(destination);
                else
                {
                    status = Status.still;
                    //Always right
                    Direction d = (Random.Range(0, 2) == 0) ? Direction.right : Direction.left;
                    StartCoroutine(WaitForNewDestination(d));
                }
                break;
            #endregion
            #region Following
            case Status.following:
                //move 1 block at a time towards the player
                Vector3 toPlayer = player.transform.position - transform.position;
                //check left
                float dot = Vector3.Dot(transform.forward, toPlayer);
			//print(transform.forward);
                if (dot > 0)
                {
                    if (destinationWhileFollowing == Vector3.down)
                    {
                        if (canFollow)
                            destinationWhileFollowing = transform.position + transform.forward;
                        else
                        {
                            status = Status.waiting;
                            break;
                        }
                    }

                    if (!HasArrived(destinationWhileFollowing))
                        Move(destinationWhileFollowing);
                    else
                        destinationWhileFollowing = Vector3.down;
                    //if (dot < 0)
                }
			else {
				transform.Rotate(0, -90, 0);
				return;
			}
                break;
            #endregion
        }

        //Raycast to player
		Debug.DrawRay(raycastTarget.transform.position, player.transform.position - transform.position, Color.green);
        
        RaycastHit h;
		Physics.Raycast(raycastTarget.transform.position, player.transform.position - transform.position, out h);
        if (h.collider != null)
        {
			if (h.collider.gameObject.Equals(player) && (player.transform.position - transform.position).magnitude < 5)
            {

                status = Status.following;
                //destination = player.transform.position;
                if (!canFollow)
                    canFollow = true;
            }
            else
                canFollow = false;
            //print(h.rigidbody.gameObject + "\t" + (player.transform.position - transform.position).magnitude);

            //print((player.transform.position - transform.position).magnitude);
        }
        else
            canFollow = false;
        //Vector3 dot. if positive is facing

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="destination"></param>
    private void Move(Vector3 dest)
    {
        //Move towards destination
        Vector3 velocity = moveSpeed * (dest - transform.position).normalized;
        transform.position += velocity * Time.deltaTime;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dest"></param>
    /// <returns></returns>
    private bool HasArrived(Vector3 dest)
    {
        if ((dest - transform.position).magnitude > STOPPING_DISTANCE)
            return false;
        return true;
    }

    //Kill enemy
    public bool DoDestroy()
    {
        Destroy(gameObject);
        return true;
    }
    
    /// <summary>
    /// Wait for WAIT_TIME seconds before rotating 90 degrees and then allowing the NPC to move again.
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitForNewDestination(Direction d)
    {
        int degrees = (d == Direction.right) ? -90 : 90;
        yield return new WaitForSeconds(WAIT_TIME);
        transform.Rotate(0, degrees, 0);
        status = Status.waiting;
    }
}
