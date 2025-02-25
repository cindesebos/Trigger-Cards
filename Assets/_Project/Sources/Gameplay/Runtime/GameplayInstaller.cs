using Zenject;
using Sources.Gameplay.Runtime.Entities;

namespace Sources.Gameplay.Runtime
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindCharacterInput();
            BindEntitiesObserver();
            BindEntitiesSpawner();
        }

        private void BindCharacterInput()
        {
            Container.BindInterfacesAndSelfTo<CharacterInput>()
                .AsSingle();
        }

        private void BindEntitiesObserver()
        {
            Container.Bind<IEntitiesObserver>()
                .To<EntitiesObserver>()
                .AsSingle();
        }

        private void BindEntitiesSpawner()
        {
            Container.Bind<IEntitiesSpawner>()
                .To<EntitiesSpawner>()
                .AsSingle();
        }
    }
}
