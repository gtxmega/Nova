using GameCore;
using GameCore.Abilities;
using GameCore.ObjectSelector;
using GameCore.Players;
using UnityEngine;
using Zenject;

namespace Game.Zenject.Installers
{
    internal class InputControllerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _selectorGameObject;
        [SerializeField] private GameObject _playerGameObject;
        
        public override void InstallBindings()
        {
            Container
                .Bind<ISelector>()
                .To<Selector>()
                .FromComponentOn(_selectorGameObject)
                .AsSingle()
                .NonLazy();

            Container
                .Bind<IPlayer>()
                .To<Player>()
                .FromComponentOn(_playerGameObject)
                .AsSingle()
                .NonLazy();

        }
    }
}
