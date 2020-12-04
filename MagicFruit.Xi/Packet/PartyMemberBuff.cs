using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using MagicFruit.Xi.Res;

namespace MagicFruit.Xi.Packet
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct PartyBuff : IEnumerable<PartyMemberBuff>
    {
        public readonly uint Header;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public readonly PartyMemberBuff[] Members;

        public IEnumerator<PartyMemberBuff> GetEnumerator()
        {
            foreach (var member in Members)
            {
                if (member.Id <= 0) yield break;

                yield return member;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct PartyMemberBuff : IEnumerable<StatusEffect>
    {
        public readonly uint Id;

        public readonly ushort Index;

        private readonly ushort unknown1;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        private readonly byte[] BitMask;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        private readonly byte[] Buffs;

        public StatusEffect GetBuffAt(int index)
        {
            var highBits = (BitMask[index / 4] >> (index % 4) * 2) & ~(0xFF << 2);

            return (StatusEffect)((highBits << 8) | Buffs[index]);
        }

        public IEnumerator<StatusEffect> GetEnumerator()
        {
            for (int i = 0; i < 32; i++)
            {
                var buff = GetBuffAt(i);

                if (buff == StatusEffect.None) yield break;
                
                yield return GetBuffAt(i);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
