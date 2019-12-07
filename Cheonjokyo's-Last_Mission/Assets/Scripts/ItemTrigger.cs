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
                StartCoroutine(item.effects(manager));
            }
        }
    }

    private IEnumerator doPurification()
    {
        Debug.Log("Purification 획득.");
        manager.timer.increase(5.0f);
        yield return new WaitForSeconds(0.01f);
        Destroy(this.gameObject);
    }

    private IEnumerator doBandage()
    {
        Debug.Log("Bandage 획득.");
        manager.player.increaseHealthPoint(3);
        Debug.Log(manager.player.HP);
        yield return new WaitForSeconds(0.01f);
        Destroy(this.gameObject);
    }

    private IEnumerator doHint(float seconds)
    { 
        Debug.Log("Hint 획득.");
        Camera.main.clearFlags = CameraClearFlags.Skybox;
        Camera.main.rect = new Rect(0f, 0f, 1f, 1f);
        yield return new WaitForSeconds(seconds);
        Camera.main.clearFlags = CameraClearFlags.Depth;
        Camera.main.rect = new Rect(0f, 0f, 0.5f, 0.5f);
        yield return new WaitForSeconds(0.01f);
        Debug.Log("Finish. ShowTopView");
        Destroy(this.gameObject);
    }
}
