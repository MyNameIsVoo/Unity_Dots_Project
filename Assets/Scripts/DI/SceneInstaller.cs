using UnityEngine;
using Zenject;

namespace DI
{
    public class SceneInstaller : MonoInstaller
    {
        
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject sceneTestObject;

        [SerializeField] private Transform spawnPoint;
        [SerializeField] private PlayerSettings playerSettings;
        [SerializeField] private TestSystem testSystem;

        public override void InstallBindings()
        {
            BindPlayer();
        }

        private void BindPlayer()
        {
            Container.Bind<PlayerSettings>().FromInstance(playerSettings);
            Player player = Container.InstantiatePrefabForComponent<Player>(playerPrefab, spawnPoint.position, Quaternion.identity, null);
            Container.BindInterfacesAndSelfTo<Player>().FromInstance(player).AsSingle();

            Container.BindInstance(testSystem).AsSingle();

            Container.BindFactory<SceneTestObject, SceneTestObject.Factory>().FromComponentInNewPrefab(sceneTestObject);
            Container.BindFactory<SceneTestObject, SceneTestObject.Factory>();
        }
    }
}