using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionPrefab;

    private void Start()
    {
        Invoke("Explode", 3f);
    }

    public void Explode()
    {
        GameObject explosion = Instantiate(explosionPrefab);
        Vector3 pos = this.transform.position;
        pos.z = -3f;
        explosion.transform.position = pos;
        Destroy(this.gameObject,0.1f);
        Destroy(explosion, 3.95f);
    }

    //private void OnCollisionEnter(Collision other)
    //{
    //    Transform goTr = other.gameObject.transform;
    //    GameObject go = goTr.gameObject;

    //    if (go.tag == "Enemy")
    //    {

    //        Explode(go);
    //        Debug.Log("Bomb say" +go.name);

    //    }
    //    else
    //    {
    //        Debug.Log("Bomb say" +go.name);
    //    }
    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    Transform goTr = other.gameObject.transform;
    //    GameObject go = goTr.gameObject;

    //    if (go.tag == "Enemy")
    //    {

    //        Explode(go);
    //        Debug.Log("Bomb say" + go.name);

    //    }
    //    else
    //    {
    //        Debug.Log("Bomb say" + go.name);
    //    }

    //}
}
