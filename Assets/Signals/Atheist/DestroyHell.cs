using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Multiunity.Unity;

public class DestroyHell : EmptySignal
{
    public override void Execute() {
        Debug.Log("destroying hell");
        Game.hell.Destroy();
        Forward();
    }
}
