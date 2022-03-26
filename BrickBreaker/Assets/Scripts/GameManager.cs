using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject worldPrefab;
    public GameObject cellPrefab;
    public GameObject playerPrefab;
    public GameObject ballPrefab;
    public GameObject[] blocks;
    private List<GameObject> worldBlocks = new List<GameObject>();
    private GameObject currentWorld;
    private MyGrid grid;
    private Ball ball;
    private Player player;
    private float currentUpdateTimer = 0.0f;
    private float updateTimer = 0.05f;
    private bool isHitPresent = false;
    private DataManager dataManager = new DataManager();
    private Debugger debugger;
    // Start is called before the first frame update
    void Start()
    {
        grid = new MyGrid(20, 33);

        debugger = GameObject.Find("Debugger").GetComponent<Debugger>();

        DisplayGrid();

        Cell playerCell = grid.CellAt(1, 16);
        Vector2 playerPosition = new Vector2(playerCell.column, playerCell.row);
        GameObject playerInstance = Instantiate(playerPrefab, playerPosition, Quaternion.identity);

        Cell ballCell = grid.CellAt(10, 16);
        Vector2 ballPosition = new Vector2(ballCell.column, ballCell.row);
        GameObject ballInstance = Instantiate(ballPrefab, ballPosition, Quaternion.identity);

        ball = ballInstance.GetComponent<Ball>();
        player = playerInstance.GetComponent<Player>();

        ball.Initialize();

        player.SetCurrentState(grid, ball);
        player.LoadData();

        ball.blockHitObservers += BlockHit;
        player.playerHitObservers += PlayerHit;
    }

    private void DisplayGrid()
    {
        currentWorld = Instantiate(worldPrefab, Vector3.zero, Quaternion.identity);
        for(int i = 0; i < grid.rows; i++)
        {
            for(int j = 0; j < grid.columns; j++)
            {
                Cell cell = grid.CellAt(i, j);
                Vector2 cellPosition = new Vector2(cell.column, cell.row);
                GameObject cellInstance = Instantiate(cellPrefab, cellPosition, Quaternion.identity);
                cellInstance.transform.parent = currentWorld.transform;
            }
        }

        for(int i = 12; i < grid.rows; i++)
        {
            for(int j = 0; j < grid.columns; j++)
            {
                Cell cell = grid.CellAt(i, j);
                Vector2 cellPosition = new Vector2(cell.column, cell.row);
                int blockIndex = Random.Range(0, blocks.Length);
                GameObject blockInstance = Instantiate(blocks[blockIndex], cellPosition, Quaternion.identity);
                blockInstance.transform.parent = currentWorld.transform;
                // worldBlocks.Add(blockInstance);
            }
        }
    }

    void BlockHit()
    {
        dataManager.IncreaseBrokenBlocks();
    }

    void PlayerHit()
    {
        dataManager.IncreaseHitCount();
        isHitPresent = true;
        print("Player hit");
        player.SetNextState(grid, ball);
        player.QUpdate(1.0f, false);
        // player.UpdateModel(1.0f);
        isHitPresent = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentUpdateTimer += Time.deltaTime;
        if(currentUpdateTimer > updateTimer)
        {
            Cell ballCell = grid.WorldPointToCell(ball.transform.position);
            Cell playerCell = grid.WorldPointToCell(player.transform.position);
            player.SelectAction();
            float currentQ = player.GetQ();

            dataManager.UpdateTimer(currentUpdateTimer);
            dataManager.SetQValue(currentQ);
            dataManager.SetMinMaxQValues(currentQ);

            debugger.SetColors(currentQ, dataManager.GetMinQValue(), dataManager.GetMaxQValue());
            debugger.SpawnMove(player, ballCell);

            player.Move();

            if(!isHitPresent)
                player.SetNextState(grid, ball);

            // player.UpdateAllTau();

            if(ballCell.row <= playerCell.row && (ballCell.column < playerCell.column - 1 || ballCell.column > playerCell.column + 1))
            {
                float reward = Mathf.RoundToInt(-100.0f - Vector2.Distance(player.transform.position, ball.transform.position) * 10.0f);
                // print(Mathf.RoundToInt(-100.0f - Vector2.Distance(player.transform.position, ball.transform.position) * 10.0f));
                player.QUpdate(reward, true);
                // player.UpdateModel(reward);
                Reset();
            }
            else
            {
                player.QUpdate(0.0f, false);
                // player.UpdateModel(0.0f);
            }

            // for(int i = 0; i < 50; i++)
            // {
            //     player.RunSimulation();
            // }

            currentUpdateTimer = 0.0f;
        }
    }

    private void Reset()
    {
        dataManager.RegisterObservation();
        dataManager.Reset();
        player.SaveData();
        ball.Reset();
        player.Reset();
        debugger.Reset();
        Destroy(currentWorld);
        DisplayGrid();
    }
}
