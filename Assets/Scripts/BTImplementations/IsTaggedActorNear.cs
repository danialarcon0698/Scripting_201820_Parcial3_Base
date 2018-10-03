using UnityEngine;
/// <summary>
/// Selector that succeeds if 'tagged' player is within a 'acceptableDistance' radius.
/// </summary>
public class IsTaggedActorNear : Selector
{
    [SerializeField]
    private float acceptableDistance;

    protected override bool CheckCondition()
    {
        return TaggedNear();
    }

    private bool TaggedNear() {
        bool taggedNear = false;
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, acceptableDistance, transform.forward);

        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.GetComponent<ActorController>() != null && hit.transform.GetComponent<ActorController>().IsTagged)
                taggedNear = true;
        }
        return taggedNear;
    }
}