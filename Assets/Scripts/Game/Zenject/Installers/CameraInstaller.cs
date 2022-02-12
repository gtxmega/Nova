using UnityEngine;
using Zenject;

namespace Game.Zenject.Installers
{
    public class CameraInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _camera;
        
        public override void InstallBindings()
        {
            Container
                .Bind<Camera>()
                .To<Camera>()
                .FromComponentOn(_camera)
                .AsCached()
                .NonLazy();
        }
    }
}
