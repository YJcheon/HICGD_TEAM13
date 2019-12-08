﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour {

	public IntVector2 size;

    public HyperCore coreprefab;
    public HyperCore coreInstance;

    public Alien alienprefab;
    public Alien alienInstance1 ;
    public Alien alienInstance2;

    public MazeCell cellPrefab;

    public Item[] itemPrefabs;

    public float generationStepDelay;

	public MazePassage passagePrefab;

	public MazeDoor doorPrefab;

	[Range(0f, 1f)]
	public float doorProbability;

	public MazeWall[] wallPrefabs;

	public MazeRoomSettings[] roomSettings;

	private MazeCell[,] cells;

	private List<MazeRoom> rooms = new List<MazeRoom>();

    public struct checkcoord {
        public int x;
        public int z;
      
    }

    List<checkcoord> checkcoordlist = new List<checkcoord>();

    public IntVector2 RandomCoordinates {
		get {
			return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
		}
	}

	public bool ContainsCoordinates (IntVector2 coordinate) {
		return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
	}

	public MazeCell GetCell (IntVector2 coordinates) {
		return cells[coordinates.x, coordinates.z];
	}
    public bool Coordchecking(int x, int z) {
        foreach (checkcoord coord in checkcoordlist) {
            if (coord.x == x && coord.z == z) {
                return false;
            }
        }
        return true;
    }
	public IEnumerator Generate () {
      
		WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
		cells = new MazeCell[size.x, size.z];
		List<MazeCell> activeCells = new List<MazeCell>();
		DoFirstGenerationStep(activeCells);
		while (activeCells.Count > 0) {
			yield return delay;
			DoNextGenerationStep(activeCells);
		}

		for (int i = 0; i < rooms.Count; i++) {
            rooms[i].Hide();
            int x = rooms[i].cells[0].coordinates.x;
            int z = rooms[i].cells[0].coordinates.z;
            checkcoord checkcoord = new checkcoord();
            checkcoord.x = x;
            checkcoord.z = z;
            checkcoordlist.Add(checkcoord);
            ItemInstance = Instantiate(itemPrefab, new Vector3(x - size.x * 0.5f + 0.5f, 0, z - size.z * 0.5f + 0.5f), Quaternion.identity) as Item;
            
        }

        IntVector2 aliencor;
        while (true)
        {
            IntVector2 cor = RandomCoordinates;
            if (Coordchecking(cor.x, cor.z) == true) {
                checkcoord checkcoord = new checkcoord();
                checkcoord.x = cor.x;
                checkcoord.z = cor.z;
                checkcoordlist.Add(checkcoord);
                aliencor = cor;
                break;
            }
        }
        alienInstance1 = Instantiate(alienprefab, new Vector3(aliencor.x - size.x * 0.5f + 0.5f, 0, aliencor.z - size.z * 0.5f + 0.5f), Quaternion.identity) as Alien;
        alienInstance1.SetLocation(aliencor.x - size.x * 0.5f + 0.5f, aliencor.z - size.z * 0.5f + 0.5f);
    

        while (true)
        {
            IntVector2 cor = RandomCoordinates;
            if (Coordchecking(cor.x, cor.z) == true)
            {
                checkcoord checkcoord = new checkcoord();
                checkcoord.x = cor.x;
                checkcoord.z = cor.z;
                checkcoordlist.Add(checkcoord);
                aliencor = cor;
                break;
            }
        }
        alienInstance2 = Instantiate(alienprefab, new Vector3(aliencor.x - size.x * 0.5f + 0.5f, 0, aliencor.z - size.z * 0.5f + 0.5f), Quaternion.identity) as Alien;
        alienInstance2.SetLocation(aliencor.x - size.x * 0.5f + 0.5f, aliencor.z - size.z * 0.5f + 0.5f);

        IntVector2 cor_result;
        while (true) {
            IntVector2 cor = RandomCoordinates;
            if (Coordchecking(cor.x, cor.z) == true)
            {
                cor_result = cor;
                break;
            }
        }
        coreInstance = Instantiate(coreprefab, new Vector3(cor_result.x - size.x * 0.5f + 0.5f, 0, cor_result.z - size.z * 0.5f + 0.5f),Quaternion.identity) as HyperCore;
	}

	private void DoFirstGenerationStep (List<MazeCell> activeCells) {
		MazeCell newCell = CreateCell(RandomCoordinates);
		newCell.Initialize(CreateRoom(-1));
		activeCells.Add(newCell);
	}

	private void DoNextGenerationStep (List<MazeCell> activeCells) {
		int currentIndex = activeCells.Count - 1;
		MazeCell currentCell = activeCells[currentIndex];
		if (currentCell.IsFullyInitialized) {
			activeCells.RemoveAt(currentIndex);
			return;
		}
		MazeDirection direction = currentCell.RandomUninitializedDirection;
		IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
		if (ContainsCoordinates(coordinates)) {
			MazeCell neighbor = GetCell(coordinates);
			if (neighbor == null) {
				neighbor = CreateCell(coordinates);
				CreatePassage(currentCell, neighbor, direction);
				activeCells.Add(neighbor);
			}
			else if (currentCell.room.settingsIndex == neighbor.room.settingsIndex) {
				CreatePassageInSameRoom(currentCell, neighbor, direction);
			}
			else {
				CreateWall(currentCell, neighbor, direction);
			}
		}
		else {
			CreateWall(currentCell, null, direction);
		}
	}
   
 
   
	private MazeCell CreateCell (IntVector2 coordinates) {
		MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
		cells[coordinates.x, coordinates.z] = newCell;
		newCell.coordinates = coordinates;
		newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
		return newCell;
	}

	private void CreatePassage (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		MazePassage prefab = Random.value < doorProbability ? doorPrefab : passagePrefab;
		MazePassage passage = Instantiate(prefab) as MazePassage;
		passage.Initialize(cell, otherCell, direction);
		passage = Instantiate(prefab) as MazePassage;
		if (passage is MazeDoor) {
			otherCell.Initialize(CreateRoom(cell.room.settingsIndex));
		}
		else {
			otherCell.Initialize(cell.room);
		}
		passage.Initialize(otherCell, cell, direction.GetOpposite());
	}

	private void CreatePassageInSameRoom (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		MazePassage passage = Instantiate(passagePrefab) as MazePassage;
		passage.Initialize(cell, otherCell, direction);
		passage = Instantiate(passagePrefab) as MazePassage;
		passage.Initialize(otherCell, cell, direction.GetOpposite());
		if (cell.room != otherCell.room) {
			MazeRoom roomToAssimilate = otherCell.room;
			cell.room.Assimilate(roomToAssimilate);
			rooms.Remove(roomToAssimilate);
			Destroy(roomToAssimilate);
		}
	}

	private void CreateWall (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		MazeWall wall = Instantiate(wallPrefabs[Random.Range(0, wallPrefabs.Length)]) as MazeWall;
		wall.Initialize(cell, otherCell, direction);
		if (otherCell != null) {
			wall = Instantiate(wallPrefabs[Random.Range(0, wallPrefabs.Length)]) as MazeWall;
			wall.Initialize(otherCell, cell, direction.GetOpposite());
		}
	}

	private MazeRoom CreateRoom (int indexToExclude) {
		MazeRoom newRoom = ScriptableObject.CreateInstance<MazeRoom>();
		newRoom.settingsIndex = Random.Range(0, roomSettings.Length);
		if (newRoom.settingsIndex == indexToExclude) {
			newRoom.settingsIndex = (newRoom.settingsIndex + 1) % roomSettings.Length;
		}
		newRoom.settings = roomSettings[newRoom.settingsIndex];
		rooms.Add(newRoom);
		return newRoom;
	}
}