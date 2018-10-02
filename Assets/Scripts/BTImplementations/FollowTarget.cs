/// <summary>
/// Task that instructs ControlledAI to follow its designated 'target'
/// </summary>
public class FollowTarget : Task
{
    public override bool Execute()
    {
        ControlledAI.Agent.SetDestination(GameController.instance.TargetActor.transform.position);
        return base.Execute();
    }
}