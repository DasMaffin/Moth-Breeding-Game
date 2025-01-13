using UnityEngine;
using UnityEngine.EventSystems;

public class MothController : MonoBehaviour
{
    public Moth self;
    public Gender Gender;
    public bool BreedingCooldownActive = false;
    public float BreedingCooldown = 30f;
    public float growSpeed = 0.001f;
    public float BreedingCooldownLeft { get => BreedingCooldown - currentBreedingCooldown; }

    private float fullSize = 0.1f;
    private float currentBreedingCooldown = 0f;

    private void Update()
    {
        if(BreedingCooldownActive)
        {
            currentBreedingCooldown += Time.deltaTime;
            if(currentBreedingCooldown > BreedingCooldown)
            {
                currentBreedingCooldown = 0f;
                BreedingCooldownActive = false;
            }
        }
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
