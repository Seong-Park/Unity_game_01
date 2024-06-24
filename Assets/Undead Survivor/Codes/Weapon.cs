using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    float timer;
    Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;

                if(timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;

        }

        //test
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(20, 5);
        }
    }



    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id ==0) 
        {
            Batch();
        }

    }


    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = 150;
                Batch();
                break;
            default:
                speed = 0.3f;
                break;

        }
    }

    void Batch()
    {
        for (int index =0 ; index < count; index++)
        {
            Transform bullet;
            
            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.Instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }
            
            
            bullet.localPosition = Vector3.zero; //위치 초기화
            bullet.localRotation = Quaternion.identity; // 위치 초기화

            Vector3 rocVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rocVec);
            bullet.Translate(bullet.up * 1.3f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // -1이면 관통이 무한


        }
    }

    void Fire()
    {
        if (!player.scanner.nearestTarget)
        {
            return;
        }

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.Instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir); // -1이면 관통이 무한
    }
}
