using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Multiunity.Unity;
public class DevilChoiceController : MonoBehaviour
{
    public GameObject releaseDevilSignal;
    public GameObject chargeSignal;
    public void Release() {
        EmptySignal choice = Instantiate(releaseDevilSignal).GetComponent<EmptySignal>();
        choice.Init(releaseDevilSignal);
        choice.Execute();
        Close();
    }
    public void Money() {
        ChargeSignal c1 = Instantiate(chargeSignal).GetComponent<ChargeSignal>();
        int picker = Game.devilLander.color;
        c1.Init(picker, -10);
        c1.Execute();
        ChargeSignal c2 = Instantiate(chargeSignal).GetComponent<ChargeSignal>();
        c2.Init((picker+1)%2, -5);
        c2.Execute();
        Close();
    }
    private void Close() {
        Game.initializer.layerController.SetLayer("Earth");
    }
}
