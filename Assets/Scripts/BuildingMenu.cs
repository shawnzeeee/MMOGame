using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
public class BuildingMenu : MonoBehaviour
{
    public GameObject menu; // Assign your popup menu in the inspector


    public void toggleActive()
    {
        menu.SetActive(!menu.activeSelf); // Toggle the popup menu
    }
    void Start()
    {
        menu.SetActive(false); // Initially disable the popup menu
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            menu.SetActive(!menu.activeSelf);
        }
    }
}