using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    public string levelName;
    
    [Space(10)]
    [Multiline(10)]
    public string levelDescription;
    
    [Space(10)]
    public Sprite levelImage;
}
