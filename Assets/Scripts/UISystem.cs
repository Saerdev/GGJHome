using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class UISystem : MonoBehaviour
{
    //6 Days
    //Increment Day
    //Switch to next level
    //Display current day
    //Darkness is triggered at stroy important moments

    // Start is called before the first frame update

    private float timer;
    private int day;
    public TextMeshProUGUI dayText;
    void Start()
    {
        timer = 5;
        day = 1;
        dayText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        //timer -= Time.deltaTime;
        dayText.text = "Day: " + day;

        if (timer < 0)
        {
            NextDay();
        }
    }

    void NextDay()
    {
        if (day < 3)
        {
            day++;
            timer = 5;
        }

        else
        {
            GameOver();
        }
    }

    void GameOver()
    {
        dayText.text = "Game Over!!!";
    }
}
