using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WindowBase : MonoBehaviour
{
    public abstract void Activate(Action callback = null);
    public abstract void Deactivate(Action callback = null);
}
