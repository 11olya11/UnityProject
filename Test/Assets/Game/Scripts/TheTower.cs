using UnityEngine;
using System.Collections;

public class TheTower : MonoBehaviour
{
    public int MinDamage;
    public int MaxDamage;
    public float CritChance;
    public float CritMultiplier;
    public int Raduis;
    public int SpeedBullet;
    public float CooldownStatic;
    public int Cost;
    public int DamageLvlUp;
    public int ExpNow;
    public int Level;
    public GameObject Target;
    public GameObject AttackEffect;
    public RadiusCircle RC;
    public bool VisionRadius;
    public float Multiplier = 1;

    public enum Attack { Физическая, Магическая, Чистая, Тёмная, Стихийная };
    public Attack AttackType;

    private float Cooldown;
    private float Damage;
    private int ExpMax = 100;


    void Update()
    {
        Cooldown -= Time.deltaTime;
      if (ExpNow >= ExpMax)
        {
            if (Level <= 24)
            {
                LevelUp();
            }
        }
        if (VisionRadius)
        {
            RC.enabled = true;
        }
        else
        {
            RC.enabled = false;
        }
        if (Target == null)
        {
            GameObject[] enemys;
            enemys = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = enemys.Length; i > 0; i--)
            {
                int e = i-1;
                if (Vector3.Distance(enemys[e].transform.position, transform.position) <= Raduis)
                {
                    Target = enemys[e];
                }
            }
        }
        else
        {
            if (Cooldown <= 0)
            {
                float DMin = (MinDamage + DamageLvlUp * Level) * Multiplier;
                float DMax = (MaxDamage + DamageLvlUp * Level) * Multiplier;
                Damage = Random.Range(DMin, DMax);
                int _chance;
                _chance = Random.Range(1, 100);
                if (_chance <= CritChance)
                {
                    Damage *= CritMultiplier;
                }
                Cooldown = CooldownStatic;
                GameObject _attackClone;
                Vector3 pos = new Vector3(transform.position.x, 
                    transform.position.y + transform.localScale.y * 2, transform.position.z);
                _attackClone = Instantiate(AttackEffect, pos, transform.rotation) as GameObject;
                AttackBullet _ac = _attackClone.GetComponent<AttackBullet>();
                _ac.Speed = SpeedBullet;
                _ac.Damage = (int)Damage;
                _ac.Target = Target;
                _ac.Creator = gameObject;
            }
            if (Vector3.Distance(transform.position, Target.transform.position) > Raduis)
            {
                Target = null;
            }
        }
    }

    public void LevelUp()
    {
        ExpNow -= ExpMax;
        ExpMax += 60;
        Level += 1;
    }
}