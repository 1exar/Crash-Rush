using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PreviewBallsAnimationController : MonoBehaviour
{

    public List<RawImageData> images = new List<RawImageData>();

    public RenderTexture GetRawImage(EntityType type)
    {
        RenderTexture result = null;

        result = images.First(i => i.type == type).image;
        
        return result;
    }
}

[Serializable]
public struct RawImageData
{
    public EntityType type;
    public RenderTexture image;
}
