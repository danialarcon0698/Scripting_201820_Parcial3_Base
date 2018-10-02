using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Collider))]
public abstract class ActorController : MonoBehaviour
{
    [SerializeField]
    protected Color baseColor = Color.blue;
    protected Color taggedColor = Color.red;
    protected MeshRenderer renderer;

    public delegate void OnActorTagged(bool val);
    public OnActorTagged onActorTagged;

    public bool IsTagged { get; protected set; }

    private NavMeshAgent agent;
    public NavMeshAgent Agent
    {
        get
        {
            return agent;
        }

        set
        {
            agent = value;
        }
    }

    float agentSpeed = 5f;

    // Use this for initialization
    protected virtual void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        renderer = GetComponent<MeshRenderer>();

        SetTagged(false);
        Agent.speed = agentSpeed;

        onActorTagged += SetTagged;
        GameController.OnGameFinish += StopAllAgents;
    }

    private void StopAllAgents() {
        Agent.isStopped = true;
    }

    protected abstract Vector3 GetTargetLocation();

    protected void MoveActor()
    {
        Agent.SetDestination(GetTargetLocation());
    }

    protected void OnCollisionEnter(Collision collision)
    {
        ActorController otherActor = collision.gameObject.GetComponent<ActorController>();

        if (otherActor != null)
        {
            print("collided!");

            otherActor.onActorTagged(true);
            onActorTagged(false);
        }
    }

    protected virtual void OnDestroy()
    {
        Agent = null;
        renderer = null;
        onActorTagged -= SetTagged;
    }

    private void SetTagged(bool val)
    {
        IsTagged = val;

        if (renderer)
        {
            print(string.Format("Changing color to {0}", gameObject.name));
            renderer.material.color = val ? taggedColor : baseColor;
        }
    }
}