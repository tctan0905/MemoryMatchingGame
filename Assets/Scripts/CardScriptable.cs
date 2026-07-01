using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "CardScriptable", menuName = "Scriptable Objects/CardScriptable")]

public class CardScriptable : ScriptableObject
{
    public List<CardData> cardData;
}
