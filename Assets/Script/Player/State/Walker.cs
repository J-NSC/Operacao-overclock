using UnityEngine;

public class Walker : State
{
    Player player;

    public Walker(Player player) : base("Walker")
    {
        this.player = player;
    }

    public override void Enter()
    {
        base.Enter();
        player.IsMoving = true;
        player.anim.Play("Walking");
        player.agent.isStopped = false;
    }

    public override void Update()
    {
        base.Update();
        
        player.direction = (player.agent.destination - player.transform.position).normalized;

        if (player.agent.remainingDistance <= 0.1f)
        {
            player.StateMachine.ChangeState(player.Idle);
        }
    }
}