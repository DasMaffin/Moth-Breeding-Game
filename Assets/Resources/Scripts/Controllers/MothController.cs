using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MothController : MonoBehaviour
{
    public Moth Moth;

    [SerializeField] private List<MeshRenderer> WingParts = new List<MeshRenderer>();
    [SerializeField] private List<MeshRenderer> BodyParts = new List<MeshRenderer>();

    private void Start()
    {
        foreach(MeshRenderer wingPart in WingParts)
        {
            Material[] GOMaterials = wingPart.materials;
            Material[] wingMaterials = new Material[Moth.Wings.Length + GOMaterials.Length];
            int i = 0;
            foreach(Material wingMaterial in Moth.Wings)
            {
                wingMaterials[i] = wingMaterial;
                i++;
            }
            foreach(Material wingMaterial in GOMaterials)
            {
                wingMaterials[i] = wingMaterial;
                i++;
            }
            wingPart.materials = wingMaterials;
        }
        foreach(MeshRenderer bodyPart in BodyParts)
        {
            Material[] bodyMaterials = new Material[Moth.Wings.Length + bodyPart.materials.Length];
            int i = 0;
            foreach(Material bodyMaterial in Moth.Body)
            {
                bodyMaterials[i] = bodyMaterial;
                i++;
            }
            foreach(Material bodyMaterial in bodyPart.materials)
            {
                bodyMaterials[i] = bodyMaterial;
                i++;
            }
            bodyPart.materials = bodyMaterials;
        }
    }

    private void Update()
    {
        Moth.ElapseBreedingCooldown(Time.deltaTime);
        if(transform.localScale.x < Moth.fullSize)
        {
            transform.localScale = new Vector3(transform.localScale.x + Moth.growSpeed * Time.deltaTime, transform.localScale.y + Moth.growSpeed * Time.deltaTime, transform.localScale.z + Moth.growSpeed * Time.deltaTime);
        }
        if(transform.localScale.x > Moth.fullSize)
        {
            transform.localScale = new Vector3(Moth.fullSize, Moth.fullSize, Moth.fullSize);
        }
    }
}
