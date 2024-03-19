using System.Collections;
using System.Collections.Generic;
using UnityEngine;
class Chest : MonoBehaviour
{
    [SerializeField] private ChestManager chestManager;
    void Start()
    {
    }
    private void OnEnable() 
    {
        ChestManager.RegisterChest(this.gameObject);
    }

    private void OnDisable()
    {
        ChestManager.UnregisterChest(this.gameObject);
    }
}
