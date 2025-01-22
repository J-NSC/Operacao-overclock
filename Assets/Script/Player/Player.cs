using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public StateMachine StateMachine;
    
    public Idle Idle;
    public Walker Walker;
    
    public Vector3 direction;

    public float speed = 5f;
    public Vector3 TargetPosition;
    public bool IsMoving = false;

    private void Awake()
    {
        StateMachine = new StateMachine();
        Idle = new Idle(this);
        Walker = new Walker(this);
        StateMachine.ChangeState(Idle);
    }

    private void Start()
    {
        TargetPosition = transform.position;
    }

    private void Update()
    {
        StateMachine.Update();
        InputCheck();
        
        Debug.Log("Direção do Movimento: " + direction);
    }

    private void FixedUpdate()
    {
        StateMachine.FixedUpdate();
    }

    private void InputCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                TargetPosition = hit.point;
                IsMoving = true;
            }
        }
        
        direction = (TargetPosition - transform.position).normalized;
    }
}
