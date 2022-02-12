using UnityEngine;

namespace GameCore.Abilities
{
    public interface IAbilityDisplying
    {
        Ability[] GetMainAbilities();
        BooksOfSpells GetBooksOfSpells();
    }
}
