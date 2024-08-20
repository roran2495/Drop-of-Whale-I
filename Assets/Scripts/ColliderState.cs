using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColliderState
{
    public Collider collider;
    public bool isEnabled;

    public ColliderState(Collider coll, bool enabled)
    {
        collider = coll;
        isEnabled = enabled;
    }
}