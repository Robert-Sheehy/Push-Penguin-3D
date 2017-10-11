using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldControl : MonoBehaviour {
    private float heightOfWorldFloor = 0.0f;
    public Transform IceBlockPreFab, WallPrefab;
    private int widthOfWorld = 20;
    private int depthOfWorld = 20;

    // Use this for initialization
    void Start () {
        //for (int i = 0; i < 5; i++)
            //CreateIceBlockAt(new Vector3(i, 0, 2 * i));

        //border();

        for (int x = 0; x < widthOfWorld; x++)
        {
            for (int z = 0; z < depthOfWorld; z++)
            {
                CreateIceBlockAt(new Vector3(x, -1, z));
                if (isOntheEdge(x, z)) createWallAt(new Vector3(x, 0, z));

                //for (int i = 0; i < widthOfWorld; i++) //Forms the rock borders in the edges of the arena based on the square design of the arena
                //{
                //    CreateIceBlockAt(new Vector3(i, 0, 0));
                //    CreateIceBlockAt(new Vector3(i, 0, depthOfWorld - 1));
                //}

                //for (int i = 1; i < depthOfWorld - 1; i++)
                //{
                //    CreateIceBlockAt(new Vector3(0, 0, i));
                //    CreateIceBlockAt(new Vector3(widthOfWorld - 1, 0, i));
                //}


                //iceblock.transform.position += new Vector3(0, 0.0011f, 0);

                //iceblock.name = "Ice Block";
                //// create floor
                //CreateIceBlockAt(new Vector3(x, heightOfWorldFloor, z));
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void createWallAt(Vector3 v)
    {
        Transform newBie = Instantiate(WallPrefab, SnapTo(v), Quaternion.identity);
    }

    private bool isOntheEdge(int x, int z)
    {
        return (x == 0) || (z == 0) || (x == (widthOfWorld - 1)) || (z == (depthOfWorld - 1));
    }

    internal Vector3 getDestinationForIceblock(IceBlockController iceBlockController, Vector3 pusherPosition)
    {
        Vector3 positionOfIceBlock = iceBlockController.transform.position;
        Vector3 direction = positionOfIceBlock - SnapTo(pusherPosition);
        if (!((direction.magnitude < 0.1f) || (direction.magnitude > 1.1f)))
            return SnapTo( positionOfIceBlock + 5.0f * direction);

        return positionOfIceBlock;
    }

    private Vector3 SnapTo(Vector3 v)
    {
        return new Vector3(Mathf.RoundToInt(v.x), heightOfWorldFloor , Mathf.RoundToInt(v.z));
    }

    private Vector3 SnapTo(Vector3 v, float height)
    {
        Vector3 snap1 = SnapTo(v);
        return new Vector3(snap1.x, height, snap1.z);
    }


    void CreateIceBlockAt(Vector3 v)
    {
        Transform newBie = Instantiate(IceBlockPreFab,SnapTo(v,v.y),Quaternion.identity);

        IceBlockController HiNewbie = newBie.gameObject.GetComponent<IceBlockController>();

        HiNewbie.ThisIsMe(this);

    }
    /// <summary>
    /// Returns true if position supplied is empty in the world
    /// </summary>
    /// <param name="Position">Position in world</param>
    /// <returns></returns>
    private bool IsEmptyAt(Vector3 Position)
    {
        //todo: Try RayCast from above point straight down
        throw new System.NotImplementedException();
    }


    private void border()
    {
        for (int i = 0; i < widthOfWorld; i++)
        {
            CreateIceBlockAt(new Vector3(i, 0, 0));
            CreateIceBlockAt(new Vector3(i, 0, depthOfWorld-1));
        }

        for (int i = 1; i < depthOfWorld-1; i++)
        {
            CreateIceBlockAt(new Vector3(0, 0, i));
            CreateIceBlockAt(new Vector3(widthOfWorld-1, 0, i));
        }
    }
}
