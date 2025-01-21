using UnityEngine;

public class StateMachine
{
     private State currentState;
     public string currentStateName { get; private set; }

     public void Update()
     {
          currentState?.Update();
     }

     public void FixedUpdate()
     {
          currentState?.FixedUpdate();
     }

     public void ChangeState(State newState)
     {
          currentState?.Exit();
          currentState = newState;
          currentStateName = newState.name;
          currentState?.Enter();
     }
}
