
using Zenject;
using GameCore.Factory;

public class ActorSpawnerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<AbstractFactory>().To<ActorFactory>().FromComponentInChildren().AsSingle();
    }
}
