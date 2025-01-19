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
        SetMeshMaterials(WingParts, Moth.Wings);
        SetMeshMaterials(BodyParts, Moth.Body);
    }

    private void SetMeshMaterials(List<MeshRenderer> meshRenderers, Material[] materials)
    {
        foreach(MeshRenderer meshRenderer in meshRenderers)
        {
            Material[] GOMaterials = meshRenderer.materials;
            Material[] bodyMaterials = new Material[Moth.Wings.Length + GOMaterials.Length];
            int i = 0;
            foreach(Material bodyMaterial in materials)
            {
                bodyMaterials[i] = bodyMaterial;
                i++;
            }
            foreach(Material bodyMaterial in GOMaterials)
            {
                bodyMaterials[i] = bodyMaterial;
                i++;
            }
            meshRenderer.materials = bodyMaterials;
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
