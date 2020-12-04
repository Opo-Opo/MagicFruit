using System.Runtime.InteropServices;
using MagicFruit.Xi.Res;

namespace MagicFruit.Xi.Packet
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct CharacterHealth
    {
        private readonly uint Header;

        public readonly uint Id;

        public readonly uint HP;

        public readonly uint MP;

        public readonly uint TP;

        public readonly ushort Index;

        public readonly byte HPPercent;

        public readonly byte MPPercent;

        private readonly ushort _unknown1;

        private readonly ushort _unknown2;

        private readonly uint _unknown3;

        public readonly Job MainJob;

        public readonly byte MainJobLevel;

        public readonly Job SubJob;

        public readonly byte SubJobLevel;
    }
}
