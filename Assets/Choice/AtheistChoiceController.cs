using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Multiunity.Unity; 
public class AtheistChoiceController : MonoBehaviour
{
    public GameObject destroyHeavenSignal;
    public GameObject destroyHellSignal;
    public GameObject destroyMetaphysicalSignal;
    public void Heaven() {
        EmptySignal choice = Instantiate(destroyHeavenSignal).GetComponent<EmptySignal>();
        choice.Init(destroyHeavenSignal);
        choice.Execute();
        Close();
    }
    public void Hell() {
        EmptySignal choice = Instantiate(destroyHellSignal).GetComponent<EmptySignal>();
        choice.Init(destroyHellSignal);
        choice.Execute();
        Close();
    }
    public void Metaphysical() {
        EmptySignal choice = Instantiate(destroyMetaphysicalSignal).GetComponent<EmptySignal>();
        choice.Init(destroyMetaphysicalSignal);
        choice.Execute();
        Close();
    }
    private void Close() {
        Game.initializer.layerController.SetLayer("Earth");
    }
}
