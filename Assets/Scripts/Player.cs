using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static bool eaten = false;

    public GameObject footsteps;
    public Joystick joystick;
    public GameObject bombPrefab;
    public GameObject ScaleBar;
    public Scale scale;
    public int speed = 10;
    public float timePassed;
    public float timeMax = 5;
    public float delayBetweenSetBomb = 3f;
    public float lastSetBomb;
    public Sprite[] sprites;

    private float xAxis, yAxis;
    private SpriteRenderer spriteRend;
    private NavMeshAgent agent;
    private int eatenCabbage = 0;
    private void Start()
    {
        delayBetweenSetBomb = 0;
        timePassed = 0;
        scale = ScaleBar.GetComponentInChildren<Scale>();
        spriteRend = GetComponent<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    void Update()
    {
         xAxis = joystick.Horizontal;
         yAxis = joystick.Vertical;

        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        if (xAxis == 0 && yAxis == 0)
        {
            footsteps.SetActive(false);
        }
        else
        {
            footsteps.SetActive(true);
        }
        ChangeSprite();

    }
    private void OnCollisionEnter(Collision other)
    {
        Transform goTr = other.gameObject.transform;
        GameObject go = goTr.gameObject;

        if (go.tag == "Enemy")
        {
            //надпись FAILED и перезагрузка сцены
            GameManager.S.textFailed.SetActive(true);
            GameManager.S.DelayedRestart(3);
            //Debug.Log(go.name);
        }
        else
        {
            //Debug.Log(go.name);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Transform goTr = other.gameObject.transform;
        GameObject go = goTr.gameObject;

        if (other.tag == "Cabbage")
        {
            ScaleBar.SetActive(true);
            Enemy.isAngry = true;
            if (eaten)
            {
                eatenCabbage++;
                if (eatenCabbage == 6)
                {
                    GameManager.S.textSuccess.SetActive(true);
                    GameManager.S.DelayedRestart(3);
                }
                Destroy(other.gameObject);
                scale.timePassed = 0;
                scale.timeScale.fillAmount = 0;
                ScaleBar.SetActive(false);
                eaten = false;
                Enemy.isAngry = false;
            }
            //Debug.Log(go.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Cabbage")
        {
            scale.timePassed = 0;
            scale.timeScale.fillAmount = 0;
            ScaleBar.SetActive(false);
            Enemy.isAngry = false;
        }
    }

    public void SpawnBomb()
    {
        if (Time.time - lastSetBomb < delayBetweenSetBomb) return;

        GameObject go = Instantiate<GameObject>(bombPrefab);
        Vector3 pos = this.gameObject.transform.position;
        pos.z = 0;
        go.transform.position = pos;
        lastSetBomb = Time.time;
        delayBetweenSetBomb = 5f;
    }

    public void ChangeSprite()
    {
        if (yAxis > 0 && yAxis > xAxis) spriteRend.sprite = sprites[2];
        else if (yAxis < 0 && yAxis < xAxis) spriteRend.sprite = sprites[3];
        else if (xAxis > 0 && yAxis < xAxis) spriteRend.sprite = sprites[0];
        else if (xAxis < 0 && yAxis > xAxis) spriteRend.sprite = sprites[1];
    }
}
