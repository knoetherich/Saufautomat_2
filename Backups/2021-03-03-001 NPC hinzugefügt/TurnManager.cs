using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    //
    static Dictionary<string, List<TacticsMove>> units = new Dictionary<string, List<TacticsMove>>();
    static Queue<string> turnKey = new Queue<string>();     
    static Queue<TacticsMove> turnTeam = new Queue<TacticsMove>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (turnTeam.Count == 0)
        {
            InitTeamTurnQueue();
        }
    }

    static void InitTeamTurnQueue()
    {
        List<TacticsMove> teamList = units[turnKey.Peek()];         //turnKey holds Team thats currently active

        foreach (TacticsMove unit in teamList)
        {
            turnTeam.Enqueue(unit);
        }

        StartTurn();
    }

    static void StartTurn()                                         //Ein Spieler des Teams macht seinen Zug
    {
        if (turnTeam.Count > 0)
        {
            turnTeam.Peek().BeginTurn();
        }
    }

    //Der Zug ist vorbei, entweder nächster Spieler oder anderes Team ist dran
    public static void EndTurn()                                       
    {
        TacticsMove unit = turnTeam.Dequeue();
        unit.EndTurn();

        if(turnTeam.Count > 0)                          //Nächster Spieler
        {
            StartTurn();
        }
        else                                            //Teamwechsel
        {
            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);

            InitTeamTurnQueue();
        }
    }

    public static void AddUnit(TacticsMove unit)
    {
        List<TacticsMove> list;

        if(!units.ContainsKey(unit.tag))
        {
            list = new List<TacticsMove>();

            units[unit.tag] = list;

            if(!turnKey.Contains(unit.tag))
            {
                turnKey.Enqueue(unit.tag);
            }
            else
            {
                list = units[unit.tag];
            }

            list.Add(unit);
        } 
    }
}
