using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemyObjects;
    private EnemyGroup[] enemyTypes;
    [SerializeField]
    private Bounds bounds;
    [SerializeField]
    private float defaultFrequ;
    [SerializeField]
    private AnimationCurve strongCurve;
    [SerializeField]
    private float breakStrong;
    [SerializeField]
    private float startSpawnRadius;
    [SerializeField]
    private float endSpawnRadius;

    private EnemyHolder enemyHolder;
    private Player player;

    public float Period => defaultFrequ / StrongKoef;

    public float StrongKoef => strongCurve.Evaluate(enemyHolder.FullStrong / breakStrong);

    private void Awake()
    {
        player = LevelManager.Instance.Player;
        enemyHolder = gameObject.GetComponent<EnemyHolder>();
        enemyTypes = new EnemyGroup[enemyObjects.Length];
        for (int i = 0; i < enemyObjects.Length; i++)
        {
            enemyTypes[i] = enemyObjects[i].GetComponent<EnemyGroup>();
        }
    }

    private void Start()
    {
        StartSpawn();
    }

    private void StartSpawn()
    {
        StartCoroutine(SpawnProcess());
    }

    private IEnumerator SpawnProcess()
    {
        while (true)
        {
            enemyHolder.SpawnEnemy(RandomEnemy(), RandomPlace());
            yield return new WaitForSeconds(Period);
        }
    }

    private Vector2 RandomPlace()
    {
        Vector2 coordinates;
        do
        {
            coordinates = CoordCircleToWorld(Random.Range(0f, 360f), Random.Range(startSpawnRadius, endSpawnRadius));
        }
        while (!CheckInBounds(coordinates));
        return coordinates;
    }

    private GameObject RandomEnemy()
    {
        float[] sumsChances = new float[enemyTypes.Length + 1];
        float sum = 0;
        for (int i = 0; i < enemyTypes.Length; i++)
        {
            sum += enemyTypes[i].GetChance(StrongKoef);
            sumsChances[i + 1] = sum;
        }
        float randFloat = Random.Range(0, sum);
        int b = BinarySearch(randFloat, sumsChances);
        return enemyTypes[b - 1].gameObject;
    }

    private Vector2 CoordCircleToWorld(float angle, float place)
    {
        Vector2 direction = AngleToDirection(angle);
        return (Vector2)player.transform.position + direction * place;
    }

    private static int BinarySearch(float k, float[] arr)
    {
        int l = -1;
        int r = arr.Length;
        int mid;
        while (r - l > 1)
        {
            mid = (l + r) / 2;
            if (k > arr[mid])
            {
                l = mid;
            }
            else
            {
                r = mid;
            }
        }
        return r;
    }

    private static Vector2 AngleToDirection(float angle)
    {
        angle = angle % 360;
        angle = 360 - angle;
        Vector2 rotationVector = new Vector2(Mathf.Cos(angle * Mathf.PI / 180), Mathf.Sin(angle * Mathf.PI / 180));
        return RotateVector(Vector2.up, rotationVector);
    }

    private static Vector2 RotateVector(Vector2 vector, Vector2 angle)
    {
        return new Vector2(vector.x * angle.x - vector.y * angle.y, vector.x * angle.y + angle.x * vector.y);
    }

    private void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.DrawWireSphere(player.transform.position, startSpawnRadius);
            Gizmos.DrawWireSphere(player.transform.position, endSpawnRadius);
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }

    private bool CheckInBounds(Vector3 position)
    {
        return position.x <= bounds.max.x && position.x >= bounds.min.x && position.y <= bounds.max.y && position.y >= bounds.min.y;
    }
}
