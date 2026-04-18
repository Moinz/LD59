using System;
using System.Runtime.InteropServices;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    
    [SerializeField]
    private EventReference music;
    
    public TimelineInfo timelineInfo = null;

    private GCHandle timelineHandle;

    private EVENT_CALLBACK beatCallback;
    private EventDescription descriptionCallback;
    private static EventInstance eventInstance;

    private void Awake()
    {
        Instance = this;
        
        eventInstance = RuntimeManager.CreateInstance(music);
        eventInstance.start();
    }

    private void Start()
    {
        timelineInfo = new TimelineInfo();
        beatCallback = BeatEventCallback;
        
        timelineHandle = GCHandle.Alloc(timelineInfo, GCHandleType.Pinned);
        eventInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));
        eventInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);

        eventInstance.getDescription(out descriptionCallback);
        descriptionCallback.getLength(out int length);
        timelineInfo.songLength = length;
    }

    private void OnDestroy()
    {
        eventInstance.setUserData(IntPtr.Zero);
        eventInstance.stop(STOP_MODE.IMMEDIATE);
        eventInstance.release();
        
        timelineHandle.Free();
    }

    private void Update()
    {
        eventInstance.getTimelinePosition(out timelineInfo.currentPosition);
    }

    [AOT.MonoPInvokeCallback(typeof(EVENT_CALLBACK))]
    static RESULT BeatEventCallback(EVENT_CALLBACK_TYPE type, IntPtr _eventPtr, IntPtr parameterPtr)
    {       
        RESULT result = eventInstance.getUserData(out var timelineInfoPtr);
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
    public FMOD.StringWrapper lastMarker = new FMOD.StringWrapper();
}
