using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ChestManager : MonoBehaviour
{
    public static List<GameObject> Chests = new List<GameObject>();

    public static void RegisterChest(GameObject chest)
    {
        Chests.Add(chest);
    }

    public static void UnregisterChest(GameObject chest)
    {
        Chests.Remove(chest);
    }
}