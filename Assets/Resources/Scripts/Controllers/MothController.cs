using UnityEngine;
using UnityEngine.EventSystems;

public class MothController : MonoBehaviour
{
    public Moth self;
    public Gender Gender;
    public float growSpeed = 0.001f;

    private float fullSize = 0.1f;

    private void Update()
    {
        if(transform.localScale.x < fullSize)
        {
            transform.localScale = new Vector3(transform.localScale.x + growSpeed * Time.deltaTime, transform.localScale.y + growSpeed * Time.deltaTime, transform.localScale.z + growSpeed * Time.deltaTime);
        }
        if(transform.localScale.x > fullSize)
        {
            transform.localScale = new Vector3(fullSize, fullSize, fullSize);
        }
    }
}
