using UnityEngine;
using System.Collections;

public class RadiusCircle : MonoBehaviour {

    public GameObject Tower;
    
    private TheTower TT;
    private float coffx;
    private float coffy;
    private float coffz;

    void Start()
    {
        TT = Tower.GetComponent<TheTower>();
    }
	
	void Update () 
    {
     
        coffx = ( Tower.transform.position.x/30);
        coffz = ( Tower.transform.position.z/30);
        coffy = Tower.transform.position.y/30 ;
        transform.localScale = new Vector3(coffx,coffy,coffz);
    }
}
