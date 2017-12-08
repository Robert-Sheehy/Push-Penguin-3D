
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItemControl : MonoBehaviour
{
    enum ItemState { growing, waiting, shrinking, destroying }

    ItemState currentState = ItemState.growing;
    private float rotSpeed = 360.0f;
    private int _points = 100;
    private int minimumPoints = 50;
    private int maximumPoints = 250;

    private float startingScale = 0.01f;
    private float growingTime = 2.0f, growingTimer = 0f;
    private float shrinkTime = 3.0f, shrinkingTimer = 0f;
    private float _timeAlive = 30; //Seconds



    public Transform Cherry, Apple, Banana;
    public Transform Popup;

    GameManagerControl theManager;

    void Start()
    {
        growingTimer = 0;
        transform.localScale = startingScale * Vector3.one;
        theManager = FindObjectOfType<GameManagerControl>();

    }
    void Update()
    {
        timeAlive -= Time.deltaTime;
        transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime);


        switch (currentState)
        {
            case ItemState.growing:

                grow();
                break;

            case ItemState.waiting:

                if (shrinkTime >= timeAlive) currentState = ItemState.shrinking;

                break;

            case ItemState.shrinking:

                shrink();


                break;

            case ItemState.destroying:


                theManager.ItemDestroyed(this);
                Destroy(gameObject);

                break;

        }
    }


    public int points
    {
        get
        {
            return _points;
        }

        set
        {

            if (value <= minimumPoints)
            {
                _points = minimumPoints;
            }
            else if (value >= maximumPoints)
            {
                _points = maximumPoints;
            }


        }
    }


    private float timeAlive
    {
        set
        {
            _timeAlive = value;
        }
        get
        {
            return _timeAlive;
        }
    }






    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PenguinControl>())
        {
            Transform newPopup = Instantiate(Popup, transform.position, Quaternion.identity);

            newPopup.GetComponent<PopUpScoreControl>().WithScoreOf(_points, transform.position);
            theManager.ItemDestroyed(this);
            Destroy(gameObject);
        }


    }



    internal void YouAre(GameManagerControl.ItemType typeOfItem, int Score, float time)
    {
        Transform part;
        switch (typeOfItem)

        {
            case GameManagerControl.ItemType.Apple:

                part = Instantiate(Apple, transform.position, Quaternion.identity);


                break;

            case GameManagerControl.ItemType.Cherry:

                part = Instantiate(Cherry, transform.position, Quaternion.identity);


                break;

            default:

                part = Instantiate(Banana, transform.position, Quaternion.identity);
                break;


        }

        part.transform.parent = transform;

        //set Score
        this.points = Score;


        //set Countsown
        this.timeAlive = time;

    }

    private void grow()
    {
        growingTimer += Time.deltaTime;
        transform.localScale = Mathf.Lerp(startingScale, 1.0f, (growingTimer / growingTime)) * Vector3.one;
        if (growingTimer > growingTime) currentState = ItemState.waiting;
    }

    private void shrink()
    {
        shrinkingTimer += Time.deltaTime;
        transform.localScale = Mathf.Lerp(1.0f, startingScale, (shrinkingTimer / shrinkTime)) * Vector3.one;
        if (timeAlive < 0) currentState = ItemState.destroying;
    }



}
