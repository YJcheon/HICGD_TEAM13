using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimeGetter : MonoBehaviour
{
    private GameManager manager;
    private Text text;
    private double time;

    // Update is called once per frame
    void Start()
    {
        GameObject managerObject = GameObject.FindWithTag("GameController");
        manager = managerObject.GetComponent<GameManager>();
        text = GetComponent<Text>();
    }

    void Update()
    {
        time = manager.timer.RestTime;
        text.text = string.Format("{0:00.00}", time);
    }
}
