using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionDestination : MonoBehaviour
{
    public enum DestinationTag
    {
        ENTER, A, B, C, D//Enumeration variables are used to identify the unique exit corresponding to the entrance of the portal 
    }
    public DestinationTag destinationTag;
}
