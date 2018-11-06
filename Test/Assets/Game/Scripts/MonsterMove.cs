using UnityEngine;
using System.Collections;

public class MonsterMove : MonoBehaviour {

    public int Health;
    public int Speed;
    public int Protect;
    public int Gold;
    public int Exp;

    public Vector3 Direction;
    public GameObject Attacker;
    public GameObject HealthBar;

    public enum Armor { Физическая, Магическая, Чистая, Стихийная, Тёмная };
    public Armor ArmorType;

    private DataBase DB;
    private InterFaceControll IFC;

    void Start()
    {
        IFC = GameObject.FindGameObjectWithTag("MainCamera").
            GetComponent<InterFaceControll>();
        DB = GameObject.FindGameObjectWithTag("GameController").
            GetComponent<DataBase>();
    }
	
	void Update () 
    {
        transform.Translate(Direction * Speed * Time.deltaTime);
        HealthBar.GetComponent<HealthBar>().Health = Health;
	}

    public void Damage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Attacker.GetComponent<TheTower>().ExpNow += Exp;
            IFC.Gold += Gold;
            DB.AllEnemy.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
