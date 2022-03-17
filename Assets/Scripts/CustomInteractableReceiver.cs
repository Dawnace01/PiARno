using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomInteractableReceiver : ReceiverBaseMonoBehavior
{


    public CustomInteractableReceiver(UnityEvent ev) : base()
    {
        
    }

    public virtual void OnClick(InteractableStates state,
                                    Interactable source,
                                    IMixedRealityPointer pointer = null)
    {
        
    }

    public void click()
    {
        this.Interactable.HasCollision = true;
    }

    public void stopClick()
    {
        this.Interactable.HasCollision = false;
    }
}
