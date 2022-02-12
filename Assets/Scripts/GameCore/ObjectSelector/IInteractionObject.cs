

using GameCore.Actors;

namespace GameCore
{
    public interface IInteractionObject
    {
        void OnSelect();
        void OnDeselect();
        Actor GetActor();
    }
}

