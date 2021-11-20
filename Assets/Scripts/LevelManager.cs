using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;
    internal event System.EventHandler<int> OnRoundChanged;
    internal event System.EventHandler<int> OnTotalOfEnemiesChanged;
    internal event System.EventHandler<int> OnTotalOfEnemiesReachedEndLevelChanged;
    internal event System.EventHandler<int> OnRemainingEnemiesToCrossEndLevelChanged;
    internal event System.EventHandler<int> OnPlayerCoinsChanged;
    internal event System.EventHandler<int> OnEnemyKilledChanged;
    internal event System.EventHandler OnGameOver;
    [SerializeField] List<EnemySpawner> enemySpawners = new List<EnemySpawner>();
    [SerializeField] private Vector2 rangeTimeBetweenEnemies;
    [SerializeField] List<Vector2> waves = new List<Vector2>();
    [SerializeField] int maxEnemiesReachedEndLine = 2;
    [SerializeField] int initialCoins = 5;
    private bool isGameOver = false;
    int enemiesReachedEndLine;
    private int round = 0;


    private int totalOfEnemies = 0;
    private int totalOfEnemiesKilled = 0;
    private List<Enemy> enemies = new List<Enemy>();
    private List<Ally> allies = new List<Ally>();



    public int TotalOfEnemies
    {
        get { return totalOfEnemies; }
        set
        {
            totalOfEnemies = value;
            OnTotalOfEnemiesChanged?.Invoke(this, totalOfEnemies);
        }
    }


    private int coins;
    public int Coins
    {
        get { return coins; }
        set
        {
            coins = value;
            OnPlayerCoinsChanged?.Invoke(this, coins);
        }
    }


    void Awake()
    {
        instance = this;
        SceneManager.sceneUnloaded += handleSceneUnloaded;
    }

    private void handleSceneUnloaded(Scene scene)
    {
        if (scene.name == "GameOverScene")
        {
            GameUtil.continueGame();
            StartCoroutine(ResetGame());
        }
    }

    void Start()
    {
        StartCoroutine(ResetGame());
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }
    internal void RemoveEnemy(Enemy enemy, bool reachedTheEndLine = false)
    {
        enemies.Remove(enemy);
        Destroy(enemy.gameObject);

        TotalOfEnemies--;

        if (!reachedTheEndLine && !this.isGameOver)
        {
            Coins += enemy.CoinsAwards;
            OnEnemyKilledChanged?.Invoke(this, ++totalOfEnemiesKilled);
        }

        if (TotalOfEnemies != 0)
        {
            //Do nothing, there are enemies alive
            return;
        }

        if (round > waves.Count)
        {
            StartCoroutine(showGameOverScene());
            return;
        }

        StartLevel();
    }

    internal void RemoveAlly(Ally ally)
    {
        this.allies.Remove(ally);
        Destroy(ally.gameObject);
    }
    internal void AddAlly(Ally ally)
    {
        this.allies.Add(ally);
        if (!this.isGameOver)
        {
            Coins -= ally.Cost;
        }
    }

    internal void EnemyReachedEndLine(Enemy enemy)
    {
        RemoveEnemy(enemy, true);
        enemiesReachedEndLine++;
        OnTotalOfEnemiesReachedEndLevelChanged?.Invoke(this, enemiesReachedEndLine);
        OnRemainingEnemiesToCrossEndLevelChanged?.Invoke(this, maxEnemiesReachedEndLine - enemiesReachedEndLine);
        if (enemiesReachedEndLine >= maxEnemiesReachedEndLine)
        {
            StartCoroutine(showGameOverScene());
        }
    }


    private IEnumerator showGameOverScene()
    {
        if (this.isGameOver) yield break;

        this.isGameOver = true;
        OnGameOver?.Invoke(this, System.EventArgs.Empty);
        
        ClearLevel();

        GameOverSceneModel.coins = coins;
        GameOverSceneModel.enemiesKilled = totalOfEnemiesKilled;


        AsyncOperation load = SceneManager.LoadSceneAsync("GameOverScene", LoadSceneMode.Additive);
        yield return load;
    }

    private IEnumerator ResetGame()
    {
        yield return new WaitForEndOfFrame();

        ClearLevel();
        
        this.Coins = initialCoins;
        this.isGameOver = false;
        enemiesReachedEndLine = 0;
        OnRemainingEnemiesToCrossEndLevelChanged?.Invoke(this, maxEnemiesReachedEndLine);
        round = 0;
        TotalOfEnemies = 0;
        totalOfEnemiesKilled = 0;
        StartLevel();
    }

    private void ClearLevel()
    {
        while (enemies.Count > 0)
        {
            RemoveEnemy(enemies[0]);
        }

        while (allies.Count > 0)
        {
            RemoveAlly(allies[0]);
        }
    }
    private void StartLevel()
    {
        if (this.isGameOver) return;
        var currentWave = waves[round];
        int numberOfEnemies = getIntRandomValue(currentWave);
        int numberOfEnemiesByLane = numberOfEnemies / enemySpawners.Count;
        
        round++;
        OnRoundChanged?.Invoke(this, round);

        TotalOfEnemies = 0;
        enemySpawners.ForEach(enemySpawner =>
        {
            TotalOfEnemies += numberOfEnemiesByLane;
            StartCoroutine(SpawnEnemies(enemySpawner, numberOfEnemiesByLane));
        });
    }

    private IEnumerator SpawnEnemies(EnemySpawner enemySpawner, int numberOfEnemies)
    {
        float speedUp = ((float)round / waves.Count) + 1;
        
        do
        {
            var delay = Random.Range(rangeTimeBetweenEnemies.x, rangeTimeBetweenEnemies.y);
            yield return new WaitForSeconds(delay);


            enemySpawner.Spawn(speedUp);
            numberOfEnemies--;

        } while (numberOfEnemies > 0);
    }

    private int getIntRandomValue(Vector2 interval)
    {
        int min = Mathf.FloorToInt(interval.x);
        int max = Mathf.FloorToInt(interval.y);
        return Random.Range(min, max);
    }
}
