using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public GameObject bombPrefab;

    void Start()
    {
        base.Start(); 
    }

    RaycastHit2D hit;
    int layerMask = ~(1 << 8);
    void Update()
    {
        if (isMove)
        {
            if (finishPoint != transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, finishPoint, speed * Time.deltaTime);
            }
            else
                isMove = false;
        }
    }

    public void SetBomb()
    {
        if (!isMove)
            Instantiate(bombPrefab, transform.position, Quaternion.identity);
    }
}
