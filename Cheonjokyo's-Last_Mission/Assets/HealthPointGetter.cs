using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPointGetter : MonoBehaviour
{
    private GameManager manager;
    private Text text;
    private double HP;

    // Update is called once per frame
    void Start()
    {
        GameObject managerObject = GameObject.FindWithTag("GameController");
        manager = managerObject.GetComponent<GameManager>();
        text = GetComponent<Text>();
    }

    void Update()
    {
        HP = manager.player.HP;
        text.text = string.Format("{0:00.00}", HP);
    }
}
