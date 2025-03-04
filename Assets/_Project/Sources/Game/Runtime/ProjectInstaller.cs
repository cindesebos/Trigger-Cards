using Zenject;
using Sources.Gameplay.Runtime.Entities;

namespace Sources.Game.Runtime
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindMonoCacheHandler();
        }

        private void BindMonoCacheHandler()
        {
            Container.BindInterfacesAndSelfTo<MonoCacheHandler>()
                .AsSingle();
        }
    }
}
