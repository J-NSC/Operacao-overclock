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
        player.agent.isStopped = true;
    }

    public override void Update()
    {
        base.Update();
        
        // Se o agente tiver um destino diferente da posição atual, mudar para Walking
        if (player.agent.remainingDistance > 0.1f)
        {
            player.StateMachine.ChangeState(player.Walker);
        }
    }
}