using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

struct PlayerStates
{
    public string name;
    public int pos;
    public float time;

    public PlayerStates(string n, int p, float t)
    {
        name = n;
        pos = p;
        time = t;
    }
}

public class Leaderboard 
{
    static Dictionary<int, PlayerStates> lb = new Dictionary<int, PlayerStates>();
    static int carsRegistered = -1;

    public static int RegCar(string name) 
    {
        carsRegistered++;
        lb.Add(carsRegistered, new PlayerStates(name, 0, 0));
        return carsRegistered;

    }
    public static void Reset() 
    {
        lb.Clear();
        carsRegistered = -1;
    }
    public static void setPos(int rego, int lap, int checkpoint , float time)
    {
        int postion = lap * 1000 + checkpoint;
        lb[rego] = new PlayerStates(lb[rego].name, postion , time);

    }
    public static string getPos(int rego) 
    {
        int index = 0;
        foreach (KeyValuePair<int,PlayerStates> pos in lb.OrderByDescending(key=>key.Value.pos).ThenBy(key => key.Value.time)) 
        {
            index++;
            if (pos.Key == rego) 
            {
                switch(index)
                {
                    case 1: return "1st";
                    case 2: return "2nd";
                    case 3: return "3rd";
                    case 4: return "4th";
                    case 5: return "5th";
                    case 6: return "6th";
                }
            }

        }
        return "Unknown";
    }
}
