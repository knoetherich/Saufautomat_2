using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : TacticsMove
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward);

        if(!turn)                                   //Wenn Spieler nicht dran ist, erlaube ihm nicht sich zu bewegen
        {
            return;                                 
        }

        if(!moving)
        {
            FindSelectableTiles();
            CheckMouse();
        }
        else
        {
            Move();
        }
    }

    void CheckMouse()
    {
        if(Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.tag == "Tile")
                {
                    Tile t = hit.collider.GetComponent<Tile>();

                    if(t.selectable)
                    {
                        MoveToTile(t);
                        
                    }
                }

                if(hit.collider.tag == "Player")
                {
                    Debug.Log("Player getouched");
                    //Tile t = hit.collider.GetComponent<Player>();

                    /**if(t.selectable)
                    {
                        MoveToTile(t);
                        
                    }**/
                }
            }
        }
    }
}
