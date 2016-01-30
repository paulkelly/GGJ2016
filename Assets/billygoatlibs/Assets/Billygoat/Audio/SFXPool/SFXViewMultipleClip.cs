using UnityEngine;
using System.Collections;
using System.Linq;

public class SFXViewMultipleClip : SFXView
{
    public AudioClip[] Clips;
    private int next = 0;

    public AudioClip GetNextClip()
    {
        if (Clips.Any())
        {
            next = Random.Range(0, Clips.Count());
            return Clips[next];
        }
        return null;
    }

    public override void Play(float fadeTime)
    {
        SetClip(GetNextClip());

        base.Play(fadeTime);
    }
}
