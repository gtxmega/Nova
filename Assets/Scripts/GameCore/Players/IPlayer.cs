using GameCore.Actors;
using GameCore.CustomDataStruct;

namespace GameCore.Players
{
    public interface IPlayer
    {
        string PlayerName { get; }
        EPlayerTeams Team { get; }
        EPlayers PlayerID { get; }

        Actor GetMainActor();
    }
}
