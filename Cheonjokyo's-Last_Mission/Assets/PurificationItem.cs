using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurificationItem : Item
{
    public override IEnumerator effects(GameManager manager)
    {
        Debug.Log("Bandage 획득.");
        manager.player.increaseHealthPoint(3);
        Debug.Log(manager.player.HP);
        yield return new WaitForSeconds(0.01f);
        Destroy(this.gameObject);
    }
}
