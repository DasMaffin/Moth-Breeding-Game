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

    public List<Moth> AllMoths = new List<Moth>();
    public Dictionary<Flower, int> OwnedFlowers = new Dictionary<Flower, int>(); // Flower, Amount; The amount you own of what flower.

    public MothPair selectedMoths;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        AllMoths = Resources.LoadAll<Moth>("ScriptableObjects/Moth").ToList();
        FirstStart();
    }

    float xAxis = 0;
    public void FirstStart()
    {
        foreach(Moth moth in AllMoths)
        {
            if(moth.isStandardMoth)
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
        if(selectedMoths.FirstMothController.BreedingCooldownActive || selectedMoths.SecondMothController.BreedingCooldownActive) return;
        List<Moth> potentialChilds = Moth.FindMothsWithPair(selectedMoths);

        Moth moth = Moth.SelectRandomMoth(potentialChilds, selectedMoths);

        SpawnMoth(moth, Gender.No);

        selectedMoths.FirstMothController.BreedingCooldownActive = true;
        selectedMoths.SecondMothController.BreedingCooldownActive = true;
    }

    public void SpawnMoth(Moth moth, Gender gender, bool isBaby = false)
    {
        GameObject toInstantiate = moth.Prefab;
        if(isBaby)
        {
            toInstantiate.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        }
        GameObject obj = Instantiate(toInstantiate, new Vector3(xAxis, 0, 0), toInstantiate.transform.rotation, null);
        MothController mc = obj.GetComponent<MothController>();
        mc.self = moth;
        mc.Gender = gender;
        xAxis += 2f;
    }
}
