using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;

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

    public int TimesTagged
    {
        get
        {
            return timesTagged;
        }
        set {
            timesTagged = value;
        }
    }
    int timesTagged;

    float agentSpeed = 5f;

    bool collisioned = false;

    private bool stunned = false;
    [SerializeField] float stunTime;

    // Use this for initialization
    protected virtual void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        renderer = GetComponent<MeshRenderer>();

        SetTagged(false);
        Agent.speed = agentSpeed;
        timesTagged = 0;

        onActorTagged += SetTagged;
        GameController.OnGameFinish += StopAllAgents;
        GameController.OnWinners += Winners;
    }

    private void Winners(List<ActorController> actors) {
        foreach (ActorController a in actors) {
            a.renderer.material.color = Color.yellow;
        }
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
        GameObject otherActor = collision.gameObject;

        if (collision.gameObject.GetComponent<Rock>() != null && GetComponent<Rock>() == null) {
            gameObject.AddComponent<Rock>();
        }

        if (otherActor != null && otherActor.GetComponent<ActorController>()!=null)
        {
            if (IsTagged && collisioned)
            {
                GameController.instance.LastActorTagged = this;

                otherActor.GetComponent<ActorController>().timesTagged++;

                print("collided!");
                otherActor.GetComponent<ActorController>().onActorTagged(true);
                onActorTagged(false);
            }
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

        if (IsTagged)
            StartCoroutine(CoolDown());

        if (renderer)
        {
            print(string.Format("Changing color to {0}", gameObject.name));
            renderer.material.color = val ? taggedColor : baseColor;
        }
    }

    IEnumerator CoolDown() {
        yield return new WaitForSeconds(4);
        collisioned = true;
    }

    public IEnumerator Stunned() {
        agent.speed = 0;
        yield return stunTime;
        agent.speed = agentSpeed;
    }
}