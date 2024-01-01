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
        private Boo boo1;

        private SceneTestObject.Factory sceneTestObjectFactory;
        private Foo.Factory fooFactory;

        private List<SceneTestObject> testObjects;
        private Foo foo;

        private IEnumerator Start()
        {
            Debug.Log(inputSystem.testText);
            Debug.Log(movable.GetType());

            if (loadScene)
            {
                yield return new WaitForSeconds(1f);

                SceneManager.LoadSceneAsync("ECSScene");
            }

            CreateSceneObjects(5);
            foo = fooFactory.Create();

            boo1.Init("Boo 1");

            yield return new WaitForSeconds(5f);

            Boo boo2 = new(null);
            boo2.Init("Boo 2");
        }

        [Inject]
        private void Inject(InputSystem inputSystem, IMovable movable, TestSystem testSystem)
        {
            this.inputSystem = inputSystem;
            this.movable = movable;
            
            boo1 = new Boo(testSystem);
        }

        [Inject]
        private void InjectFactories(SceneTestObject.Factory sceneTestObjectFactory, Foo.Factory fooFactory)
        {
            this.sceneTestObjectFactory = sceneTestObjectFactory;
            this.fooFactory = fooFactory;
        }

        private void CreateSceneObjects(int amount)
        {
            testObjects = new List<SceneTestObject>();

            for (int i = 0; i < amount; i++)
            {
                SceneTestObject sceneTestObject = sceneTestObjectFactory.Create();
                testObjects.Add(sceneTestObject);
            }
        }
    }

    [Serializable]
    public class Boo
    {
        private TestSystem testSystem;
        private string name;

        public void Init(string name)
        {
            this.name = name;

            Debug.Log(name + " " + (testSystem != null ? testSystem.GetType() : "testSystem = null"));
        }

        public Boo(TestSystem testSystem)
        {
            this.testSystem = testSystem;
        }
    }

    [Serializable]
    public class Foo
    {
        private TestSystem testSystem;

        [Inject]
        private void Inject(TestSystem testSystem)
        {
            this.testSystem = testSystem;
        }

        public class Factory : PlaceholderFactory<Foo>
        {
        }
    }
}