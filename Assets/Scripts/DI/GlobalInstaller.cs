using UnityEngine;
using Zenject;

namespace DI
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private InputSystem inputSystem;

        public override void InstallBindings()
        {
            Container.Bind<InputSystem>().FromInstance(inputSystem);
        }
    }
}