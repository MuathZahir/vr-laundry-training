using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Page : MonoBehaviour
{
    public abstract void Initialize();
    public abstract void OnLevelLoaded();
    public abstract void OnLevelRestart();
    public abstract void OnLevelComplete();
}
