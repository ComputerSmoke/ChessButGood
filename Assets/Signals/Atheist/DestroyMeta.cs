using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Multiunity.Unity;

public class DestroyMetaphysicalSignal : EmptySignal
{
    public override void Execute() {
        Debug.Log("destroying metaphysical on earth");
        Game.earth.MetaPurge();
        Forward();
    }
}
