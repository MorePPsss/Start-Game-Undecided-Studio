using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionDestination : MonoBehaviour
{
    public enum DestinationTag
    {
        ENTER, A, B, C, D1, D2, E1, E2//Enumeration variables are used to identify the unique exit corresponding to the entrance of the portal 
    }
    public DestinationTag destinationTag;
}
