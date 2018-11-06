using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

    public float Health;
    public float HealthMax;
    public GameObject HisMaster;

    void Update()
    {
        if (HisMaster != null)
        {
            float hp = Health / HealthMax;
            transform.localScale = new Vector3(8 * hp, 0.001f, 1f);
            transform.position = new Vector3(HisMaster.transform.position.x + 4 - hp * 4, 
                HisMaster.transform.position.y * 2 + 2, HisMaster.transform.position.z);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
