using UnityEngine;
using Zenject;
using Sources.Gameplay.Runtime.Entities;
using Sources.Gameplay.Runtime.Panels;

namespace Sources.Gameplay.Runtime
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private CardSelectionPanel _cardSelectionPanel;

        public override void InstallBindings()
        {
            BindCharacterInput();
            BindEntitiesObserver();
            BindEntitiesSpawner();
            BindCardSelectionPanel();
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

        private void BindCardSelectionPanel()
        {
            Container.Bind<CardSelectionPanel>()
                .FromInstance(_cardSelectionPanel)
                .AsSingle();
        }
    }
}
