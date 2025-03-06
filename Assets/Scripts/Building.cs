using System;
using UnityEngine;

[Serializable]
public class Building
{
    public string name;
    public int cost;
    public GameObject prefab;

    public Building(string _name, int _cost, GameObject _prefab)
    {
        name = _name;
        cost = _cost;
        prefab = _prefab;
    }
}

