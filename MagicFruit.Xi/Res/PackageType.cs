using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicFruit.Xi.Res
{
    // https://github.com/project-topaz/topaz/blob/release/src/map/packets/
    public enum PackageType : uint
    {
        Action = 0x24,
        CharacterHealth = 0xDF,
        CharacterUpdate = 0x37,
        PartyMemberUpdate = 0xDD,
        PartyMemberStatusEffects = 0x76,
    }
}
