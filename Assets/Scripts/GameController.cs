using System.Collections;
using UnityEngine;

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

    // Use this for initialization
    private IEnumerator Start()
    {
        instance = this;
        CurrentGameTime = gameTime;

        int enemiesToSpawn = Mathf.Clamp(enemies, 1, 4);

        for (int i = 0; i < enemiesToSpawn; i++) {
            Instantiate(enemy, GetSpawnPoint(), Quaternion.identity);
        }

        // Sets the first random tagged player
        Players = FindObjectsOfType<ActorController>();

        yield return new WaitForSeconds(0.5F);

        Players[Random.Range(0, Players.Length)].onActorTagged(true);
    }

    private void Update()
    {
        CurrentGameTime -= Time.deltaTime;

        if (CurrentGameTime <= 0F)
        {
            //TODO: Send GameOver event.
            OnGameFinish();
        }
    }

    private Vector3 GetSpawnPoint()
    {
        Vector3 spawnPoint = new Vector3(Random.Range(plane.bounds.min.x, plane.bounds.max.x), 0, Random.Range(plane.bounds.min.z, plane.bounds.max.z));
        return spawnPoint;
    }
}