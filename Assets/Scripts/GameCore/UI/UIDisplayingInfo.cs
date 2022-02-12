

using GameCore.Actors;

namespace GameCore.UI
{
    public interface UIDisplayingInfo

    {
        void Displaying(Actor actor);
        void HideDisplaying();
    }
}
