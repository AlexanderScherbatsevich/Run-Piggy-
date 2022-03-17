using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cabbage : MonoBehaviour
{
    //public Vector3 step;
    //public Vector3 maxSize;

    private NavMeshAgent agent;


    void Start()
    {
        //step = new Vector3(0.01f, 0.01f, 0.01f);
        //maxSize = new Vector3(0.3f, 0.3f, 0.3f);
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    //public void Growth()
    //{
    //    Vector3 size = transform.localScale;
    //    if (Vector3.Equals(size, maxSize)) return;
    //    else transform.localScale += step * Time.deltaTime; ;
            

    //}
    //private void FixedUpdate()
    //{
    //    Growth();
    //}
}
