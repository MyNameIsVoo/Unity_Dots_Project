using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace DI
{
    public class Player : MonoBehaviour, IDisposable, IMovable
    {

        public float Speed { get; private set; }
        public Transform Transform => transform;

        public virtual void Move()
        {
            Debug.Log("Move player");
        }

        [Inject]
        public void Inject(PlayerSettings playerSettings)
        {
            Speed = playerSettings.Speed;
        }

        public void Dispose()
        {
            Debug.Log("Тут происходит освобождение");
        }
    }

    public interface IMovable
    {
        public float Speed { get; }
        public Transform Transform { get; }
    }

    public interface IStatic
    {

    }
}
