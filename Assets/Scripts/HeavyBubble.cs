using UnityEngine;

public class HeavyBubble : MonoBehaviour
{
    private GameManager gm;
    public HeavyBubbleData Data;
   
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject destination;
    private int _hp;
    
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody>();
        Invoke("Babl", Data.bablInterval);
    }

    private void Babl()
    {
        FlyToDoor();
        Invoke("Babl", Data.bablInterval);
    }
        
    private void FlyToDoor()
    {
        Vector3 dir = (destination.transform.position - transform.position).normalized;
        rb.AddForce(dir * Data.flyStrength);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gm.AddPoints(1);
            gm.AddCoins(15);
            Destroy(gameObject);
        }
    }
}

[System.Serializable]
public class HeavyBubbleData
{
    public float flyStrength;
    public float bablInterval;
}