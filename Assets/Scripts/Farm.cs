using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Farm : Structure
{   

    void Start()
    {
        currentState = State.Farming;
        taskCompletionValue = 0;
        level = 1;
        showTask = false;
        numWorkers = 0;
        maxWorkers = 4;
        //this.gameObject.SetActive(false);
    }

    /*
    * This method updates the completion of a task
    */
    override public bool updateTaskCompletion(int workSpeed, bool instantComplete){
        if (instantComplete){
            switchState();
            OnTaskCompleted();
            return true;
        }
        if (taskCompletionValue < 100){
            taskCompletionValue = (float)(workSpeed) * Time.deltaTime + taskCompletionValue;
            if(taskCompletionValue >= 100){
                switchState();
                OnTaskCompleted();
                return true;
            }
        }
       // completionBar.updateBar(taskCompletionValue, maxCompletionValue);
        return false;
    }
    private void switchState(){
        switch (currentState)
        {
            case State.Farming:
                taskCompletionValue = 0;
                numWorkers = 0;
                currentState = State.Transporting;
                break;
            case State.Transporting:
                taskCompletionValue = 0;
                numWorkers = 0;
                currentState = State.Farming;
                break;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (numWorkers < maxWorkers && !showTask){
            addTask();
            showTask = true;
        }
    }
}