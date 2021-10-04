using System.Collections.Generic;
using UnityEngine;
using System;

public class Puller : Manager<Puller>
{
    private class PullQueue
    {
        private int count;
        private GameObject prefab;
        private Transform parent;
        private Queue<GameObject> queue;
        private readonly float EXPAND_KOEF = 1.3f;

        public PullQueue(int count, GameObject prefab, Transform parent)
        {
            this.count = count;
            this.prefab = prefab;
            this.parent = parent;
            queue = new Queue<GameObject>();
            for (int i = 0; i < count; i++)
            {
                this.AddEnemy();
            }
        }

        private void AddEnemy()
        {
            GameObject obj = Instantiate(prefab, parent);
            obj.SetActive(false);
            queue.Enqueue(obj);
        }

        public GameObject GetNextObj(Vector3 position, Quaternion rotation, Transform parent)
        {
            GameObject obj;
            bool found = false;
            for (int i = 0; i < count; i++)
            {
                obj = queue.Peek();
                if (!obj.activeSelf)
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                Expand();
            }
            return GetObj(position, rotation, parent);
        }

        private GameObject GetObj(Vector3 position, Quaternion rotation, Transform parent)
        {
            GameObject obj = queue.Dequeue();
            queue.Enqueue(obj);
            obj.SetActive(true);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.transform.parent = parent;
            return obj;
        }

        private void Expand()
        {
            int lastCount = count;
            count = (int)Math.Round(count * EXPAND_KOEF);
            for (int i = 0; i < count - lastCount; i++)
            {
                this.AddEnemy();
            }
        }
    }

    [Serializable]
    private struct ElementQueue
    {
        public int count;
        public GameObject prefab;
    }

    private Dictionary<GameObject, PullQueue> pull;
    [SerializeField]
    private List<ElementQueue> queueElements;

    private void Awake()
    {
        pull = new Dictionary<GameObject, PullQueue>();
        for (int i = 0; i < queueElements.Count; i++)
        {
            pull[queueElements[i].prefab] = new PullQueue(queueElements[i].count, queueElements[i].prefab, transform);
        }
    }

    public GameObject GetFromPull(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
    {
        return pull[prefab].GetNextObj(position, rotation, parent);
    }

    public GameObject GetFromPull(GameObject prefab)
    {
        return GetFromPull(prefab, Vector3.zero, Quaternion.identity, null);
    }

    public GameObject GetFromPull(GameObject prefab, Vector3 position)
    {
        return GetFromPull(prefab, position, Quaternion.identity, null);
    }

    public GameObject GetFromPull(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return GetFromPull(prefab, position, rotation, null);
    }

    public GameObject GetFromPull(GameObject prefab, Transform parent)
    {
        return GetFromPull(prefab, Vector3.zero, Quaternion.identity, parent);
    }

}
