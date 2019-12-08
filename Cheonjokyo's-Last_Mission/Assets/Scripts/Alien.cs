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
  
    public void SetLocation(float x, float z) {
        this.x = x;
        this.z = z;
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
        if (nx >= -10f && nx < 10f && nz >= -10f && nz < 10f)
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
