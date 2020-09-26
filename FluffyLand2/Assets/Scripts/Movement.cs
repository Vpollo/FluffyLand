using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Bound
{
    public float minX, maxX, minY, maxY;
    public Bound() { minX = maxX = minY = maxY = 0.0f; }
    public Bound(float a, float b, float c, float d)
    {
        minX = a;
        maxX = b;
        minY = c;
        maxY = d;
    }
}

public class Movement : MonoBehaviour
{
    [SerializeField] float step = 5.0f;

    [SerializeField] float speed = 5.0f;
    [SerializeField] float startWaitTime;
    float waitTime = 5.0f;

    //[SerializeField] Transform MoveSpot;
    Vector2 moveSpot;
    Vector2 oldpos;
    Bound mainscene = new Bound(-2.5f, 7.15f, -3.55f, 2.45f);
    Bound hospital = new Bound(-7.73f, -5.39f, 0.45f, 2.6f);
    public string status = "mainscene";
    bool legal = true;
    /*float minX = -8.37f;
    float maxX = 8.37f;
    float minY = -4.48f;
    float maxY = 4.48f;*/

    bool isAlive = true;

    bool isDragging;



    GameObject gameManager;
    GameManager gameScript;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameScript = gameManager.GetComponent<GameManager>();
        waitTime = startWaitTime;
        moveSpot = newTarget();
        status = "mainscene";
    }

    void Update()
    {
        // Code for mouse dragging and moving
        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }

        transform.position = Vector2.MoveTowards(transform.position, moveSpot, speed * Time.deltaTime);

        // if reach target wait for startWaitTime seconds before move on
        if (Vector2.Distance(transform.position, moveSpot) <= 0.2f)
        {
            if (waitTime <= 0)
            {
                moveSpot = newTarget();
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        if (Checkfaintbound(transform.position, mainscene)) DeHospitalize();
        if (Checkfaintbound(transform.position, hospital)) Hospitalize();
        if (!legal)
        {
            legal = true;
            transform.position = oldpos;
        }
    }

    Vector2 newTarget()
    {
        //if (Checkfaintbound(moveSpot, mainscene)) status = "mainscene";
        //if (Checkfaintbound(moveSpot, hospital)) status = "hospital";
        float deltaX = Random.Range(-step, step);
        float deltaY = Mathf.Sqrt(step * step - deltaX * deltaX) * Mathf.Pow(-1, Random.Range(0, 2));
        float nextX = transform.position.x + deltaX;
        float nextY = transform.position.y + deltaY;
        Vector2 spot = new Vector2(nextX, nextY);

        if (status == "mainscene") return Clampbound(spot, mainscene);
        if (status == "hospital") return Clampbound(spot, hospital);
        return spot;
    }
    public void Hospitalize()
    {
        if (status == "mainscene")
        {
            GameManager script = GameObject.Find("GameManager").GetComponent<GameManager>();
            if (script.hospital_patient == script.hospital_capacity)
            {
                legal = false;
                Debug.Log("too many people in hospital!");
                return;
            }
            script.hospital_patient++;
            //hospital_patient++;
            Debug.Log(new Vector2((float)script.hospital_patient, (float)script.hospital_capacity));
            status = "hospital";
        }
    }
    public void DeHospitalize()
    {
        if (status == "hospital")
        {
            GameManager script = GameObject.Find("GameManager").GetComponent<GameManager>();
            script.hospital_patient--;
            //hospital_patient++;
            Debug.Log(new Vector2((float)script.hospital_patient, (float)script.hospital_capacity));
            status = "mainscene";
        }
    }
    public void OnMouseDown()
    {
        isDragging = true;
        oldpos = transform.position;
    }

    public void OnMouseUp()
    {
        if (Checkfaintbound(transform.position, mainscene)) DeHospitalize();
        else if (Checkfaintbound(transform.position, hospital))
        {
            Hospitalize();
            StartCoroutine(HosReoverSeq());
        }
        else
        {
            Debug.Log("outofbound");
            isDragging = false;
            legal = false;
            return;
        }
        isDragging = false;
        moveSpot = newTarget();
        Debug.Log(status);
    }

    public bool Checkbound(Vector2 A, Bound B)
    {
        return B.minX <= A.x && A.x <= B.maxX && B.minY <= A.y && A.y <= B.maxY;
    }
    public bool Checkfaintbound(Vector2 A, Bound B)
    {
        return B.minX - 0.35 <= A.x && A.x <= B.maxX + 0.35 && B.minY - 0.35 <= A.y && A.y <= B.maxY + 0.35;
    }
    public Vector2 Clampbound(Vector2 A, Bound B)
    {
        return new Vector2(Mathf.Clamp(A.x, B.minX, B.maxX), Mathf.Clamp(A.y, B.minY, B.maxY));
    }

    IEnumerator HosReoverSeq()
    {
        float recT = gameScript.hosMaxRec;
        yield return new WaitForSeconds(recT);
        gameObject.GetComponent<AgentPandemic>().recover();
    }
}
