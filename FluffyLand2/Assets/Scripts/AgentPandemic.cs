using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentPandemic : MonoBehaviour
{
    [SerializeField] bool infected = false;

    [SerializeField] float infectiveness = 0.12f;

    public GameObject healthBar;
    private healthStats stats;

    SpriteRenderer spriteR;
    private Animator anim;

    float minRecoverTime = 1.0f;
    float maxRecoverTime = 10.5f;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        healthBar = GameObject.Find("HealthBar");
        stats = healthBar.GetComponent<healthStats>();
        stats.test();
        spriteR = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {

    }

    public void infect()
    {
        anim.SetFloat("infection", 1.0f);
        stats.addInfect();

        infected = true;
        spriteR.color = Color.red;

        StartCoroutine(recoverSequence());
    }

    public void recover()
    {
        anim.SetFloat("infection", 0.0f);
        stats.addRecover();
        infected = false;
        spriteR.color = Color.white;
        infectiveness = infectiveness / 2;
    }

    public bool isInfected()
    {
        return infected;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            AgentPandemic otherPan = other.transform.GetComponent<AgentPandemic>();
            if (otherPan.isInfected())
            {
                if (Random.Range(0.0f, 1.0f) <= infectiveness)
                {
                    infect();
                }
            }
        }
    }

    IEnumerator recoverSequence()
    {
        float rec = Random.Range(minRecoverTime, maxRecoverTime);
        yield return new WaitForSeconds(rec);
        if (rec >= 10.0f)
        {
            // Agent die sript
        }
        recover();
    }

    public void PutOnMask()
    {
        anim.SetFloat("mask", 1.0f);
        infectiveness = 0.7f * infectiveness;
    }
}
