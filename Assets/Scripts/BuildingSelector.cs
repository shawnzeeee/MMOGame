using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

class BuildingSelector : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject prefab;
    private MyCamera player;
    public void OnButtonClick()
    {  
        player = playerObject.GetComponent<MyCamera>();
        //String name = this.gameObject.name; 
        //String objectName = this.gameObject.name.Replace("Icon","Farm");
         //Debug.Log(objectName);
        //GameObject targetObject = GameObject.Find(objectName);
        //Debug.Log(targetObject.name);
       // Debug.Log(prefab);
        player.triggerMenuSelection(prefab);
    } 
}