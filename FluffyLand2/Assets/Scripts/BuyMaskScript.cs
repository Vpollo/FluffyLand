using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMaskScript : MonoBehaviour
{
    [SerializeField] private GameObject agentContainer;

    GameObject gameManager;
    GameManager gameScript;

    GameObject bank;
    MoneySystem moneySystem;

    int agentCount;
    //List<GameObject> agents;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameScript = gameManager.GetComponent<GameManager>();

        bank = GameObject.Find("Bank");
        moneySystem = bank.GetComponent<MoneySystem>();

        agentCount = gameScript.agentNum;
        //agents = GetAllChilds(agentContainer);
    }

    void Update()
    {
        
    }

    public void SendMask()
    {
        if (!moneySystem.subMoney(300))
        {
            Debug.Log("Not enough money!");
        } else
        {
            List<GameObject> agents = GetAllChilds(agentContainer);
            agents[Random.Range(0, agentCount)].GetComponent<AgentPandemic>().PutOnMask();
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
}
