using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Worker : MonoBehaviour
{
    // Start is called before the first frame update
        [SerializeField] public float speed; // Speed of the player     
        //private UnityEngine.AI.NavMeshAgent agent;
        private int level;
        private int workSpeed;
        Structure targetStructure;
        GameObject targetObject;

        private Rigidbody2D rb;

        public Dictionary<string, int> jobs = new Dictionary<string, int>(){{"Farming",0}, {"Transporting",1}}; 
        public string currentJob;

        [SerializeField] private Tasks tasks;
        private bool walkToFarm = false;
        private enum Skills{
            Farming,
            Transporting,
        }
        private enum State{
            Idle,
            Working,
        }
        State currentState;
    void Start()
    {
        // Initialize the agent
       // agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        level = 1;
        workSpeed = 50;
        currentState = State.Idle;
    }
    private void OnTaskCompleted(object source, EventArgs args)      
    {
        Debug.Log("Switching States");  
        targetStructure.TaskCompleted -= OnTaskCompleted;
        currentState = State.Idle;
    }
    void farming(){
        Vector3 direction = (targetObject.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, targetObject.transform.position) < 1.0f){
            targetStructure.updateTaskCompletion(workSpeed, false);
        }
    }

    void transporting(){
    GameObject nearestChest = FindNearestChest();
    if (!walkToFarm)
    {
        Vector3 direction = (targetObject.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, targetObject.transform.position) < 1.0f)
        {
            walkToFarm = true;
        }
    }
    else if (nearestChest != null)
    {
        Vector3 direction = (nearestChest.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, nearestChest.transform.position) < 1.0f)
        {
            targetStructure.updateTaskCompletion(workSpeed, true);
            walkToFarm = false; // Reset for the next time
        }
    }
    }
    GameObject FindNearestChest()   
    {
        GameObject nearestChest = null;
        float minDistance = Mathf.Infinity;
        foreach (GameObject chest in ChestManager.Chests)
        {
            float distance = Vector3.Distance(transform.position, chest.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestChest = chest;
            }
        }
        return nearestChest;
    }
    public void setStateIdle(){
        currentState = State.Idle;
    }
    
    void Update()
    {   

        if (currentState == State.Idle){
            foreach (Skills skill in Skills.GetValues(typeof(Skills))){
                int index = tasks.getColLength()*level + tasks.stateValues[skill.ToString()];
                if (tasks.getQueue(index).Count > 0){
                    targetObject = tasks.getQueue(index).Peek();
                    targetStructure = targetObject.GetComponent<Structure>();
                    if(targetStructure.checkFull() == false){
                        targetStructure.addWorker();
                        currentJob = targetStructure.getCurrentState().ToString();
                        targetStructure.TaskCompleted += OnTaskCompleted;
                        currentState = State.Working;
                    }
                    break;
                }
            }
        }
        if (currentState == State.Working){
            if(jobs[currentJob] == jobs["Farming"]){
                farming();
            }
            if(jobs[currentJob] == jobs["Transporting"]){
                transporting();
            }
        }
    }
    
}
