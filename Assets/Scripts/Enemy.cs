using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    isCalm,
    isAngry,
    isStuned,
}
public class Enemy : MonoBehaviour
{
    public static bool isAngry = false;

    public bool isStuned = false;

    public float timeStune = 3f;
    public Vector2 direction;
    Vector2[] dir;
    public Sprite[] spritesCalm;
    public Sprite[] spritesAngry;
    public Sprite spritesStuned;
    public SpriteRenderer spriteRend;

    [SerializeField] Transform target;
    private NavMeshAgent agent;
    private EnemyState state = EnemyState.isCalm;
    private float timePassed;
    private Vector3 newPos;      
    private float timeStart;     
    private float duration = 5;  //продолжительность перемещения
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

        Move(state);
        ChangeSprite(state);
        if (isAngry && !isStuned) state = EnemyState.isAngry;
        else if (isStuned)
        {
            isAngry = false;
            state = EnemyState.isStuned;
        }
        else state = EnemyState.isCalm;
    }

    public void ChangeSprite(EnemyState state)
    {
        Vector2 pos = this.transform.position;
        float xValue;
        float yValue;

        switch (state)
        {
            case EnemyState.isCalm:
                xValue = newPos.x - pos.x;
                yValue = newPos.y - pos.y;
                if (yValue > 0 && yValue > xValue) spriteRend.sprite = spritesCalm[2];
                else if (yValue < 0 && yValue < xValue) spriteRend.sprite = spritesCalm[3];
                else if (xValue > 0 && yValue < xValue) spriteRend.sprite = spritesCalm[0];
                else if (xValue < 0 && yValue > xValue) spriteRend.sprite = spritesCalm[1];
                break;
            case EnemyState.isAngry:
                 xValue = target.position.x - pos.x;
                 yValue = target.position.y - pos.y;
                if (yValue > 0 && yValue > xValue) spriteRend.sprite = spritesAngry[2];
                else if (yValue < 0 && yValue < xValue) spriteRend.sprite = spritesAngry[3];
                else if (xValue > 0 && yValue < xValue) spriteRend.sprite = spritesAngry[0];
                else if (xValue < 0 && yValue > xValue) spriteRend.sprite = spritesAngry[1];
                break;
            case EnemyState.isStuned:
                this.spriteRend.sprite = spritesStuned;
                break;
        }
    }

    public Vector3 pos
    {
        get { return (this.transform.position); }
        set { this.transform.position = value; }
    }
    public void InitMovement()
    {
        newPos = dir[Random.Range(0, dir.Length)];
        timeStart = Time.time;
    }

    public void Move(EnemyState state)
    {
        float u = (Time.time - timeStart) / duration;
        if (u >= 1)
        {
            InitMovement();
            u = 0;
        }

        switch (state)
        {
            case EnemyState.isCalm:
                agent.SetDestination(newPos);
                break;
            case EnemyState.isAngry:
                agent.SetDestination(target.position);
                break;
            case EnemyState.isStuned:
                agent.SetDestination(this.gameObject.transform.position);
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if (go.tag == "Explosion")
        {
            this.isStuned = true;
            isAngry = false;
            state = EnemyState.isStuned;
            Invoke("ChangeState", 3);
            Debug.Log("enemy say" + other.gameObject.name);
        }
    }
    public void ChangeState()
    {
        state = EnemyState.isCalm;
        this.isStuned = false;
    }

}
