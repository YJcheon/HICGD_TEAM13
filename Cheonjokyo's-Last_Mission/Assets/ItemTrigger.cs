using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Player 아이템 획득.");

            // TODO: 아이템 행동 구현 연결

            Destroy(this.gameObject);
        }
    }
}
