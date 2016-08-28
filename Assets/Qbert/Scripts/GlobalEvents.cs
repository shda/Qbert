using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GlobalEvents
{
    public interface IGlobalEvent
    {
        void OnPause(bool pause);
    }

    public static List<IGlobalEvent> events = new List<IGlobalEvent>();
    private static bool _isPause = false;

    public static bool isPause
    {
        get { return _isPause; }
        set
        {
            UpdatePauseEvents();
            _isPause = value;
        }
    }

    private static void UpdatePauseEvents()
    {
        events.RemoveAll(x => x == null);
        foreach (var globalEvent in events)
        {
            globalEvent.OnPause(_isPause);
        }
    }
}
