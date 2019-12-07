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
            if (this.gameObject.name == "Purification")
            {
                StartCoroutine(doPurification());
            }
            else if (this.gameObject.name == "Bandage")
            {
                StartCoroutine(doBandage());
            }
            else if (this.gameObject.name == "Hint")
            {
                StartCoroutine(doHint(5.0f));
            }
            else
            {
                Debug.Log("Nothing to execute!");
            }

        }
    }

    private IEnumerator doPurification()
    {
        Debug.Log("Purification 획득.");
        manager.timer.increase(5.0f);
        Destroy(this.gameObject);
        yield return null;
    }

    private IEnumerator doBandage()
    {
        Debug.Log("Bandage 획득.");
        manager.player.increaseHealthPoint(3);
        Debug.Log(manager.player.HP);
        Destroy(this.gameObject);
        yield return null;
    }

    private IEnumerator doHint(float seconds)
    { 
        Debug.Log("Hint 획득.");
        Camera.main.clearFlags = CameraClearFlags.Skybox;
        Camera.main.rect = new Rect(0f, 0f, 1f, 1f);
        yield return new WaitForSeconds(seconds);
        Camera.main.clearFlags = CameraClearFlags.Depth;
        Camera.main.rect = new Rect(0f, 0f, 0.5f, 0.5f);
        Debug.Log("Finish. ShowTopView");
        Destroy(this.gameObject);
    }
}
