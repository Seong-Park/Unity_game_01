using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // Prefab���� ������ ����  []�� �迭�� ����.
    public GameObject[] prefabs;
    // Pool�� ����� ����Ʈ []�� �迭�� ����.
    List<GameObject>[] pools;
    // ����Ʈ 1 : prefab 1

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }

    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // ������ Ǯ�� ��� (��Ȱ��ȭ ��) �ִ� ���� ������Ʈ ����

        // �߰��ϸ� select ������ �Ҵ�

        foreach (GameObject item in pools[index]) 
        { 
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }


        // �� ã������? (�� Ȱ��ȭ �Ǿ� �ִٸ�?)

        // ���Ӱ� �����ϰ� select ������ �Ҵ�

        if (!select) 
        {
            select = Instantiate(prefabs[index], transform); // ���⼭ transform�� PoolManager ���� �ȿ� prefab�� �����ϰڴ�.
            pools[index].Add(select);
        }

        return select;
    }
}
