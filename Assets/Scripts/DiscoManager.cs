using System;
using System.Collections.Generic;
using TriInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiscoManager : MonoBehaviour
{
    [SerializeField]
    private List<SpriteRenderer> discoRenderers;

    [SerializeField]
    private Transform[] oceanTransforms;

    [SerializeField]
    private GameObject parent;
    
    private List<(Vector3 originalPositions, Transform transforms)> oceanTransformData;

    private void Start()
    {
        MusicManager.beatUpdated += Disco;
        MusicManager.markerUpdated += OnMarkerUpdated;

        oceanTransformData = new();
        foreach (var t in oceanTransforms)
        {
            oceanTransformData.Add((t.localPosition, t));
        }
    }

    private void OnMarkerUpdated()
    {
        if (MusicManager.lastMarkerString == "Party")
            parent.SetActive(true);
    }

    [Button]
    private void Disco()
    {
        var validDiscos = Random.Range(2, discoRenderers.Count);
        
        foreach (var disco in discoRenderers)
            disco.enabled = false;
        
        // choose randomly 
        while (validDiscos > 0)
        {
            var disco = discoRenderers[Random.Range(0, discoRenderers.Count)];
            
            if (disco.enabled) 
                continue;
            
            disco.enabled = true;
            validDiscos--;
        }

        foreach (var valueTuple in oceanTransformData)
        {
            valueTuple.transforms.localPosition = valueTuple.originalPositions + Vector3.right * Random.Range(-1.2f, 1.2f);
        }
    }
}