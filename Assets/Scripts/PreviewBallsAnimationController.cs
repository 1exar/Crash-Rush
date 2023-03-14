using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PreviewBallsAnimationController : MonoBehaviour
{
    public List<CardTypesData> types = new List<CardTypesData>();

    public Color GetCardColor(EntityType type)
    {
        Color resultColor = new Color();

        resultColor = types.First(i => i.type == type).color;
        
        return resultColor;
    }
}

[Serializable]
public struct CardTypesData
{
    public EntityType type;
    public Color color;
}
