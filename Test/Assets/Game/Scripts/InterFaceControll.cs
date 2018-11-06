using UnityEngine;
using System.Collections;

public class InterFaceControll : MonoBehaviour
{
    public GameObject[] Towers;
    public GameObject Tower;
    public Vector3 Point;
    public int Gold;


    public Texture2D Window;
    public GUIStyle style;


    private Material[] all;
    private GameObject TowerBuild = null;
    private DataBase DB;
    private bool Build;
    private GameObject CubeBuild;


    void Start()
    {
        DB = GameObject.FindGameObjectWithTag("GameController")
            .GetComponent<DataBase>();
    }

    void Update()
    {
        for (int i = 1; i < 6; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                Tower = Towers[i-1];
                Build = !Build;
            }
        }

        if (Input.GetMouseButtonDown(0))

        {
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit _hit;
       
            if (Physics.Raycast(_ray, out _hit, 10000))
         
            {
                if (_hit.transform.tag == "Enemy")
                 
                {
                    foreach (GameObject tower in DB.AllTower)
                     
                    {
                        if (Vector3.Distance(_hit.transform.position, tower.transform.position)
                            <= tower.GetComponent<TheTower>().Raduis)
                         
                        {
                            tower.GetComponent<TheTower>().Target = _hit.transform.gameObject;
                         
                        }
                    }
                }
            }
        }
   
        if (Build)
         
        {
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hit;
            if (Physics.Raycast(_ray, out _hit, 10000))
            {
                if (_hit.transform.tag == "BuildCube")
                {
                    if (!_hit.transform.GetComponent<CubeBuild>().Busy)
                    {
                        Point = _hit.transform.position;
                        CubeBuild = _hit.transform.gameObject;
                        if (TowerBuild != null)
                        TowerBuild.GetComponentInChildren<RadiusCircle>()
                            .GetComponent<Renderer>().material.color = new Color(0, 1, 0, 0.4f);
                    }
                    else
                    {
                        if (TowerBuild != null)
                        TowerBuild.GetComponentInChildren<RadiusCircle>()
                            .GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0.4f);
                    }
                }
                if (TowerBuild == null)
                {
                    TowerBuild = Instantiate(Tower, Point,
                        Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                    TowerBuild.GetComponent<TheTower>().enabled = false;
                }
                TowerBuild.transform.position = Point;
                all = TowerBuild.GetComponent<Renderer>().materials;
                foreach (Material mat in all)
                {
                    mat.color = new Color(1, 1, 1, 0.4f);
                }
                TowerBuild.GetComponentInChildren<RadiusCircle>().enabled = true;
                if (Input.GetMouseButtonDown(0))
                {
                    if (_hit.transform.tag == "BuildCube")
                    {
                        if (CubeBuild.transform.GetComponent<CubeBuild>().Busy == false)
              
                        {
                            if (TowerBuild.GetComponent<TheTower>().Cost <= Gold)
                            {
                                Gold -= TowerBuild.GetComponent<TheTower>().Cost;
                                CubeBuild.transform.GetComponent<CubeBuild>().Busy = true;
                                GameObject tow;
                                tow = Instantiate(Tower, new Vector3(Point.x, Point.y, Point.z), 
                                    Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                                DB.AllTower.Add(tow);
                                Build = false;
                                all = tow.GetComponent<Renderer>().materials;
                                foreach (Material mat in all)
                                {
                                    mat.shader = Shader.Find("Diffuse");
                                }
                            }
                        }
                    }
                }
            }
        }
        else
        {
            if (TowerBuild != null)
            {
                Destroy(TowerBuild);
            }
        }
    }


    void OnGUI()
    {
        style.fontSize = 15;
        style.normal.textColor = Color.black;
        style.hover.textColor = Color.red;
        style.normal.background = Window;
        style.hover.background = Window;
        style.wordWrap = true;
        style.alignment = TextAnchor.MiddleCenter;


        int _windowh = 150;
        GUI.DrawTexture(new Rect(0, Screen.height - _windowh, Screen.width, _windowh), Window);
        for (int i = 0; i < 5; i++)
        {
            Rect RP = new Rect(40 + 200 + i * 130, Screen.height - 130, 120, 50);
            if (GUI.Button(RP,Towers[i].transform.name + " - " 
                + Towers[i].GetComponent<TheTower>().Cost, style))
            {
                Tower = Towers[i];
                Build = !Build;

            }
        }
        style.normal.textColor = Color.yellow;
        style.hover.textColor = Color.yellow;
        GUI.Label(new Rect(0 + 50, Screen.height - 120, 150, 20),
            "Золото: " + Gold, style);
        GUI.Label(new Rect(0 + 50, Screen.height - 80, 150, 20), 
            "Волна: " + DB.Wave, style);
        GUI.Label(new Rect(0 + 50, Screen.height - 40, 150, 20), 
            "До начала: " + (int)DB.TimeWave, style);
        style.fontSize = 30;
        style.normal.textColor = Color.red;
        GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height - 60, 400, 40),
            "Health: " + DB.HealthAll, style);


    }
}
