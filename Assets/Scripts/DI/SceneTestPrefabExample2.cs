using UnityEngine;
using Zenject;

namespace DI
{
    public class SceneTestPrefabExample2 : MonoBehaviour
    {
        [Inject] private TestSystem testSystem;
        [Inject] private ExecutiveClass executiveClass;
    }
}