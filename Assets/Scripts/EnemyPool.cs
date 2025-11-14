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
    
    private Sprite defaultSprite;
    private IObjectPool<SpriteRenderer> pool;

    private void Awake()
    {
        defaultSprite = defaultEnemyPrefab.GetComponent<SpriteRenderer>().sprite;
        pool = new ObjectPool<SpriteRenderer>(
            createFunc: CreateItem,
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: OnDestroyItem,
            collectionCheck: true,
            defaultCapacity: numberOfEnemiesToSpawn,
            maxSize: 5000);

        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            SpriteRenderer spriteRenderer = pool.Get();
            Vector2 randomPosition = new Vector2(
                Random.Range(areaHorizontalConstraints.x, areaHorizontalConstraints.y), 
                Random.Range(areaVerticalConstraints.x, areaVerticalConstraints.y));
            spriteRenderer.transform.position = randomPosition;
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

    
    private SpriteRenderer CreateItem()
    {
        GameObject newGameObject = new GameObject();
        newGameObject.transform.SetParent(transform);
        newGameObject.transform.localScale = defaultEnemyScale;
        newGameObject.name = $"{"PooledCube" + newGameObject.gameObject.transform.GetSiblingIndex()}";
        var spawnedSpriteRenderer = newGameObject.AddComponent<SpriteRenderer>();
        spawnedSpriteRenderer.sprite = defaultSprite;
        newGameObject.SetActive(false);
        return spawnedSpriteRenderer;
    }

    private void OnGet(SpriteRenderer pooledSpriteRenderer)
    {
        pooledSpriteRenderer.gameObject.SetActive(true);
    }

    private void OnRelease(SpriteRenderer pooledSpriteRenderer)
    {
        pooledSpriteRenderer.gameObject.SetActive(false);
    }

    private void OnDestroyItem(SpriteRenderer pooledSpriteRenderer)
    {
        Destroy(pooledSpriteRenderer.gameObject);
    }
}
