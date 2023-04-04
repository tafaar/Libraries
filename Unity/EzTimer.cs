using System;
using System.Collections.Generic;
using UnityEngine;

//A single component capable of holding multiple timed functions with independent trigger times

public class EzTimer : MonoBehaviour
{
    class Node
    {
        public Action timerCallback;
        public float timerStart;
        public float timerCurrent;
        public bool loop;

        public bool IsComplete()
        {
            return timerCurrent <= 0;
        }

        public float TimeElapsed()
        {
            return timerStart - timerCurrent;
        }
    }

    Dictionary<string, Node> nodes;

    public bool destroyOnFinish;

    private void Awake()
    {
        nodes = new Dictionary<string, Node>();
    }

    public void SetTimer(string id, Action timerCallback, float timer, bool loop = false)
    {
        Node action;
        bool newAction = true;

        if (nodes.ContainsKey(id)) 
        { 
            newAction = false;
            action = nodes[id];
        }
        else
        {
            action = new Node();
        }

        action.timerCallback = timerCallback;
        action.timerStart = timer;
        action.timerCurrent = timer;
        action.loop = loop;

        if (!newAction) return;

        nodes.Add(id, action);
    }

    public void SetTimer(string id, Action timerCallback, Animator anim, float timer = -1f, bool loop = false)
    {
        Node action;
        bool newAction = true;

        if (nodes.ContainsKey(id))
        {
            newAction = false;
            action = nodes[id];
        }
        else
        {
            action = new Node();
        }

        timer = timer == -1? Utils.GetClipLength(anim, id) : timer;

        action.timerCallback = timerCallback;
        action.timerStart = timer;
        action.timerCurrent = timer;
        action.loop = loop;

        if (!newAction) return;

        nodes.Add(id, action);
    }

    void Update()
    {
        foreach(KeyValuePair<string, Node> n in nodes)
        {
            n.Value.timerCurrent -= Time.deltaTime;
            if (n.Value.IsComplete())
            {
                n.Value.timerCallback();

                if (!n.Value.loop)
                {
                    nodes.Remove(n.Key);
                    return;
                }

                n.Value.timerCurrent = n.Value.timerStart;
            }
        }

        if (destroyOnFinish && nodes.Count == 0) Destroy(this);
    }

    public float GetTime(string id)
    {
        if (!nodes.ContainsKey(id))
        {
            return -1f;
        }

        return nodes[id].TimeElapsed();
    }

    public void Clear()
    {
        nodes.Clear();
    }

    
}
