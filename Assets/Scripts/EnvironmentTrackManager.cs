using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnvironmentTrackManager : MonoBehaviour
{
    public static Vector2 MapRange = new(120f, -30f);

    private List<EnvironmentTrack> tracks;

    private void Start()
    {
        tracks = new List<EnvironmentTrack>(GetComponentsInChildren<EnvironmentTrack>());
        
        MusicManager.markerUpdated += OnMarkerUpdated;
    }

    private void OnMarkerUpdated()
    {
        if (MusicManager.lastMarkerString == "Whiteout")
        {
            Invoke(nameof(Disable), 0.5f);
        }
    }

    private void Update()
    {
        tracks = tracks.OrderBy(x => x.NormalizedProgress).ToList();
        
        for (int i = 0; i < tracks.Count; i++)
        {
            tracks[i].SetSortingGroup(i);
        }
    }

    private void Disable()
    {
        foreach (var environmentTrack in tracks)
        {
            environmentTrack.gameObject.SetActive(false);
        }
    }
}