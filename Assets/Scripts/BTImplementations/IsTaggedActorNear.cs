using UnityEngine;
/// <summary>
/// Selector that succeeds if 'tagged' player is within a 'acceptableDistance' radius.
/// </summary>
public class IsTaggedActorNear : Selector
{
    [SerializeField]
    private float acceptableDistance = 5F;

    protected override bool CheckCondition()
    {
        return IsTaggedNear();
    }

    public bool IsTaggedNear()
    {
        bool taggedNear = false;
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, acceptableDistance, transform.forward, out hit))
        {
            if (hit.transform.GetComponent<ActorController>() != null && hit.transform.GetComponent<ActorController>().IsTagged)
                taggedNear = true;
        }
        return taggedNear;
    }
}