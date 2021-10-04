using System.Collections.Generic;
using UnityEngine;

public class EnemyHolder : MonoBehaviour
{
    private List<Enemy> enemiesOnField;
    private float m_fullStrong;

    public float FullStrong => m_fullStrong;
    public int CountEnemies => enemiesOnField.Count;

    private void Awake()
    {
        enemiesOnField = new List<Enemy>();
    }

    private void Update()
    {
        for (int i = 0; i < enemiesOnField.Count; i++)
        {
            if (!enemiesOnField[i].gameObject.activeSelf)
            {
                m_fullStrong -= enemiesOnField[i].Strong;
                enemiesOnField.RemoveAt(i);
            }
        }
    }

    public void AddEnemy(Enemy enemy)
    {
        enemiesOnField.Add(enemy);
        m_fullStrong += enemy.Strong;
    }

    public void AddEnemy(EnemyGroup enemy)
    {
        for (int i = 0; i < enemy.Count; i++)
        {
            this.AddEnemy(enemy[i]);
        }
    }

    public GameObject SpawnEnemy(GameObject enemy, Vector3 position)
    {
        GameObject obj = Puller.Instance.GetFromPull(enemy, position);
        this.AddEnemy(obj.GetComponent<EnemyGroup>());
        return obj;
    }
}
