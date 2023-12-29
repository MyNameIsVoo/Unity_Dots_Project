using System;
using UnityEngine;

namespace DI
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Settings/Player")]
    public class PlayerSettings : ScriptableObject
    {
        [field: SerializeField, Range(0, 10)] public float Speed { get; private set; }
    }
}
