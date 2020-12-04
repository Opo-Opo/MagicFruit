using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using MagicFruit.Xi.Res;

namespace MagicFruit.Xi.Packet
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct CharacterBuff : IEnumerable<StatusEffect>
    {
        private readonly uint Header;

        private readonly ushort _unknown1;

        private readonly ushort _unknown2;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public readonly StatusEffect[] Buffs;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public readonly uint[] Times;

        public IEnumerator<StatusEffect> GetEnumerator()
        {
            foreach (var buff in Buffs)
            {
                if (buff == StatusEffect.None) yield break;

                yield return buff;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
