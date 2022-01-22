using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float bombWait;

    void Update()
    {
        bombWait -= Time.deltaTime;
        if(bombWait <= 0)
        {
            Boom();
        }
    }

    int layerMask = (1 << 8);
    void Boom()
    {
        Collider2D[] allCollider = Physics2D.OverlapCircleAll(transform.position, 2f, layerMask);
        foreach (Collider2D col in allCollider)
        {
            col.GetComponent<Entity>().isLive = false;
            col.gameObject.SetActive(false);
        }
        GameManager.gm.CheckWin();
        Destroy(this.gameObject);
    }
}
