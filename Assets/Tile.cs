using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool walkable = true;    //Ist walkable wenn der Spieler drauf darf
    public bool current = false;    //Tile ist current wenn Spieler drauf steht
    public bool target = false;     //Tile wo der Spieler hinläuft
    public bool selectable = false; //Tiles die in der Reichweite des Spieler sind 

    public List<Tile> adjacencyList = new List<Tile>(); //Enthält die Nachbarn eines Tiles

    //Needed BFS (breadth fist search)
    public bool visited = false;    //Tile has been processed
    public Tile parent = null;
    public int distance = 0;

    //Needesfor AStar                   
    public float f = 0;     //  g+h
    public float g = 0;     //  Cost from parent to current Tile
    public float h = 0;     //  Cost from current Tile tio destination 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (current)
        {
            GetComponent<Renderer>().material.color = Color.magenta;
        }
        else if(target)
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else if (selectable)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }

    public void Reset()
    {   
        adjacencyList.Clear();
        current = false;    
        target = false;     
        selectable = false; 

        visited = false;    
        parent = null;
        distance = 0;

        f = g = h = 0;
    }

    public void FindNeighbors(float jumpHeight, Tile target)
    {
        Reset();

        CheckTile(Vector3.forward, jumpHeight, target);
        CheckTile(-Vector3.forward, jumpHeight, target);
        CheckTile(Vector3.right, jumpHeight, target);
        CheckTile(-Vector3.right, jumpHeight, target);
    }

    public void CheckTile(Vector3 direction, float jumpHeight, Tile target)
    {
        Vector3 halfExtents = new Vector3(0.25f, (1+ jumpHeight) / 2.0f, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

        foreach (Collider item in colliders)
        {
            Tile tile = item.GetComponent<Tile>();
            if(tile!=null && tile.walkable)
            {
                RaycastHit hit;         //Prüfen ob etwas bereits auf einem Tile steht 
                /**if (!Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1) || (tile == target))
                {
                    adjacencyList.Add(tile);
                }**/

                adjacencyList.Add(tile);
            }
        }
    }
}
