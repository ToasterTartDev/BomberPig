using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    int change = 0;
    int nowDirection = 0;
    Vector3 vectorDirection = Vector3.left;

    void Start()
    {
        base.Start();
        nowDirection = Random.Range(0, 4);
        isMove = false;
        SetRandomDirection();
    }

    void Update()
    {
        if (isLive)
        {
            if (!isMove)
                MoveDirection();

            if (isMove)
            {
                if (finishPoint != transform.position)
                {
                    transform.position = Vector3.MoveTowards(transform.position, finishPoint, speed * Time.deltaTime);
                }
                else
                {
                    isMove = false;

                    if (Random.Range(0, 7) == change)
                        SetRandomDirection();
                    else
                    {
                        if (CheckWall(ConvertDirection(nowDirection)))
                            SetRandomDirection();
                    }
                }
            }
        }
    }

    void MoveDirection()
    {
        if(nowDirection == 0)
            SetDirection(Vector3.up, 2f);
        if (nowDirection == 1)
            SetDirection(Vector3.right, 2.25f);
        if (nowDirection == 2)
            SetDirection(Vector3.down, 2f);
        if (nowDirection == 3)
            SetDirection(Vector3.left, 2.25f);
    }

    Vector3 ConvertDirection(int dir)
    {
        if (dir == 0)
            return Vector3.up;
        if (dir == 1)
            return Vector3.right;
        if (dir == 2)
            return Vector3.down;
        if (dir == 3)
            return Vector3.left;
        return Vector3.zero;
    }

    void SetRandomDirection()
    {
        List<int> freeDirection = new List<int>();
        for (int i = 0; i <= 3; i++)
        {
            if (!CheckWall(ConvertDirection(i)))
                freeDirection.Add(i);
        }
        nowDirection = freeDirection[Random.Range(0, freeDirection.Count)];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Entity>().isLive = false;
            collision.gameObject.SetActive(false);
            GameManager.gm.CheckWin();
        }
    }
}
