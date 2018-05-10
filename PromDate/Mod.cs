using System;
using UnityEngine;

public abstract class Mod : MonoBehaviour
{
    public abstract string Name { get; }
    public abstract string Author { get; }
    public abstract Version Version { get; }

    public virtual string DisplayName { get { return Name; } }
}

