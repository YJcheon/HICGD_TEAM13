using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    int seed = 0;
    private MazeCell currentCell;
    private float x;
    private float z;
    private float[] x_move = { 0, 0, -1f, 1f }; //상하좌우
    private float[] z_move = { -1f, 1f, 0, 0 };
    //private MazeDirection currentDirection;
    private float sizex;
    private float sizez;
    private GameManager manager;

    public void SetSize(float sizex, float sizez) {
        this.sizex = sizex;
        this.sizez = sizez;
    }
    public void SetLocation(float x, float z) {
        this.x = x;
        this.z = z;
    }
    
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            manager.player.decreaseHealthPoint(10);
            Debug.Log("alien 꼴박.");
            Debug.Log(manager.player.HP);
        }
    }

    void Start()
    {
        GameObject managerObject = GameObject.FindWithTag("GameController");
        manager = managerObject.GetComponent<GameManager>();

    }
    private void Move()
    {
       ;
        int r_rand = Random.Range(0,4);
        //int r_rand = r.Next(4);
        
        float cx = this.x;
        float cz = this.z;

        float nx = cx + x_move[r_rand];
        float nz = cz + z_move[r_rand];
        if (nx >= -(sizex/2) && nx < sizex/2 && nz >= -(sizez/2) && nz < sizez/2)
        {
            this.x = nx;
            this.z = nz;
           
            transform.position = new Vector3(this.x, 0, this.z);
        }
        else
            return;
      
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move();
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move();
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move();
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move();
        }
        
    }
}
