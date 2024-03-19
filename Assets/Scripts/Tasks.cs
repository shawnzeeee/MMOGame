using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class Tasks : MonoBehaviour
{
    //private Queue<Task> farmingTasks = new Queue<Task>();
    //private Queue<Task> transportingTasks = new Queue<Task>();
    private int rowLength = 10;
    private int colLength = 10;
    private Queue<GameObject>[] gameObjectsQueues = new Queue<GameObject>[100]; 
    public Dictionary<string, int> stateValues = new Dictionary<string, int>(){{"Farming",0}, {"Transporting",1}};

    void Start(){
        for (int i = 0; i < gameObjectsQueues.Length; i++){
            gameObjectsQueues[i] = new Queue<GameObject>();
        }
    }


    private int getIndex(int level, int state){
        return level * colLength + state;
    }
    public void addTask (GameObject obj){
        Structure structure = obj.GetComponent<Structure>();
        int level = structure.getLevel();
        int state = stateValues[structure.getCurrentState().ToString()];

        if(state == stateValues["Farming"]){
            gameObjectsQueues[getIndex(level, state)].Enqueue(obj);
        }
        if(state == stateValues["Transporting"]){
            gameObjectsQueues[getIndex(level, state)].Enqueue(obj);
        }
    }
    public void sendTaskToBack(int index){
        GameObject obj = gameObjectsQueues[index].Dequeue();
        gameObjectsQueues[index].Enqueue(obj);
    }
    public void removeTask(GameObject obj){
        Structure structure = obj.GetComponent<Structure>();
        int level = structure.getLevel();
        int state = stateValues[structure.getCurrentState().ToString()]; 
        if(gameObjectsQueues[getIndex(level, state)].Count > 0){
            gameObjectsQueues[getIndex(level, state)].Dequeue();
        }     
    }
    public int getRowLength(){
        return rowLength;
    }
    public int getColLength(){
        return colLength;
    }
    public Queue<GameObject> getQueue(int index){
        return gameObjectsQueues[index];
    }
    void Update(){
        
    }
}
