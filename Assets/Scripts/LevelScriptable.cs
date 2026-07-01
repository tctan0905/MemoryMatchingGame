using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelScriptable", menuName = "Scriptable Objects/LevelScriptable")]
public class LevelScriptable : ScriptableObject
{
    public List<LevelData> levelDatas;
}
