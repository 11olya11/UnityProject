using UnityEngine;
using System.Collections;

public class Turn : MonoBehaviour
{

    public enum turn { Право, Лево, Верх, Низ, Конец };
    public turn TurnOn;


    private DataBase DB;
    private Quaternion turnon;
    private GameObject KillGO;


    void Start()
    {
        DB = GameObject.FindGameObjectWithTag("GameController").GetComponent<DataBase>();
    }

    void Update()
    {
        switch (TurnOn)
        {
            case turn.Верх:
                turnon = new Quaternion(0,1,0,0);
                break;
            case turn.Низ:
                turnon = new Quaternion(0, 0, 0, 0);
                break;
            case turn.Лево:
                turnon = new Quaternion(0, -1, 0, -1);
                break;
            case turn.Право:
                turnon = new Quaternion(0, 1, 0, -1);
                break;
      
            case turn.Конец:
                foreach (GameObject enemy in DB.AllEnemy)

                {
                    Vector3 pos1 = new Vector3(transform.position.x, 0, transform.position.z);
                    Vector3 pos2 = new Vector3(enemy.transform.position.x, 0, enemy.transform.position.z);

                    if (Vector3.Distance(pos1, pos2) <= 1 * enemy.GetComponent<MonsterMove>().Speed * Time.deltaTime)
                    {
                        DB.HealthAll -= 1;
                        KillGO = enemy;
                    }
                }
                DB.AllEnemy.Remove(KillGO);
                Destroy(KillGO);
                break;
           
                
        }
        foreach (GameObject enemy in DB.AllEnemy)
        {
            Vector3 pos1 = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 pos2 = new Vector3(enemy.transform.position.x, 0, enemy.transform.position.z);
            if (Vector3.Distance(pos1, pos2) <= 1 * enemy.GetComponent<MonsterMove>().Speed * Time.deltaTime)
            {
                enemy.transform.localRotation = turnon;
                enemy.transform.position = new Vector3(transform.position.x, enemy.transform.position.y, transform.position.z);
            }
        }

        if (DB.HealthAll >= 0)
        {
            Application.Quit();
        }
    }
}
