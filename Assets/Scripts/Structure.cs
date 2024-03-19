using System;
using UnityEngine;
public enum State
{
    Farming,
    Transporting
}
public delegate void TaskCompletedEventHandler(object source, EventArgs args);

abstract public class Structure: MonoBehaviour
{
    public event TaskCompletedEventHandler TaskCompleted;   
    [SerializeField] public Tasks tasksObj;    // Reference to the Tasks object
    protected float taskCompletionValue; 
    protected float maxCompletionValue = 100f;
    protected int level;
    protected bool showTask;
    protected int maxWorkers;
    protected int numWorkers;
    [SerializeField] protected CompletionBar completionBar; // Reference to the CompletionBar object
    protected State currentState;

    public void addWorker()
    {
        if (numWorkers < maxWorkers)
        {
            numWorkers++;
            if (numWorkers >= maxWorkers)
            {
                removeTask();
                showTask = false;
            }
        }
    }
    public bool checkFull()
    {
        if (numWorkers < maxWorkers)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public State getCurrentState()
    {
        return currentState;
    }
    public int getLevel()
    {
        return level;
    }
    public virtual bool updateTaskCompletion(int workSpeed, bool instantComplete)
    {
        if (instantComplete || taskCompletionValue >= 100)
        {
            taskCompletionValue = 100;
            OnTaskCompleted();
            return true;
        }
        else
        {
            taskCompletionValue += workSpeed;
            taskCompletionValue = (float)(workSpeed) * Time.deltaTime + taskCompletionValue;
        }
        completionBar.updateBar(taskCompletionValue, maxCompletionValue);
        return false;
    }
    public void removeWorker()
    {
        if (numWorkers > 0)
        {
            numWorkers--;
            showTask = false;
        }
    }
    public void addTask()
    {
        tasksObj.addTask(this.gameObject);
    }
    public void removeTask()
    {
        tasksObj.removeTask(this.gameObject);
    }
    public void levelUp()
    {
        level++;
    }
    protected virtual void OnTaskCompleted()
    {
        TaskCompleted?.Invoke(this, EventArgs.Empty);
    }
}