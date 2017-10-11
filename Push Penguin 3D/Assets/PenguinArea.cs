using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinArea : MonoBehaviour,IDestoryable,IHitable
{
    private float heightOfWorldFloor = 0.0f;
    public Transform IceBlockPreFab,WallPrefab;
    private int widthOfWorld = 20;
    private int depthOfWorld = 20;
    //public GameObject[] ranIceBlocks;
    //public Transform[] spawn;




    // Use this for initialization
    void Start()
    {
        //Creates rocks for area to be placed at the edges of the arena

        //rock.name = "Rock Edge ";

        //GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);

        //floor.transform.position += new Vector3(9.5f, -0.2f, 10);

        //floor.name = "Floor";

        //floor.transform.localScale += new Vector3(17.2f, 0, 17);
        //Sets up the floor for the game arena.


        //border(); //Creates border for rocks to be placed at the arena's edges.

        for(int x = 0; x < widthOfWorld; x++) {
            for(int z = 0; z < depthOfWorld; z++)
            {
                CreateIceBlockAt(new Vector3(x,-1,z));
                if (isOntheEdge(x, z)) createWallAt( new Vector3(x, 0 , z));

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

    private void createWallAt(Vector3 v)
    {
        Transform newBie = Instantiate(WallPrefab, SnapTo(v), Quaternion.identity);
    }

    private bool isOntheEdge(int x, int z)
    {
        return (x == 0) || (z == 0) || (x == (widthOfWorld - 1)) || (z == (depthOfWorld - 1));
    }

    private Vector3 SnapTo(Vector3 v)
    {
        return new Vector3(Mathf.RoundToInt(v.x), heightOfWorldFloor, Mathf.RoundToInt(v.z));
    }

    private Vector3 SnapTo(Vector3 v, float height)
    {
        Vector3 snap1 = SnapTo(v);
        return new Vector3(snap1.x, height, snap1.z);
    }




    void CreateIceBlockAt(Vector3 v) //Creates the Ice Blocks based on the prefabs
    {
        Transform newBie = Instantiate(IceBlockPreFab, SnapTo(v, v.y), Quaternion.identity);

    }




    void Update()
    {

    }



    public bool DoDestroy()
    {
        throw new NotImplementedException();
    }

    public Hit OnHit()
    {
        throw new NotImplementedException();
    }

}
