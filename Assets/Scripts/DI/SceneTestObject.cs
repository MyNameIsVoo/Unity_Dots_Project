using UnityEngine;
using Zenject;

namespace DI
{
    public class SceneTestObject : MonoBehaviour
    {
        private TestSystem testSystem;

        [Inject]
        private void Inject(TestSystem testSystem)
        {
            this.testSystem = testSystem;
        }

        public class Factory : PlaceholderFactory<SceneTestObject>
        {
        }
    }
}