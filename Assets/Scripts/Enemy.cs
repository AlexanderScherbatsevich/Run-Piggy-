using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public static bool isStuned = false;
    public static bool isAngry = false;

    public float timeStune = 3f;

    public Vector2 direction;
    Vector2[] dir;
    public Sprite[] spritesCalm;
    public Sprite[] spritesAngry;
    public Sprite spritesStuned;
    public SpriteRenderer spriteRend;

    [SerializeField] Transform target;
    private NavMeshAgent agent;

    private float timePassed;
    private Vector3 newPos;      //две точки дл€ интерпол€ции
    private float timeStart;     //врем€ создани€ этого корабл€
    private float duration = 5;  //продолжительность перемещени€
    private void Start()
    {
        timePassed = 0;
        dir = GameManager.S.freeSpot.ToArray();
        spriteRend = GetComponent<SpriteRenderer>();

        target = GameObject.FindGameObjectWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        newPos = pos;
        InitMovement();
    }
    void Update()
    {
        //agent.SetDestination(target.position);
        ChangeSprite();
        Move();

        if (isStuned)
        {
            float tSpeed = agent.speed;
            agent.speed = 0;
            Sprite tSprite = spriteRend.sprite;
            spriteRend.sprite = spritesStuned;
            timePassed += Time.fixedDeltaTime;
            float time = timePassed / timeStune;
            if (time >= 1)
            {
                isStuned = false;
                spriteRend.sprite = tSprite;
                agent.speed = tSpeed;

            }
        }
    }

    public void ChangeSprite()
    {
        Vector2 pos = this.transform.position;
        //float xValue = target.position.x - pos.x;
        //float yValue = target.position.y - pos.y;

        if (isAngry)
        {
            float xValue = target.position.x - pos.x;
            float yValue = target.position.y - pos.y;
            if (yValue > 0 && yValue > xValue) spriteRend.sprite = spritesAngry[2];
            else if (yValue < 0 && yValue < xValue) spriteRend.sprite = spritesAngry[3];
            else if (xValue > 0 && yValue < xValue) spriteRend.sprite = spritesAngry[0];
            else if (xValue < 0 && yValue > xValue) spriteRend.sprite = spritesAngry[1];
        }
        else
        {
            float xValue = newPos.x - pos.x;
            float yValue = newPos.y - pos.y;
            if (yValue > 0 && yValue > xValue) spriteRend.sprite = spritesCalm[2];
            else if (yValue < 0 && yValue < xValue) spriteRend.sprite = spritesCalm[3];
            else if (xValue > 0 && yValue < xValue) spriteRend.sprite = spritesCalm[0];
            else if (xValue < 0 && yValue > xValue) spriteRend.sprite = spritesCalm[1];
        }
    }

    public Vector3 pos
    {
        get { return (this.transform.position); }
        set { this.transform.position = value; }
    }
    public void InitMovement()
    {
        // выбрать новую точку р1 на экране
        //if (isAngry)
        //{
        //    newPos = target.position;
        //}
        //else
        //{
        //    newPos = dir[Random.Range(0,dir.Length)];
        //}
        newPos = dir[Random.Range(0, dir.Length)];
        //сбросить врем€
        timeStart = Time.time;
    }

    public void Move()
    {
        float u = (Time.time - timeStart) / duration;
        if (u >= 1)
        {
            InitMovement();
            u = 0;
        }
        if (isAngry) agent.SetDestination(target.position);
        else agent.SetDestination(newPos);
    }
    private void OnTriggerEnter(Collider other)
    {
        //Transform goTr = other.gameObject.transform;
        // GameObject go = goTr.gameObject;
        GameObject go = other.gameObject;
        if (go.tag == "Explosion" || go.tag == "Bomb")
        {
            isStuned = true;
            Debug.Log("enemy say" + other.gameObject.name);
        }
    }
}
