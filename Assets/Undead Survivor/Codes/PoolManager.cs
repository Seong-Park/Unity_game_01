using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // Prefab들을 보관할 변수  []은 배열을 뜻함.
    public GameObject[] prefabs;
    // Pool을 담당할 리스트 []은 배열을 뜻함.
    List<GameObject>[] pools;
    // 리스트 1 : prefab 1

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

        // 선택한 풀의 놀고 (비활성화 된) 있는 게임 오브젝트 접근

        // 발견하면 select 변수에 할당

        foreach (GameObject item in pools[index]) 
        { 
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }


        // 못 찾았으면? (다 활성화 되어 있다면?)

        // 새롭게 생성하고 select 변수에 할당

        if (!select) 
        {
            select = Instantiate(prefabs[index], transform); // 여기서 transform은 PoolManager 폴더 안에 prefab을 생성하겠다.
            pools[index].Add(select);
        }

        return select;
    }
}
