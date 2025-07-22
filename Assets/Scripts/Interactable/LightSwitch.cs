using UnityEngine;

public class LightSwitch : Interactable
{
    public bool lightOn = false;

    //turn GO on/off, unless 
    public GameObject lightObj;

    public override void Interact()
    {
        if (canInteractWith && iInteractInRangeToInteract)
        {
            lightOn = !lightOn;
            lightObj.SetActive(lightOn);
        }
    }
    
    public override string InteractString()
    {
        if(lightOn)
           return "TURN OFF LIGHT";

        return "TURN ON LIGHT";
    }
}