using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Signal", menuName = "ScriptableObjects/Signal", order = 1)]
public class Signal : ScriptableObject {
    private readonly IList<SignalListener> listeners = new List<SignalListener>();

    public IList<SignalListener> GetListeners() {
        return listeners;
    }

    public void DoSignal() {
        for(int i = listeners.Count - 1; i >= 0; i--) {
            listeners[i].OnSignalReceive();
        }
    }

    public void RegisterListener(SignalListener listener) {
        listeners.Add(listener);
    }

    public void UnregisterListener(SignalListener listener) {
        listeners.Remove(listener);
    }
}
