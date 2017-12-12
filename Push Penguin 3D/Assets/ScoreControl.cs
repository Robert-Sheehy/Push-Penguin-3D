
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreControl : MonoBehaviour, iScore {

    //PopUpScoreControl popUpScore;
    private int CurrentScore = 0;
    TextMesh scoreMesh;
    int Count = 10;
   // private int currentScore;


   public void AddScore(int scoreIncrement)
    {

        //GameObject scoreText = new GameObject();
        //scoreText.GetComponent<TextMesh>();
        /*TextMesh scoreMesh = new TextMesh ();
        scoreText.AddComponent (scoreMesh);
        MeshRenderer meshRenderer = scoreText.AddComponent(meshRenderer);*/
        CurrentScore += scoreIncrement;
        //scoreMesh.transform.position = new Vector3(5, 5, 0);
        scoreMesh.text = "SCORE: " + CurrentScore;
        scoreMesh.fontSize = 255;
        scoreMesh.characterSize = .03f;
        print(CurrentScore);
        /*https://www.dafont.com/black-santa.font - Font - Black Santa - Created by imagex */
    }


    // Use this for initialization
    void Start () {
        PopUpScoreControl myScore = FindObjectOfType<PopUpScoreControl>();
        scoreMesh = GetComponent<TextMesh>();
        AddScore(0);
        scoreMesh.transform.parent = Camera.main.transform;
        scoreMesh.transform.position = new Vector2(4.5f, 5.7f);
        
        //Cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //scoreMesh.transform.parent = Camera.main.transform;
    }
	 
	// Update is called once per frame
	void Update () {
       /* if(CurrentScore >=300 && Count >= 0)
        {
            scoreMesh.text = "Score: " + CurrentScore + "\nCongrats Over 300 points";
            scoreMesh.characterSize = .03f;
            scoreMesh.transform.position = new Vector2(4,4);
        }
        Count--;*/
        //Testing Score

        /*if(Input.GetKeyDown(KeyCode.Space) && Validator == 1)
        {
            Destroy(Cube1);
            Increment += 100;
            Validator = 0;
            Cube1.transform.position = new Vector3(0, 0, 0);
            tree1.text = "Score: " + Increment;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && Validator == 0)
        {
            print("No 1");
            print(Increment);
        }

        if(Input.GetKeyDown(KeyCode.S) && Validator == 0)
        {
            Cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Validator = 1;
        }
        else if(Input.GetKeyDown(KeyCode.S) && Validator == 1)
        {
            print("No");
            print(Increment);

        }*/


    }

    // Update is called once per frame

}

