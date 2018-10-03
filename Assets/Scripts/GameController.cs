using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [SerializeField] private float gameTime;
    [SerializeField] private int enemies;
    [SerializeField] GameObject enemy;
    [SerializeField] Collider plane;

    public float CurrentGameTime { get; private set; }

    public delegate void GameFinish();
    public static event GameFinish OnGameFinish;

    public delegate void Winners(List<ActorController> actor);
    public static event Winners OnWinners;

    private ActorController targetActor;
    public ActorController TargetActor
    {
        get
        {
            return targetActor;
        }

        set
        {
            targetActor = value;
        }
    }

    private ActorController[] players;
    public ActorController[] Players
    {
        get
        {
            return players;
        }

        set
        {
            players = value;
        }
    }

    List<ActorController> actorsWinners;

    private ActorController lastActorTagged;
    public ActorController LastActorTagged {
        get {
            return lastActorTagged;
        }
        set {
            lastActorTagged = value;
        }
    }

    public GameObject rock;
    [SerializeField] int rocksToSpawn;

    // Use this for initialization
    private IEnumerator Start()
    {
        instance = this;
        CurrentGameTime = gameTime;
        actorsWinners = new List<ActorController>();
        
        int enemiesToSpawn = Mathf.Clamp(enemies, 2, 4);

        for (int i = 0; i < enemiesToSpawn; i++) {
            Instantiate(enemy, GetSpawnPoint(), Quaternion.identity);
        }

        for (int i = 0; i < rocksToSpawn; i++)
        {
            Instantiate(rock, GetSpawnPoint(), Quaternion.identity);
        }

        // Sets the first random tagged player
        Players = FindObjectsOfType<ActorController>();

        yield return new WaitForSeconds(0.5F);
        int rand = Random.Range(0, Players.Length);
        Players[rand].onActorTagged(true);
        Players[rand].TimesTagged++;
    }

    private void Update()
    {
        CurrentGameTime -= Time.deltaTime;

        if (CurrentGameTime <= 0F)
        {
            //TODO: Send GameOver event.
            OnGameFinish();
            VerifyWinners();
        }
    }

    private Vector3 GetSpawnPoint()
    {
        Vector3 spawnPoint = new Vector3(Random.Range(plane.bounds.min.x, plane.bounds.max.x), 0, Random.Range(plane.bounds.min.z, plane.bounds.max.z));
        return spawnPoint;
    }

    private void VerifyWinners() {
        foreach (ActorController actor in players) {
            if (actor.TimesTagged == 0) {
                actorsWinners.Add(actor);
            }
        }
        OnWinners(actorsWinners);
    }
}