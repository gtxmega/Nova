using GameCore;
using GameCore.Attributes.Armor;
using GameCore.Attributes.Damage;
using GameCore.Attributes.Expirience;
using GameCore.Attributes.Health;
using GameCore.Attributes.Mana;
using GameCore.Attributes.Specification;
using GameCore.Movement;
using Zenject;

namespace Game.Zenject.Installers
{
    public class DiContainerInstaller : MonoInstaller
    {
        [Inject] DiContainer _baseActorContainer;

        public override void InstallBindings()
        {
            _baseActorContainer = new DiContainer();
            
            AttributesInstaller();
            MovementInstalls();
        }

        private void AttributesInstaller()
        {
            _baseActorContainer
                .Bind<IHealth>()
                .To<HealthAttributes>()
                .FromComponentsInParents();

            _baseActorContainer
                .Bind<IMana>()
                .To<ManaAttributes>()
                .FromComponentInParents();

            _baseActorContainer
                .Bind<IDamage>()
                .To<DamageAttributes>()
                .FromComponentInParents();

            _baseActorContainer
                .Bind<IArmor>()
                .To<ArmorAttributes>()
                .FromComponentInParents();

            _baseActorContainer
                .Bind<ISpecification>()
                .To<SpecificationAttributes>()
                .FromComponentInParents();
            
            _baseActorContainer
                .Bind<IExpirience>()
                .To<ExpirienceAttributes>()
                .FromComponentInParents();
            
        }

        private void MovementInstalls()
        {
            _baseActorContainer
                .Bind<IMovement>()
                .To<Movements>()
                .FromComponentInParents();

            _baseActorContainer
                .Bind<IPoolObjects>()
                .To<PoolObjects>()
                .FromComponentInParents();
        }
    }
}
