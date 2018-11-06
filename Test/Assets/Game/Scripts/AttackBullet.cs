using UnityEngine;
using System.Collections;

public class AttackBullet : MonoBehaviour {

    public GameObject Target;
    public int Speed;
    public int Damage;
    public GameObject Creator;

    private Vector3 Point;


    void Update()
    {
        if (Target != null)
        {
            MonsterMove MM = Target.gameObject.GetComponent<MonsterMove>();
            transform.LookAt(Target.transform.position);
            transform.Translate(Vector3.forward * Speed *Time.deltaTime);
            Point = Target.transform.position;
            if (Vector3.Distance(transform.position, Target.transform.position) < 0.5 + Speed / 50)
            {
                Destroy(gameObject);
                MM.Attacker = Creator;
                MM.Damage(Damage);
            }
        }
        if (Target == null)
        {
            transform.LookAt(Point);
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, Point) < 0.5 + Speed / 50)
            {
                Destroy(gameObject);
            }
            if (Point == new Vector3(0, 0, 0))
            {
                Destroy(gameObject);
            }
        }
    }
}
