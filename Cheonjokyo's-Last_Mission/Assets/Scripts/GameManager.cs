using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Maze mazePrefab;

	public Player playerPrefab;

    public Timer timerPrefab;

	private Maze mazeInstance;

    private Player playerInstance;

    private Timer timerInstance;

    public GameObject canvas;

    private bool restart;


    private void Start () {
        Time.timeScale = 1.0f;
        StartCoroutine(BeginGame());
	}
	
	private void Update () {
        if (playerInstance == null && timerInstance == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
			RestartGame();
		}

        if (timerInstance.RestTime < 0.0f && !restart)
        {
            Debug.Log("Game Over:: time's up");
            restart = true;
            RestartGame();
        }
	}

	private IEnumerator BeginGame () {
		Camera.main.clearFlags = CameraClearFlags.Skybox;
		Camera.main.rect = new Rect(0f, 0f, 1f, 1f);
		mazeInstance = Instantiate(mazePrefab) as Maze;
		yield return StartCoroutine(mazeInstance.Generate());
		playerInstance = Instantiate(playerPrefab) as Player;
		playerInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
        timerInstance = Instantiate(timerPrefab) as Timer;
		Camera.main.clearFlags = CameraClearFlags.Depth;
        Camera.main.rect = new Rect(0f, 0f, 0.5f, 0.5f);

        canvas.SetActive(true);
	}

	private void RestartGame () {
        canvas.SetActive(false);
		StopAllCoroutines();
        Destroy(mazeInstance.HyperCore.gameObject);
        foreach(Item item in mazeInstance.Items)
        {
            if (item == null)
            {
                continue;
            }
            Destroy(item.gameObject);
        }
        foreach (Alien alien in mazeInstance.Aliens)
        {
            Destroy(alien.gameObject);
        }

        Destroy(mazeInstance.gameObject);
		if (playerInstance != null) {
			Destroy(playerInstance.gameObject);
		}
        if (timerInstance != null)
        {
            Destroy(timerInstance.gameObject);
        }
        StartCoroutine(BeginGame());
        restart = false;
	}

    public Timer timer
    {
        get { return timerInstance; }
    }

    public Player player
    {
        get { return playerInstance; }
    }
}