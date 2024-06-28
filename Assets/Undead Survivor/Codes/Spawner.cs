using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public Transform[] spwanPoint;
    public SpawnData[] spawnData;

    int level;
    float timer;

    void Awake()
    {
        spwanPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        if (!GameManager.Instance.isLive)
            return;

        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.gameTime / 10f), spawnData.Length - 1);

        if(timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }

        void Spawn()
        {
            GameObject enemy = GameManager.Instance.pool.Get(0);
            enemy.transform.position = spwanPoint[Random.Range(1, spwanPoint.Length)].position; // 0은 자기(부모)라서 자식만 선택하도록 1부터
            enemy.GetComponent<Enemy>().Init(spawnData[level]);
        }
    }
}

[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;
}
