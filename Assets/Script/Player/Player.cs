using System;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    // PlayerInputs _inputs;
    public NavMeshAgent agent;

    [SerializeField] ParticleSystem clickEffect;
    [SerializeField] LayerMask clickbleLayers;

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

    void Awake()
    {
        StateMachine = new StateMachine();
        Idle = new Idle(this);
        Walker = new Walker(this);
        StateMachine.ChangeState(Idle);

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

    }

    void OnEnable()
    {
        GameEventsManager.instance.InputEvent.OnTouchEvent += ClickToMove;
    }

    void OnDisable()
    {
        GameEventsManager.instance.InputEvent.OnTouchEvent -= ClickToMove;
    }
    
    void Start()
    {
        TargetPosition = transform.position;
    }

    void Update()
    {
        StateMachine.Update();

        speed = agent.velocity.magnitude / maxSpeed;
        anim.SetFloat("speed", speed);

        RotateBodyToFaceTouch();
    }

    void FixedUpdate()
    {
        StateMachine.FixedUpdate();
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
    }

    public void RotateBodyToFaceTouch()
    {
        if (direction == Vector3.zero) return;

        Vector3 lookDirection = new Vector3(direction.x, 0f, direction.z);
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
    }

    void ClickToMove(Vector2 touchPosition)
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(touchPosition), out hit, 100, clickbleLayers))
        {
            agent.destination = hit.point;
            TargetPosition = hit.point;

            if (clickEffect != null)
            {
                ParticleSystem instantiatedEffect = Instantiate(clickEffect, hit.point + new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
                Destroy(instantiatedEffect.gameObject, instantiatedEffect.main.duration + instantiatedEffect.main.startLifetime.constantMax);
            }

            direction = (agent.destination - transform.position).normalized;
            StateMachine.ChangeState(Walker);
        }
    }
}
