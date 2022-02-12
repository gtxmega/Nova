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
    public class BaseActorInSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            AttributesInstalls();
            MovementInstalls();
        }

        private void MovementInstalls()
        {
            Container
                .Bind<IMovement>()
                .To<Movements>()
                .FromComponentInParents();

            Container
                .Bind<IPoolObjects>()
                .To<PoolObjects>()
                .FromComponentInParents();

        }

        private void AttributesInstalls()
        {
            Container
                .Bind<IHealth>()
                .To<HealthAttributes>()
                .FromComponentsInParents();
            
            Container
                .Bind<IMana>()
                .To<ManaAttributes>()
                .FromComponentInParents();

            Container
                .Bind<IDamage>()
                .To<DamageAttributes>()
                .FromComponentInParents();

            Container
                .Bind<IArmor>()
                .To<ArmorAttributes>()
                .FromComponentInParents();

            Container
                .Bind<ISpecification>()
                .To<SpecificationAttributes>()
                .FromComponentInParents();

            Container
                .Bind<IExpirience>()
                .To<ExpirienceAttributes>()
                .FromComponentInParents();
        }
    }
}
