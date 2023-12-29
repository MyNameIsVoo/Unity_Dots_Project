using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace DI
{
    public class ExecutiveClass : MonoBehaviour
    {
        [SerializeField] private bool loadScene;

        private InputSystem inputSystem;
        private IMovable movable;

        private IEnumerator Start()
        {
            Debug.Log(inputSystem.testText);
            Debug.Log(movable.GetType());

            if (loadScene)
            {
                yield return new WaitForSeconds(1f);

                SceneManager.LoadSceneAsync("ECSScene");
            }
        }

        [Inject]
        private void Inject(InputSystem inputSystem, IMovable movable)
        {
            this.inputSystem = inputSystem;
            this.movable = movable;
        }
    }

    public class Boo
    {
        private IMovable movable;

        public void Start()
        {
            Debug.Log(movable.GetType());
        }

        private Boo(IMovable movable)
        {
            this.movable = movable;

            Start();
        }
    }
}