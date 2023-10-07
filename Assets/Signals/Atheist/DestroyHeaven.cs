using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Multiunity.Unity;

public class DestroyHeaven : EmptySignal
{
    public override void Execute() {
        Debug.Log("destroying heaven");
        Game.heaven.Destroy();
        Forward();
    }
}
