using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManagerControl : MonoBehaviour
{

    public enum ItemType { Apple, Banana, Cherry}

    private int currentLevel = 1;

    private List<EggControl> eggs;
    private List<PickUpItemControl> items;
    private List<NPCControl> enemies;
    private List<PenguinControl> player;

    public Transform itemClone;
    public Transform eggClone;
    public Transform playerClone;

    WorldControl theWorld;
    private int numberOfEggs= 4;
    private int numberOfStartingItems = 3;

    public void EnemySpawn(NPCControl enemy)
    {
        enemies.Add(enemy);
    }

    public void PlayerSpawn()

    {
        Vector3 positionToSpawn;// = theWorld.randomEmptyPosition();
        positionToSpawn = new Vector3(1, 1, 1);
        PlayerSpawnAt(positionToSpawn);
    }

    private void SpawnRandomItem()
    {
        ItemType newItemType = (ItemType)UnityEngine.Random.Range(0, 3);

        Vector3 positionToSpawn= theWorld.randomEmptyPosition();

 
        ItemSpawnAt(newItemType, positionToSpawn);

    }

    private void PlayerSpawnAt(Vector3 positionToSpawn)
    {
        Transform newPlayer = Instantiate(playerClone, positionToSpawn, Quaternion.identity);
        PenguinControl newPlayerControl = newPlayer.GetComponent<PenguinControl>();
        newPlayerControl.IAm(this);
        player.Add(newPlayerControl);
    }

    private void ItemSpawnAt(ItemType newItemType, Vector3 positionToSpawn)
    {
            Transform newItem = Instantiate(itemClone, positionToSpawn, Quaternion.identity);
            PickUpItemControl newItemControl = newItem.GetComponent<PickUpItemControl>();
            newItemControl.YouAre(newItemType, 100 * (1 + (int)newItemType), 30.0f);
            items.Add(newItemControl);
    }


    private void EggSpawnAt( Vector3 positionToSpawn)
    {
        Transform newEgg = Instantiate(eggClone, positionToSpawn, Quaternion.identity);
        EggControl newEggControl = newEgg.GetComponent<EggControl>();
        newEggControl.IAm(this);
        eggs.Add(newEggControl);
    }

    public void ItemDestroyed(PickUpItemControl item)
    {
        items.Remove(item);
    }

    public void EnemyDestroyed(NPCControl enemy)
    {
        enemies.Remove(enemy);
        if(enemies.Count == 0)
        {
            nextLevel();
        }
    }

    private void nextLevel()
    {
        throw new NotImplementedException();
    }

    public void EggDestroyed(EggControl egg)
    {
        eggs.Remove(egg);
    }

    public void PlayerDestroyed(PenguinControl penguin)
    {
        player.Remove(penguin);
    }

    //public void LevelControl()
    //{
    //    while (currentLevel < 4)
    //    {
    //        switch (currentLevel)
    //        {
    //            case 1:
    //                {
    //                    //theWorld.LevelStart(currentLevel);

    //                    PlayerSpawn();

    //                    for(int i = 0; i < 5; i++)
    //                    {
    //                        SpawnRandomItem();
    //                        EggSpawn();
    //                    }
    //                    //currentLevel++;
    //                    break;
    //                }
    //            case 2:
    //                {
    //                    //theWorld.LevelStart(currentLevel);

    //                    PlayerSpawn();

    //                    for(int i = 0; i < 8; i++)
    //                    {
    //                        EggSpawn();

    //                        if(i < 5)
    //                        {
    //                            SpawnRandomItem();
    //                        }
    //                    }
    //                    //currentLevel++;
    //                    break;
    //                }
    //            case 3:
    //                {
    //                    //theWorld.LevelStart(currentLevel);

    //                    PlayerSpawn();

    //                    for (int i = 0; i < 10; i++)
    //                    {
    //                        EggSpawn();

    //                        if (i < 7)
    //                        {
    //                            SpawnRandomItem();
    //                        }
    //                    }
    //                    //currentLevel++;
    //                    break;
    //                }
    //            case 4:
    //                {
    //                    break;
    //                }
    //        }
    //    }
    //}

    // Use this for initialization
    void Start()
    {
        theWorld = FindObjectOfType<WorldControl>();

        if (theWorld) print("found the world");
        else print("No world");
        eggs = new List<EggControl>();
        items = new List<PickUpItemControl>();
        enemies = new List<NPCControl>();
        player = new List<PenguinControl>();
        theWorld.spawnRandomRocks(10);
        theWorld.spawnRandomIceBlocks(20);


        for (int i = 0; i < numberOfEggs; i++)
        {
            Vector3 eggStartingPosition = theWorld.randomEmptyPosition();
            EggSpawnAt(eggStartingPosition);
        }

           
            for (int i = 0; i < numberOfStartingItems; i++)
            SpawnRandomItem();



        Vector3 playerStartingPostion = theWorld.randomEmptyPosition();
        Instantiate(playerClone, playerStartingPostion+0.2f*Vector3.up, Quaternion.identity);


        //    LevelControl();
    }

    // Update is called once per frame
    void Update(){

        if (Input.GetKeyDown(KeyCode.C)) SpawnRandomItem();
    }
}
