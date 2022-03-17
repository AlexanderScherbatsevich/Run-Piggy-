using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static bool eaten = false;

 
    public GameObject textFailed;
    public GameObject bombPrefab;
    public GameObject ScaleBar;
    public Scale scale;
    public int speed = 10;
    public float timePassed;
    public float timeMax = 5;
    public float delayBetweenSetBomb = 5f;
    public float lastSetBomb;

    private NavMeshAgent agent;
    private void Start()
    {
        delayBetweenSetBomb = 0;
        timePassed = 0;
        scale = ScaleBar.GetComponentInChildren<Scale>();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        if (Input.GetAxis("Jump") == 1)
        {
            SpawnBomb();
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        Transform goTr = other.gameObject.transform;
        GameObject go = goTr.gameObject;

        if (go.tag == "Enemy")
        {
            //надпись FAILED и перезагрузка сцены
            textFailed.SetActive(true);
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

    //public void ChangeSprite()
    //{
    //    Vector2 pos = this.transform.position;
    //    float xValue = target.position.x - pos.x;
    //    float yValue = target.position.y - pos.y;

    //    if (yValue > 0 && yValue > xValue) spriteRend.sprite = spritesCalm[2];
    //    else if (yValue < 0 && yValue < xValue) spriteRend.sprite = spritesCalm[3];
    //    else if (xValue > 0 && yValue < xValue) spriteRend.sprite = spritesCalm[0];
    //    else if (xValue < 0 && yValue > xValue) spriteRend.sprite = spritesCalm[1];
    //}
}
