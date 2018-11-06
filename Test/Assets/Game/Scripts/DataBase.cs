using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DataBase : MonoBehaviour
{
    public List<GameObject> AllEnemy;
    public List<GameObject> AllTower;


    public GameObject Enemy;
    public GameObject EnemyHealthBar;
    public GameObject StartSpawn;
    public float TimeForStart;
    public float TimeEveryRound;
    public int NumberWave;
    public int Wave;
    public int HealthAll;
    public float TimeWave;


    private int HEALTH;
    private int SPEED;
    private int EXP;
    private int GOLD;


    public int UnitNumber;
    private float TimeSpawn = 1f;
    private float _time;
    private bool WaveCom = false;
    private int UNW;
    private bool Equilazer;

    void Start()
    {
        Screen.SetResolution(1024, 768, false);

        if (Wave == 0)
        {
            TimeWave = TimeForStart;
        }
    }

    void Update()
    {

        if (!Equilazer)
        {
            for (int i = 0; i <= Enemy.GetComponent<MonsterMove>().Speed; i += 20)
            {
                if (TimeSpawn >= 0.1f)
                {
                    TimeSpawn -= 0.1f;
                    Equilazer = true;
                }
            }
        }

        if(TimeWave >= 0)
        {
        TimeWave -= Time.deltaTime;
        }
        if (TimeWave <= 0)
        {
            if (!WaveCom)
            {
                Wave += 1;



                int r1 = Random.Range(0, 6);
                if (r1 == 0 || r1 == 1)
                {
                    UnitNumber = 18;
                    SPEED = Random.Range(20, 35);
                    HEALTH = Random.Range(50 + 170 * Wave, 150 + 230 * Wave);
                    GOLD = Random.Range(5 + 1 * Wave, 8 + 2 * Wave);
                    EXP = Random.Range(10 + 1 * Wave, 10 + 5 * Wave);
                }
                if (r1 == 2 || r1 == 3)
                {
                    UnitNumber = 30;
                    SPEED = Random.Range(30, 50);
                    HEALTH = Random.Range(50 + 100 * Wave, 80 + 120 * Wave);
                    GOLD = Random.Range(2 + 1 * Wave, 5 + 1 * Wave);
                    EXP = Random.Range(10 + 1 * Wave, 10 + 2 * Wave);
                }
                if (r1 == 4 || r1 == 5)
                {
                    UnitNumber = 5;
                    SPEED = Random.Range(10, 20);
                    HEALTH = Random.Range(100 + 600 * Wave, 150 + 800 * Wave);
                    GOLD = Random.Range(5 + 4 * Wave, 10 + 8 * Wave);
                    EXP = Random.Range(10 + 20 * Wave, 10 + 40 * Wave);
                }
        

                UNW = 0;
                WaveCom = true;
      
            }
        }
        if (WaveCom)
        {
            _time -= Time.deltaTime;
            if (_time <= 0)
            {
                SpawnUnit(+1);
                _time = TimeSpawn;
            }
        }
        if (AllEnemy.Count == 0)
        {
            if (UNW >= UnitNumber)
            {
                if (WaveCom)
                {
                    UNW = 0;
                    TimeWave = TimeEveryRound;
                    WaveCom = false;
                }
            }
        }
    }

    public void SpawnUnit(int UnitNumberNow)
    {
        UNW += UnitNumberNow;
        if (UNW <= UnitNumber)
        {
            GameObject _enemy;
            GameObject _enemyHealthBar;
            _enemy = Instantiate(Enemy, StartSpawn.transform.position, 
                transform.rotation) as GameObject;
            _enemyHealthBar = Instantiate(EnemyHealthBar, 
                StartSpawn.transform.position, transform.rotation) as GameObject;

            MonsterMove E = _enemy.GetComponent<MonsterMove>();
            HealthBar EH = _enemyHealthBar.GetComponent<HealthBar>();

            E.Health = HEALTH;
            E.Gold = GOLD;
            E.Exp = EXP;
            E.Speed = SPEED;
            E.HealthBar = _enemyHealthBar;
            EH.HisMaster = _enemy;
            EH.HealthMax = E.Health;

            AllEnemy.Add(_enemy);
        }
    }
}
