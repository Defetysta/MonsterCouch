using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class EnemyPool : MonoBehaviour
{
    [SerializeField]
    private GameObject defaultEnemyPrefab;
    [SerializeField]
    private int numberOfEnemiesToSpawn;
    [SerializeField]
    private Vector3 defaultEnemyScale;
    [SerializeField]
    private Vector2 areaHorizontalConstraints;
    [SerializeField]
    private Vector2 areaVerticalConstraints;
    
    private IObjectPool<GameObject> pool;

    private void Awake()
    {
        pool = new ObjectPool<GameObject>(
            createFunc: CreateItem,
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: OnDestroyItem,
            collectionCheck: true,
            defaultCapacity: numberOfEnemiesToSpawn,
            maxSize: 5000);

        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            GameObject newEnemyObject = pool.Get();
            Vector2 randomPosition = new Vector2(
                Random.Range(areaHorizontalConstraints.x, areaHorizontalConstraints.y), 
                Random.Range(areaVerticalConstraints.x, areaVerticalConstraints.y));
            newEnemyObject.transform.position = randomPosition;
        }
    }
    
    // void Update()
    // {
    //     // Press Space to spawn one pooled object for 1 second.
    //     if (spawn)
    //     {
    //         SpriteRenderer pooledSpriteRenderer = pool.Get();
    //         pooledSpriteRenderer.gameObject.transform.position = Random.insideUnitCircle * 5f;
    //
    //         StartCoroutine(ReturnAfter(pooledSpriteRenderer, 1f));
    //     }
    // }

    
    private GameObject CreateItem()
    {
        GameObject newGameObject = Instantiate(defaultEnemyPrefab, transform, true);
        newGameObject.transform.localScale = defaultEnemyScale;
        newGameObject.name = $"{"PooledCube" + newGameObject.gameObject.transform.GetSiblingIndex()}";
        newGameObject.SetActive(false);
        return newGameObject;
    }

    private void OnGet(GameObject pooledGameObject)
    {
        pooledGameObject.SetActive(true);
    }

    private void OnRelease(GameObject pooledGameObject)
    {
        pooledGameObject.SetActive(false);
    }

    private void OnDestroyItem(GameObject pooledGameObject)
    {
        Destroy(pooledGameObject);
    }
}
