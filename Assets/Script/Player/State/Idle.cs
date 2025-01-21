using UnityEngine;

public class Idle : State
{
    
    private Player _player;
    public Idle(Player player) : base("Idle")
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

        if (_player.direction != Vector3.zero)
        {
            _player.StateMachine.ChangeState(_player.Walker);
        }
    }
}
