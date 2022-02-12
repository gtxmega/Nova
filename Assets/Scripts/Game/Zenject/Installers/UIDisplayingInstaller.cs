using GameCore.UI;
using UnityEngine;
using Zenject;

namespace Game.Zenject.Installers
{
    public class UIDisplayingInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _actorBaseDisplaying;
        [SerializeField] private GameObject _actorAbilitiesDisplaying;

        public override void InstallBindings()
        {
            Container
                .Bind<UIDisplayingInfo>()
                .WithId("ActorDisplaying")
                .To<UIActorDisplaying>()
                .FromComponentOn(_actorBaseDisplaying)
                .AsSingle()
                .NonLazy();

            Container
                .Bind<UIDisplayingInfo>()
                .WithId("ActorAbilitiesDisplaying")
                .To<UIActorAbilitiesDisplaying>()
                .FromComponentOn(_actorAbilitiesDisplaying)
                .AsSingle()
                .NonLazy();
        }

    }
}
