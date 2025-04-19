using UnityEngine;

public class ScannerPing : MonoBehaviour
{

    [SerializeField] float pingSpeed;
    [SerializeField] float maxScale;
    [SerializeField] float duration;

    float timer = 0f;
    Vector3 startScale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float scale = Mathf.Lerp(0, maxScale, timer / duration);
        transform.localScale = new Vector3(scale, transform.localScale.y, scale);

        if(timer >= duration)
        {
            Destroy(gameObject);
        }
            
    }
}
