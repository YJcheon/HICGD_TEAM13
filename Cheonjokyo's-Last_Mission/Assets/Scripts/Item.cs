using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public virtual IEnumerator effects(GameManager manager)
    {
        // something;
        yield return null;
    }
}
