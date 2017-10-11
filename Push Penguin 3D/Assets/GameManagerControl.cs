using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerControl : MonoBehaviour,IScorable,ISpawnable{
    enum ItemType { Apple,Banana,Cherry}

    private int currentEggs;
    private List<EggControl> eggs;
    private List<PickUpItemControl> items;
    private int currentEnemies;
    private int currentItems;
    private int players;

    public int EggSpawn()
    {
        //Spawn Egg

        currentEggs++;

        return currentEggs;
    }

    public int EnemySpawn()
    {
        //Spawn Enemy

        currentEnemies++;

        return currentEnemies;
    }

    public int ItemSpawn()
    {
        //spawn Item

        currentItems++;

        return currentItems;
    }

    public int PlayerSpawn()
    {
        //spawn Player

        players++;

        return players;
    }


  

    public int AddScore(char item, int currentScore)
    {
       /* List<ItemType> myList = new List<ItemType>();

        foreach (ItemType i in myList)
            if (i== currentlyActiveType) 

        switch(item)
        {
            case 'A':
                {
                    currentScore += apple.getScore();
                    break;
                }
            case 'B':
                {
                    currentScore += banana.getScore();
                    break;
                }
            case 'C':
                {
                    currentScore += cherry.getScore();
                    break;
                }
            default:
                {
                    break;
                }
        }
        */
        return currentScore;
    }

    public void SetScore(char item)
    {
        int minScore = 50;
        int maxScore = 250;

        switch (item)
        {
            case 'A':
                {
                    //apple.setScore(minScore);
                    break;
                }
            case 'B':
                {
                    //banana.setScore(minScore*1.25f);
                    break;
                }
            case 'C':
                {
                    //cherry.setScore(maxScore);
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    public void ItemDestroyed(PickUpItemControl  item)
    {

        items.Remove(item);

    }

    // Use this for initialization
    void Start () {

        eggs = new List<EggControl>();
        items = new List<PickUpItemControl>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    
}
