using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public int health;
    public float speed;
    public bool isLive = true;

    [Space]
    public Sprite leftSprite;
    public Sprite rightSprite;
    public Sprite upSprite;
    public Sprite downSprite;
    public SpriteRenderer spriteRenderer;
    public Animator entityAnimator;

    [HideInInspector]
    public Vector3 finishPoint;

    [HideInInspector]
    public bool isMove = false;
    // Start is called before the first frame update
    public void Start()
    {
        isMove = false;
        entityAnimator.speed = speed / 10f;
        spriteRenderer.sprite = leftSprite;
        finishPoint = transform.position;
    }

    public void SetDirection(Vector3 direction, float value)
    {
        if(direction == Vector3.up)
        {
            if(!CheckWall(Vector3.up))
            {
                spriteRenderer.sprite = upSprite;
                finishPoint = new Vector3(transform.position.x, transform.position.y + value, transform.position.z);
                isMove = true;
                entityAnimator.Play(0);
            }
        }
        if (direction == Vector3.right)
        {
            if (!CheckWall(Vector3.right))
            {
                spriteRenderer.sprite = rightSprite;
                finishPoint = new Vector3(transform.position.x + value, transform.position.y, transform.position.z);
                isMove = true;
                entityAnimator.Play(0);
            }
        }
        if (direction == Vector3.down)
        {
            if (!CheckWall(Vector3.down))
            {
                spriteRenderer.sprite = downSprite;
                finishPoint = new Vector3(transform.position.x, transform.position.y - value, transform.position.z);
                isMove = true;
                entityAnimator.Play(0);
            }
        }
        if (direction == Vector3.left)
        {
            if (!CheckWall(Vector3.left))
            {
                spriteRenderer.sprite = leftSprite;
                finishPoint = new Vector3(transform.position.x - value, transform.position.y, transform.position.z);
                isMove = true;
                entityAnimator.Play(0);
            }
        }


    }

    RaycastHit2D hit;
    int layerMask = ~(1 << 8);
    public bool CheckWall(Vector3 dirVector)
    {
        if (Physics2D.Raycast(transform.position, transform.TransformDirection(dirVector), 1.5f, layerMask))
        {
            hit = Physics2D.Raycast(transform.position, transform.TransformDirection(dirVector), 1.5f, layerMask);
            if (hit.collider.tag != "Wall")
                return false;
            else
                return true;
        }
        else
            return false;
    }

}
