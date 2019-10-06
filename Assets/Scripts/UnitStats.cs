using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour, IComparable
{
    [SerializeField] float health;
    [SerializeField] float mana;
    [SerializeField] float attack;
    [SerializeField] float magic;
    [SerializeField] float defense;
    [SerializeField] float speed = 1;

    // derived
    int order;

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
