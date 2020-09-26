using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthStats : MonoBehaviour
{
    private Slider slider;
    int health;
    int max_health;

    GameObject gameManager;
    GameManager gameScript;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameScript = gameManager.GetComponent<GameManager>();

        health = gameScript.agentNum;
        max_health = health;

        slider = gameObject.GetComponent<Slider>();
        slider.maxValue = health;
        slider.value = slider.maxValue;
    }

    void Update()
    {
            
    }

    public void addInfect()
    {
        if (health >= 0)
        {
            health -= 1;
            slider.value = health;
        }
    }

    public void addRecover()
    {
        if (health <= max_health)
        {
            health += 1;
            slider.value = health;
            Debug.Log("!!!!!!!!!!!!!!!!!!!");
        }

    }

    public void test()
    {
        Debug.Log("ss");
    }
}
