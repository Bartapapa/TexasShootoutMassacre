using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sc_CollisionPart : MonoBehaviour
{
    public enum BodyPart
    {
        NONE,
        BODY,
        HEAD
    }
    [SerializeField] private BodyPart _bodyPart = BodyPart.NONE;

    public BodyPart GetBodyPart
    {
        get
        {
            return _bodyPart;

        }
    }
}
