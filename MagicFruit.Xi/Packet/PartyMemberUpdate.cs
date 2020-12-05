using System.Runtime.InteropServices;
using MagicFruit.Xi.Res;

namespace MagicFruit.Xi.Packet
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public readonly struct PartyMemberUpdate
    {
        private readonly uint Header;

        public readonly uint Id;

        public readonly uint HP;

        public readonly uint MP;

        public readonly uint TP;

        public readonly ushort Flags;

        private readonly ushort _unknown1;

        public readonly ushort Index;

        private readonly ushort _unknown2;

        private readonly byte _unknown3;

        public readonly byte HPPercent;

        public readonly byte MPPercent;

        private readonly byte _unknown4;

        public readonly Zone Zone;

        public readonly Job MainJob;

        public readonly byte MainJobLevel;

        public readonly Job SubJob;

        public readonly byte SubJobLevel;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
        public readonly string Name;
    }
}
