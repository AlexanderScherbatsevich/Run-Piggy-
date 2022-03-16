using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Vector2 direction;
    Vector2[] dir;
    public Sprite[] sprites;
    public SpriteRenderer spriteRend;

    [SerializeField] Transform target;
    private NavMeshAgent agent;

    private Vector3 p0, p1;      //две точки для интерполяции
    private float timeStart;     //время создания этого корабля
    private float duration = 5;  //продолжительность перемещения
    private void Start()
    {
        dir = GameManager.S.freeSpot.ToArray();
        spriteRend = GetComponent<SpriteRenderer>();

        target = GameObject.FindGameObjectWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        p0 = p1 = pos;
        InitMovement();
    }
    void Update()
    {
        //agent.SetDestination(target.position);
        ChangeSprite();
        Move();
    }

    public void ChangeSprite()
    {      
        Vector2 pos = this.transform.position;
        float xValue = target.position.x - pos.x;
        float yValue = target.position.y - pos.y;

        if (yValue > 0 && yValue > xValue) spriteRend.sprite = sprites[2];
        else if (yValue < 0 && yValue < xValue)spriteRend.sprite = sprites[3];
        else if (xValue > 0 && yValue < xValue) spriteRend.sprite = sprites[0];
        else if (xValue < 0 && yValue > xValue) spriteRend.sprite = sprites[1];
    }

    public Vector3 pos
    {
        get { return (this.transform.position); }
        set { this.transform.position = value; }
    }
    public void InitMovement()
    {
        p0 = p1;  //переписать р1 в р0
        // выбрать новую точку р1 на экране
        p1 = dir[Random.Range(0,dir.Length)];
        //сбросить время
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
        //u = 1 - Mathf.Pow(1 - u, 2);  //применить плавное замедление
        //pos = (1 - u) * p0 + u * p1;
        agent.SetDestination(p1);
    }
}
