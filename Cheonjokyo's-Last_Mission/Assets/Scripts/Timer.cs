using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public double initialTime;
    private double restTime { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        restTime = initialTime;
    }

    void initialize()
    {
    }

    // Update is called once per frame
    void Update()
    {
        restTime -= Time.deltaTime;
        Debug.Log("timer: " + restTime + " seconds");
    }

    public void increase(float seconds)
    {
        restTime += seconds;
    }

    public void decrease(float seconds)
    {
        restTime -= seconds;
    }

    public double RestTime { get { return restTime; } }
}
