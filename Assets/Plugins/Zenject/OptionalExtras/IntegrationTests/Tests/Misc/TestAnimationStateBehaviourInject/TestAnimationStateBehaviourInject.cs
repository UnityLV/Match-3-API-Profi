
using System.Collections;
using ModestTree;
using UnityEngine.TestTools;

namespace Zenject.Tests.TestAnimationStateBehaviourInject
{
    public class TestAnimationStateBehaviourInject : ZenjectIntegrationTestFixture
    {
        const string ResourcePrefix = "TestAnimationStateBehaviourInject/";

        [UnityTest]
        public IEnumerator Test1()
        {
            PreInstall();
            var prefab = FixtureUtil.GetPrefab(ResourcePrefix + "Foo");

            StateBehaviour.OnStateEnterCalls = 0;

            Container.InstantiatePrefab(prefab);
            Container.BindInterfacesAndSelfTo<Foo>().AsSingle();
            PostInstall();

            yield return null;

            Assert.IsEqual(StateBehaviour.OnStateEnterCalls, 1);
        }

        public class Foo : IInitializable
        {
            public bool HasInitialized
            {
                get; private set;
            }

            public void Initialize()
            {
                HasInitialized = true;
            }
        }
    }
}

