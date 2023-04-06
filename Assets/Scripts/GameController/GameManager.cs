using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float greenSlimeExperienceDrop = 25;

    public float _greenSlimeExperienceDrop => greenSlimeExperienceDrop;
}
