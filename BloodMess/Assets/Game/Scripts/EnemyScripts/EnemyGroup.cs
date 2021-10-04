using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemyObjects;
    private Enemy[] enemies;
    [SerializeField]
    private AnimationCurve chanceCurve;

    public Enemy this[int index] => enemies[index];

    public int Count => enemies.Length;

    public float Strong
    {
        get
        {
            float sum = 0;
            for (int i = 0; i < enemyObjects.Length; i++)
            {
                sum += enemyObjects[i].GetComponent<Enemy>().Strong;
            }
            return sum;
        }
    }

    private void Awake()
    {
        enemies = new Enemy[enemyObjects.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i] = enemyObjects[i].GetComponent<Enemy>();
        }
    }

    public float GetChance(float StrongKoef)
    {
        return chanceCurve.Evaluate(StrongKoef) / Strong;
    }
}
