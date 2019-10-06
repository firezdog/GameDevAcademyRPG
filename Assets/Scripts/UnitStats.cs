using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour, IComparable
{
    public float health;
    public float mana;
    public float attack;
    public float magic;
    public float defense;
    public float speed = 1;

    // derived
    public int order;

    public int CompareTo(object other)
    {
        return  order - ((UnitStats) other).order;
    }

    // "turn" argument gets larger and larger to maintain order as battle advances
    public void SetOrder(int turn)
    {
        order = turn + (int) Math.Ceiling(100 / speed);
    }

    public override String ToString() {
        return gameObject.name;
    }

}
