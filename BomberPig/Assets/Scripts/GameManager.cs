using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    enum SwipeDirection
    {
        none,
        up,
        down, 
        left, 
        right
    };
    private Vector2 tapPosition;
    private float deadZone = 100f;
    private SwipeDirection currentSwipe = SwipeDirection.none;
    public Enemy enemyPrefab;
    public int maxCountEnemy;
    public List<GameObject> enemySpawn = new List<GameObject>();
    public  List<Enemy> allEnemys = new List<Enemy>();
    
    [Space]
    public Player player;

    void Start()
    {
        gm = this;
        StartGame();
    }

    void Update()
    {
        if (player.isLive)
        {
            if (tapPosition != Vector2.zero)
            {
                if (currentSwipe == SwipeDirection.none)
                {
                    PointerCheck();
                }
            }
            if ((currentSwipe != SwipeDirection.none) && (!player.isMove))
            {
                switch (currentSwipe)
                {
                    case SwipeDirection.left:
                        player.SetDirection(Vector3.left, 2.25f);
                        break;
                    case SwipeDirection.right:
                        player.SetDirection(Vector3.right, 2.25f);
                        break;
                    case SwipeDirection.up:
                        player.SetDirection(Vector3.up, 2f);
                        break;
                    case SwipeDirection.down:
                        player.SetDirection(Vector3.down, 2f);
                        break;
                }
            }
        }
    }

    public void PointerDown()
    {
        tapPosition = Input.mousePosition;
    }
    public void PointerCheck()
    {
        if(Mathf.Abs(tapPosition.x - Input.mousePosition.x) > deadZone)
        {
            if(tapPosition.x < Input.mousePosition.x)
                currentSwipe = SwipeDirection.right;
            else
                currentSwipe = SwipeDirection.left;
        }
        else if (Mathf.Abs(tapPosition.y - Input.mousePosition.y) > deadZone)
        {
            if (tapPosition.y < Input.mousePosition.y)
                currentSwipe = SwipeDirection.up;
            else
                currentSwipe = SwipeDirection.down;
        }
    }
    public void PointerUp()
    {
        currentSwipe = SwipeDirection.none;
        tapPosition = Vector3.zero;
    }
    public void ClickBomb()
    {
        player.SetBomb();
    }

    void StartGame()
    {
        for(int i=0; i<maxCountEnemy; i++)
        {
            allEnemys.Add(Instantiate(enemyPrefab, enemySpawn[Random.Range(0, enemySpawn.Count)].transform.position, Quaternion.identity).GetComponent<Enemy>());
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void CheckWin()
    {
        Debug.Log("win");
        bool flag = true;
        if (player.isLive)
        {
            foreach (Enemy es in allEnemys)
                if (es.isLive)
                    flag = false;
        }
        if (flag)
            Invoke("RestartGame", 1f);
    }    
}
