using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Craftable
{
    public string itemName;
    [Space]
    public GameObject itemOutput;
    public List<GameObject> ingredients;
}
