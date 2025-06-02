using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    public static AsteroidManager instance;

    [SerializeField] List<GameObject> bigAsteroidsPool = new List<GameObject>();
    [SerializeField] List<GameObject> smallAsteroidsPool = new List<GameObject>();
    [SerializeField] private float spawnCooldown = 1f;

    private GameObject asteroidPrefab;
    private GameObject smallAsteroidPrefab;

    private void Start()
    {
        if (instance == null) instance = this;
        asteroidPrefab = Resources.Load<GameObject>("Prefabs/Asteroid");
        smallAsteroidPrefab = Resources.Load<GameObject>("Prefabs/SmallAsteroid");
    }

 
    private void StartGame()
    {
        StartCoroutine(SpawnAsteroids());
    }

    private void StopGame()
    {
        StopAllCoroutines();
        foreach (Transform asteroid in transform)
        {
            AddToPool(asteroid.GetComponent<Asteroid>());
            asteroid.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        Menu.onStartGame += StartGame;
        Menu.onRestart += StopGame;
    }

    private void OnDisable()
    {
        Menu.onStartGame -= StartGame;
        Menu.onRestart -= StopGame;
    }

    IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            SpawnBigAsteroid();
            yield return new WaitForSeconds(spawnCooldown);
        }
    }

    internal static void SpawnSmallerAsteroids(Vector3 position)
    {
        int amount = Random.Range(2, 4);

        // create new asteroids if not in pool
        while (instance.smallAsteroidsPool.Count < amount)
        {
            GameObject newAsteroid = Instantiate(instance.smallAsteroidPrefab, instance.transform);
            newAsteroid.SetActive(false);
            instance.smallAsteroidsPool.Add(newAsteroid);
        }

        for (int i = 0; i < amount; i++)
        {
            int lastIndex = instance.smallAsteroidsPool.Count - 1;
            GameObject asteroid = instance.smallAsteroidsPool[lastIndex];
            instance.smallAsteroidsPool.RemoveAt(lastIndex);

            Vector3 offset = (Vector3)Random.insideUnitCircle.normalized * 2f;
            asteroid.transform.position = position + offset;
            asteroid.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
            asteroid.SetActive(true);
        }
    }

    private Vector2 GetRandomPositionOnBounds()
    {
        Vector2 viewportPosition = Vector2.zero;

        int side = Random.Range(0, 4); // 0=Top, 1=Bottom, 2=Left, 3=Right

        switch (side)
        {
            case 0:
                viewportPosition = new Vector2(Random.value, 1f);
                break;
            case 1:
                viewportPosition = new Vector2(Random.value, 0f);
                break;
            case 2:
                viewportPosition = new Vector2(0f, Random.value);
                break;
            case 3: 
                viewportPosition = new Vector2(1f, Random.value);
                break;
        }

        Vector3 worldPosition = Camera.main.ViewportToWorldPoint(new Vector3(viewportPosition.x, viewportPosition.y, Camera.main.nearClipPlane));
        worldPosition.z = 0f; 

        return worldPosition;
    }

    public void SpawnBigAsteroid()
    {
        if (bigAsteroidsPool.Count == 0)
        {
            var asteroid = Instantiate(asteroidPrefab, instance.transform);
            asteroid.transform.position = GetRandomPositionOnBounds();
        }
        else
        {
            var asteroid = bigAsteroidsPool[0];
            asteroid.SetActive(true);
            bigAsteroidsPool.Remove(asteroid);
            asteroid.transform.position = GetRandomPositionOnBounds();
        }
    }

    internal static void AddToPool(Asteroid asteroid)
    {
        switch (asteroid.asteroidSize)
        {
            case AsteroidSize.BIG:
                instance.bigAsteroidsPool.Add(asteroid.gameObject); break;
            case AsteroidSize.SMALL:
                instance.smallAsteroidsPool.Add(asteroid.gameObject); break;
            default:
                break;
        }
    }
}
