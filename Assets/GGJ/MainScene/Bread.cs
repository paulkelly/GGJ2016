using UnityEngine;
using System.Collections;

public class Bread : InterestingTarget
{
    private bool _locked = false;

    public void LockOn()
    {
        _locked = true;
    }

    public void Peck()
    {
        _locked = false;
    }
}
