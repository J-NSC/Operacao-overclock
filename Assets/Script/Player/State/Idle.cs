using UnityEngine;

public class Idle : State
{
    private Player player;

    public Idle(Player player) : base("Idle")
    {
        this.player = player;
    }

    public override void Enter()
    {
        base.Enter();
        player.IsMoving = false;
        player.anim.Play("Idle");
        // player.agent.isStopped = true;
    }

    public override void Update()
    {
        base.Update();
        
        if (player.agent.remainingDistance > 0.1f)
        {
            player.StateMachine.ChangeState(player.Walker);
        }
    }
}