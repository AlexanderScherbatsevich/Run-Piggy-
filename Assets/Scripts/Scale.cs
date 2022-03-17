using UnityEngine;
using UnityEngine.UI;

public class Scale : MonoBehaviour
{
    public Image timeScale;
    public float timeMax = 5f;
    public float timePassed;
    public GameObject canvas;

    void Start()
    {
        timePassed = 0f;
        timeScale.GetComponent<Image>();
    }

    void FixedUpdate()
    {      
        timePassed += Time.fixedDeltaTime;
        timeScale.fillAmount = timePassed / timeMax;
        if (timeScale.fillAmount >= 1)
        {
            Player.eaten = true;
        }

    }

    //public void Filling()
    //{

    //}
}
