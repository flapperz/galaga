using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrid
{
    public float DivX = 0f;
    public float DivY = 0f;

    public float Padding = 1f;
    public float Expand = 1f;

    public int SizeX = 6;
    public int SizeY = 5;

    public float middlePosX = 0;
    public float middlePosY = 3;

    public float GetMiddleX()
    {
        return (SizeX + 1) / 2f;
    }

    public float GetMiddleY()
    {
        return (SizeY + 1) / 2f;
    }

    public Vector2 GetPosition(int coordX, int coordY)
    {
        float posX = ( (coordX - GetMiddleX()) * Padding * Expand ) + middlePosX + DivX;
        float posY = ( (coordY - GetMiddleX()) * Padding * Expand ) + middlePosY + DivY;

        return new Vector2(posX, posY);
    }
}

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager _instance;
    public static EnemyManager Instance { get { return _instance; } }

    [SerializeField]
    private GameObject blueEnemyPrefab;
    [SerializeField]
    private GameObject redEnemyPrefab;
    [SerializeField]
    private GameObject greenEnemyPrefab;

    private bool isBeatable = false;

    private EnemyGrid enemyGrid = new EnemyGrid();
    public Dictionary<GameObject, Vector2Int> Enemy2Coord = new Dictionary<GameObject, Vector2Int>();

    private Vector3 spawnPos = new Vector3(-7, -5, 0);
    private int expandCounter = 0;

    private float spawnDelay = 0.3f;
    private float attackRoutineDelay = 1f;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        StartCoroutine("AttackRoutine");
        StartCoroutine("MoveRoutine");

        StartCoroutine("SpawnLine1");
        StartCoroutine("SpawnLine2");
        StartCoroutine("SpawnLine3");
        StartCoroutine("SpawnLine4");
        StartCoroutine("SpawnLine5");

        StartCoroutine("SetBeatable");

    }

    private void Update()
    {
        if (Enemy2Coord.Count == 0 && isBeatable)
        {
            GameManager.GameOver();
        }
    }

    public Vector2 GetGridPosition(GameObject enemy)
    {
        int coordX = Enemy2Coord[enemy].x;
        int coordY = Enemy2Coord[enemy].y;
        return enemyGrid.GetPosition(coordX, coordY);
    }

    IEnumerator SpawnLine1()
    {
        int CoordY = 1;

        for (int i=0; i<enemyGrid.SizeX; i++)
        {
            GameObject blueEnemyObject = Instantiate(blueEnemyPrefab, spawnPos, Quaternion.Euler(0f, 0f, 180f));
            Enemy2Coord[blueEnemyObject] = new Vector2Int(i + 1, CoordY);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    IEnumerator SpawnLine2()
    {
        int CoordY = 2;

        yield return new WaitForSeconds(5);

        for (int i=0; i<enemyGrid.SizeX; i++)
        {
            GameObject blueEnemyObject = Instantiate(blueEnemyPrefab, spawnPos, Quaternion.Euler(0f, 0f, 180f));
            Enemy2Coord[blueEnemyObject] = new Vector2Int(i + 1, CoordY);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    IEnumerator SpawnLine3()
    {
        int CoordY = 3;

        yield return new WaitForSeconds(10);

        for (int i = 0; i < enemyGrid.SizeX; i++)
        {
            GameObject blueEnemyObject = Instantiate(redEnemyPrefab, spawnPos, Quaternion.Euler(0f, 0f, 180f));
            Enemy2Coord[blueEnemyObject] = new Vector2Int(i + 1, CoordY);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    IEnumerator SpawnLine4()
    {
        int CoordY = 4;

        yield return new WaitForSeconds(15);

        for (int i = 0; i < enemyGrid.SizeX; i++)
        {
            GameObject blueEnemyObject = Instantiate(redEnemyPrefab, spawnPos, Quaternion.Euler(0f, 0f, 180f));
            Enemy2Coord[blueEnemyObject] = new Vector2Int(i + 1, CoordY);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    IEnumerator SpawnLine5()
    {
        int CoordY = 5;

        yield return new WaitForSeconds(20);

        for (int i = 0; i < enemyGrid.SizeX; i++)
        {
            GameObject blueEnemyObject = Instantiate(greenEnemyPrefab, spawnPos, Quaternion.Euler(0f,0f,180f));
            Enemy2Coord[blueEnemyObject] = new Vector2Int(i + 1, CoordY);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    IEnumerator SetBeatable()
    {
        yield return new WaitForSeconds(22);
        isBeatable = true;
    }

    IEnumerator AttackRoutine()
    {
        for(;;)
        {
            List<GameObject> enemyList = new List<GameObject>(Enemy2Coord.Keys);

            if (enemyList.Count != 0)
            {
                var chooseEnemy = enemyList[Random.Range(0, enemyList.Count)];

                var enemyStateMachine = chooseEnemy.GetComponent<EnemyStateMachine>();

                if (enemyStateMachine.State == State.AtGrid)
                    enemyStateMachine.SetAttack();
            }


            yield return new WaitForSeconds(attackRoutineDelay);
        
        }
    }

    IEnumerator MoveRoutine()
    {
        for(;;)
        {
            expandCounter = (expandCounter + 1) % 10;
            enemyGrid.Expand = 1 + Mathf.Abs(expandCounter - 5) / 10f;
            yield return new WaitForSeconds(0.5f);
        }
    }

    void OnDestroy()
    {
        StopCoroutine("AttackRoutine");    
    }
}
