using UnityEngine;

public class MothController : MonoBehaviour
{
    public Moth self;
    public float growSpeed = 0.01f;

    private void Update()
    {
        if(transform.localScale.x < 1)
        {
            transform.localScale = new Vector3(transform.localScale.x + growSpeed * Time.deltaTime, transform.localScale.y + growSpeed * Time.deltaTime, transform.localScale.z + growSpeed * Time.deltaTime);
        }
        if(transform.localScale.x > 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
