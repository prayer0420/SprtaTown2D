using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;

    }

    public List<Pool> pools = new List<Pool>();
    public Dictionary<string, Queue<GameObject>> PoolDictionary;


    private void Awake()
    {
        //PoolDictionary 초기화
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();

        //최초 풀 만들기
        foreach (var pool in pools)
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                //만들고 비활성화, 큐에 넣음
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }

            PoolDictionary.Add(pool.tag, queue);
        }

    }

    //Instantiate대신 생성해주는 것
    public GameObject SpawnFromPool(string tag)
    {
        //존재하지 않으면
        if (!PoolDictionary.ContainsKey(tag))
        {
            return null;
        }

        //꺼내고 다시 집어넣음(순환하도록)
        GameObject obj = PoolDictionary[tag].Dequeue();
        PoolDictionary[tag].Enqueue(obj);


        obj.SetActive(true);
        return obj;
    }

}
