using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintItem : Item
{
    public override IEnumerator effects(GameManager manager)
    {
        Debug.Log("Hint 획득.");
        Camera.main.clearFlags = CameraClearFlags.Skybox;
        Camera.main.rect = new Rect(0f, 0f, 1f, 1f);
        yield return new WaitForSeconds(3.0f);
        Camera.main.clearFlags = CameraClearFlags.Depth;
        Camera.main.rect = new Rect(0f, 0f, 0.5f, 0.5f);
        yield return new WaitForSeconds(0.01f);
        Debug.Log("Finish. ShowTopView");
        Destroy(this.gameObject);
    }
}
