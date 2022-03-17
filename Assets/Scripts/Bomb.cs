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
        Destroy(explosion, 0.5f);
    }
}
