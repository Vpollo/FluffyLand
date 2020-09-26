using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int agentNum = 50;
    [SerializeField] GameObject agentPrefab;
    [SerializeField] GameObject agentContainer;

    Bound mainscene = new Bound(-2.5f, 7.15f, -3.55f, 2.45f);

    public int hospital_capacity = 7;   //change
    public int hospital_patient = 0;    //change

    public float hosMaxRec = 3.0f;


    void Start()
    {
        initAgent();
        //List<GameObject> agents = GetAllChilds(agentContainer);
        //Debug.Log(agents[0]);
        //agents[0].GetComponent<AgentPandemic>().infect();
        StartCoroutine(InfectFirst());
    }

    void Update()
    {
        
    }

    void initAgent()
    {
        int luckyGuy = Random.Range(0, agentNum);
        for(int i = 0; i < agentNum; i++)
        {
            Vector2 spawnSpot = new Vector2(Random.Range(mainscene.minX, mainscene.maxX), Random.Range(mainscene.minY, mainscene.maxY));
            GameObject newAgent = Instantiate(agentPrefab, spawnSpot, Quaternion.identity);
            newAgent.transform.parent = agentContainer.transform;
        }
    }

    List<GameObject> GetAllChilds(GameObject Go)
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < Go.transform.childCount; i++)
        {
            list.Add(Go.transform.GetChild(i).gameObject);
        }
        return list;
    }

    IEnumerator InfectFirst()
    {
        yield return new WaitForSeconds(3.0f);
        List<GameObject> agents = GetAllChilds(agentContainer);
        agents[0].GetComponent<AgentPandemic>().infect();
        Debug.Log("!!!!!!!!!!!!!!");
    }
}
