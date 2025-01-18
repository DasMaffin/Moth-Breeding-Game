using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
        set
        {
            if(_instance != null)
            {
                Destroy(value.gameObject);
                return;
            }
            _instance = value;
        }
    }

    #endregion

    public List<Species> AllMoths = new List<Species>();
    public Dictionary<Flower, int> OwnedFlowers = new Dictionary<Flower, int>(); // Flower, Amount; The amount you own of what flower.

    public SpeciesPair selectedMoths;
    public Flower selectedFlower;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        AllMoths = Resources.LoadAll<Species>("ScriptableObjects/Moth").ToList();
        foreach(Flower flower in Resources.LoadAll<Flower>("ScriptableObjects/Flower"))
        {
            OwnedFlowers.Add(flower, 0);
            HUDManager.Instance.AddFlowerToSelection(OwnedFlowers.FirstOrDefault(kv => kv.Key.Equals(flower)));
        }
        FirstStart();
    }

    float xAxis = 0;
    public void FirstStart()
    {
        foreach(Species moth in AllMoths)
        {
            if(moth.isStandardSpecies)
            {
                for(int i = 0; i < 2; i++)
                {
                    SpawnMoth(moth, (Gender)i);
                }
            }
        }
    }

    public void BreedMoths()
    {
        if(!selectedMoths.FirstMothController.Moth.CanBreed() || !selectedMoths.SecondMothController.Moth.CanBreed()) return;
        List<Species> potentialChildren = Species.FindMothsWithPair(selectedMoths);

        Species moth = Species.SelectRandomMoth(potentialChildren, selectedMoths, selectedFlower);

        SpawnMoth(moth, (Gender)UnityEngine.Random.Range(0, 2));

        selectedMoths.FirstMothController.Moth.BreedingCooldownActive = true;
        selectedMoths.SecondMothController.Moth.BreedingCooldownActive = true;
    }

    public void SpawnMoth(Species species, Gender gender, bool isBaby = false)
    {
        GameObject toInstantiate = species.Prefab;
        if(isBaby)
        {
            toInstantiate.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        }
        GameObject obj = Instantiate(toInstantiate, new Vector3(xAxis, 0, 0), toInstantiate.transform.rotation, null);
        MothController mc = obj.GetComponent<MothController>();
        mc.Moth = new Moth(species, gender);
        xAxis += 2f;
    }
}
