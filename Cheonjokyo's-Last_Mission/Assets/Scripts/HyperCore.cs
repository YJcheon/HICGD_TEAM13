using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperCore : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        { 
            Debug.Log("겜끝");
           
        }
    }

}
