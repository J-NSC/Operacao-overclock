using UnityEngine;

public class Walker : State
{
    
    private Player _player;
    public Walker(Player player) : base("Walker")
    {
        _player = player;
    }


    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }


    public override void Update()
    {
        base.Update();
        

        if (_player.direction == Vector3.zero)
        {
            _player.StateMachine.ChangeState(_player.Idle);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
  

        if (_player.IsMoving)
        {
            _player.transform.position += _player.direction * (_player.maxSpeed * Time.deltaTime);

            if (Vector3.Distance(_player.transform.position, _player.TargetPosition) < 0.1f)
            {
                _player.IsMoving = false;
                _player.direction = Vector3.zero; 
            }
        }
    }
}
