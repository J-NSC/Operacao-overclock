using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public StateMachine StateMachine;
    
    public Idle Idle;
    public Walker Walker;
    
    public Vector3 direction;

    public float maxSpeed = 5f;
    public float speed;
    public Vector3 TargetPosition;
    public bool IsMoving = false;

    public Animator anim;
    public Rigidbody rb;
    
    private void Awake()
    {
        StateMachine = new StateMachine();
        Idle = new Idle(this);
        Walker = new Walker(this);
        StateMachine.ChangeState(Idle);

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        TargetPosition = transform.position;
    }

    private void Update()
    {
        StateMachine.Update();
        InputCheck();
        
        float velocity = rb.linearVelocity.magnitude;
        speed = velocity / maxSpeed;
        anim.SetFloat("speed", speed);
        RotateBodyToFaceTouch();

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

    public void RotateBodyToFaceTouch()
    {
        if (direction == Vector3.zero) return;
    
        Vector3 lookDirection = new Vector3(direction.x, 0f, direction.z); 
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
    
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
    }

}
