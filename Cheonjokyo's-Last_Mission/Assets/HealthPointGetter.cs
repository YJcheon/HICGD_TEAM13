using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPointGetter : MonoBehaviour
{
    private GameManager manager;
    public SimpleHealthBar healthBar;
    private double HP;

    // Update is called once per frame
    void Start()
    {
        GameObject managerObject = GameObject.FindWithTag("GameController");
        manager = managerObject.GetComponent<GameManager>();
    }

    void Update()
    {
        HP = manager.player.HP;

        float maxHp = HP > manager.player.initialHealthPoint
                        ? (float) HP
                        : (float) manager.player.initialHealthPoint;
        float currentHp = (float) HP;

        healthBar.UpdateBar(currentHp, maxHp);
        if (currentHp / maxHp < 0.3)
        {
            healthBar.UpdateColor(new Color(255, 0, 0));
        }
    }
}
