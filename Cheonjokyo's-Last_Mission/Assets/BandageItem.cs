using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandageItem : Item
{
    public override IEnumerator effects(GameManager manager)
    {
        Debug.Log("Purification 획득.");
        manager.timer.increase(5.0f);
        yield return new WaitForSeconds(0.01f);
        Destroy(this.gameObject);
    }
}
