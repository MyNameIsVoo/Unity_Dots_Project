using UnityEngine;
using Zenject;

namespace DI
{
    public class TestSystem : MonoBehaviour
    {
        private InputSystem inputSystem;

        private void Start()
        {
            Debug.Log("TestSystem => " + inputSystem.testText);
        }

        [Inject]
        private void Inject(InputSystem inputSystem)
        {
            this.inputSystem = inputSystem;
        }
    }
}