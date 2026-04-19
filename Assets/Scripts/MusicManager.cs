using System;
using System.Runtime.InteropServices;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using TriInspector;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private EventReference music;
    private static EventInstance musicInstance;
    
    public TimelineInfo timelineInfo = null;

    private GCHandle timelineHandle;

    private EVENT_CALLBACK beatCallback;
    private EventDescription descriptionCallback;

    public delegate void BeatEventDelegate();
    public static event BeatEventDelegate beatUpdated;

    public delegate void MarkerListenerDelegate();
    public static event MarkerListenerDelegate markerUpdated;

    public static int lastBeat = 0;
    public static string lastMarkerString = null;
    
    private bool _isPlaying;
    

    private void Awake()
    {
        musicInstance = RuntimeManager.CreateInstance(music);
        Game.OnGameStart += StartGame;
    }

    private void StartGame()
    {
        musicInstance.start();
        
        timelineInfo = new TimelineInfo();
        beatCallback = BeatEventCallback;
        
        timelineHandle = GCHandle.Alloc(timelineInfo, GCHandleType.Pinned);
        musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));
        musicInstance.setCallback(beatCallback, EVENT_CALLBACK_TYPE.TIMELINE_BEAT | EVENT_CALLBACK_TYPE.TIMELINE_MARKER);

        musicInstance.getDescription(out descriptionCallback);
        descriptionCallback.getLength(out int length);
        timelineInfo.songLength = length;
        
        _isPlaying = true;
    }

    private void OnDestroy()
    {
        musicInstance.setUserData(IntPtr.Zero);
        musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        musicInstance.release();
        
        timelineHandle.Free();
    }

    private void Update()
    {
        if (!_isPlaying)
            return;
        
        musicInstance.getTimelinePosition(out timelineInfo.currentPosition);
        
        if (lastMarkerString != timelineInfo.lastMarker)
        {
            lastMarkerString = timelineInfo.lastMarker;
            markerUpdated?.Invoke();
        }
        
        if (lastBeat != timelineInfo.currentBeat)
        {
            lastBeat = timelineInfo.currentBeat;
            beatUpdated?.Invoke();
        }
    }

    [Button]
    private void TestMarker(string marker)
    {
        lastMarkerString = marker;
        markerUpdated?.Invoke();
    }

    [AOT.MonoPInvokeCallback(typeof(EVENT_CALLBACK))]
    static RESULT BeatEventCallback(EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr)
    {
        EventInstance instance = new EventInstance(instancePtr);
        RESULT result = instance.getUserData(out var timelineInfoPtr);
        
        if (result != RESULT.OK)
        {
            Debug.LogError("Timeline Callback error: " + result);
        }
        else if (timelineInfoPtr != IntPtr.Zero)
        {
            GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
            TimelineInfo timelineInfo = (TimelineInfo)timelineHandle.Target;

            switch (type)
            {
                case EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
                {
                    var parameter = (TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(TIMELINE_BEAT_PROPERTIES));
                    timelineInfo.currentBeat = parameter.beat;
                    timelineInfo.currentBar = parameter.bar;
                    timelineInfo.currentTempo = parameter.tempo;
                }
                    break;
                case EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                {
                    var parameter = (TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(TIMELINE_MARKER_PROPERTIES));
                    timelineInfo.lastMarker = parameter.name;
                }
                    break;
            }
        }
        
        return RESULT.OK;
    }
}

[StructLayout(LayoutKind.Sequential)]
public class TimelineInfo
{
    public int currentBeat = 0;
    public int currentBar = 0;
    public float currentTempo = 0;
    public int currentPosition = 0;
    public float songLength = 0;
    public FMOD.StringWrapper lastMarker = new();
}
