using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Sphere : MonoBehaviour
{
    public WayPool WayPool;
    public float Speed;
    public float SpeedIncrease;
    public GameObject Canvas;
    public Text Text;
    public int Clicks;
    protected int TriggerCount = 0;
    protected Vector3 direction;
    protected bool Dead = false;
    protected float Cooldown = 0f;

    // Use this for initialization
    void Start()
    {
        direction = Vector3.forward;
        Cooldown = 0f;
        Dead = false;
        Speed = 5f;
        Clicks = 0;
        Canvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Dead)
            return;

        transform.position += direction * Time.deltaTime * Speed;
        Speed += Time.deltaTime * SpeedIncrease;

        if (Input.GetMouseButtonDown(0))
        {
            if (direction != WayPool.CurrentDirection)
                Clicks++;
            direction = WayPool.CurrentDirection;
            Text.text = "Clicks: " + Clicks;
        }

        if(Cooldown > 1f && TriggerCount <= 0)
        {
            Canvas.gameObject.SetActive(true);
            Dead = true;
        }
        Cooldown += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Cube>() != null)
            TriggerCount++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Cube>() != null)
            TriggerCount--;
    }

    public void ResetMe()
    {
        transform.position = Vector3.up * 0.25f;
        Start();
        WayPool.ResetMe();
    }
}
