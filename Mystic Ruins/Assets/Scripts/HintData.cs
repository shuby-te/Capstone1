using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HintData", menuName = "ScriptableObjects/MapHintData", order = 1)]
public class HintData : ScriptableObject
{
    [TextArea(3, 10)]
    public string[] hints;
}
