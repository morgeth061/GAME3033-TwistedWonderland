using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStateMachine : MonoBehaviour
{

    public GameObject[] objectsToSpawn;
    public int buffer;

    public State currentState { get; private set; }
    protected Dictionary<ZombieStateType, State> states;
    bool isRunning;
    // Start is called before the first frame update

    private void Awake()
    {
        states = new Dictionary<ZombieStateType, State>();
    }

    public void Initialize(ZombieStateType startingState)
    {
        if (states.ContainsKey(startingState))
        {
            ChangeState(startingState);
        }
    }

    public void AddState(ZombieStateType stateName, State state)
    {
        if (states.ContainsKey(stateName)) return;
        states.Add(stateName, state);
    }
    public void RemoveState(ZombieStateType stateName)
    {
        if (!states.ContainsKey(stateName)) return;
        states.Remove(stateName);
    }

    public void ChangeState(ZombieStateType nextState)
    {
        if (isRunning)
        {
            //stop current state
            StopRunningState();
        }
        if (!states.ContainsKey(nextState)) return;

        if (nextState == ZombieStateType.isDead)
        {
            float randomizer = Random.Range(0, buffer + objectsToSpawn.Length);
            if ((int)randomizer < objectsToSpawn.Length)
            {
                Instantiate(objectsToSpawn[(int)randomizer], new Vector3(transform.position.x, 2.5f, transform.position.z), transform.rotation);
            }
        }

        if (nextState == ZombieStateType.Attacking)
        {
            GetComponent<AudioSource>().Play();
        }
        currentState = states[nextState];
        currentState.Start();

        if (currentState.UpdateInterval > 0)
        {
            InvokeRepeating(nameof(IntervalUpdate), 0, currentState.UpdateInterval);
        }
        isRunning = true;
    }

    void StopRunningState()
    {
        isRunning = false;
        currentState.Exit();
        CancelInvoke(nameof(IntervalUpdate));
    }

    private void IntervalUpdate()
    {
        if (isRunning)
        {
            currentState.IntervalUpdate();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            currentState.Update();
        }
    }
}
