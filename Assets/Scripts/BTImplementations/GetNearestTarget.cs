using UnityEngine;

public class GetNearestTarget : Task
{
    public override bool Execute()
    {
        GetNearestActor();
        return base.Execute();
    }

    private void GetNearestActor() {
        float closestActorDistance = Mathf.Infinity;
        ActorController closestActor = null;
        foreach (ActorController actor in GameController.instance.Players)
        {
            float dist = Vector3.Distance(transform.localPosition, actor.transform.localPosition);
            if (dist < closestActorDistance && actor != GetComponent<ActorController>() && actor != GameController.instance.LastActorTagged)
            {
                closestActorDistance = dist;
                closestActor = actor;
            }
        }
        GameController.instance.TargetActor = closestActor;
    }
}
