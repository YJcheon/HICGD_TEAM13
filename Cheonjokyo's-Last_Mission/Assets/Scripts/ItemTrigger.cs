using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : MonoBehaviour
{
    private GameManager manager;
    void Start ()
    {
        GameObject managerObject = GameObject.FindWithTag("GameController");
        manager = managerObject.GetComponent<GameManager>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Player 아이템 획득.");
            // TODO: 아이템 행동 구현 연결
            Item item = this.gameObject.GetComponent<Item>();
            if (item != null)
            {
                Debug.Log(item);
                Debug.Log(col.gameObject.name);
                StartCoroutine(item.effects(manager));
            }
        }
    }
}
