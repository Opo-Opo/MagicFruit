using System;
using EliteMMO.API;

namespace MagicFruit.Xi
{
    public class PartyMember
    {
        public PartyMember(EliteAPI.PartyMember member)
        {
            Update(member);
        }

        public void Update(EliteAPI.PartyMember member)
        {
            Index = member.Index;
            MemberNumber = member.MemberNumber;
            ID = member.ID;
            TargetIndex = member.TargetIndex;

            Name = member.Name;
            MainJob = (Job)Enum.Parse(typeof(Job), member.MainJob.ToString());
            MainJobLevel = member.MainJobLvl;
            SubJob = (Job)Enum.Parse(typeof(Job), member.SubJob.ToString());
            SubJobLevel = member.SubJobLvl;

            HP = member.CurrentHP;
            HPP = member.CurrentMPP;
            MP = member.CurrentMP;
            MPP = member.CurrentMPP;
            TP = member.CurrentTP;
            Zone = member.Zone;
        }

        public bool Active { get; set; } = true;

        public uint Index { get; private set; }

        public uint MemberNumber { get; private set; }

        public uint ID { get; private set; }

        public uint TargetIndex { get; private set; }

        public string Name { get; private set; }

        public Job MainJob { get; private set; }

        public uint MainJobLevel { get; private set; }

        public Job SubJob { get; private set; }

        public uint SubJobLevel { get; private set; }

        public uint HP { get; private set; }
        public uint HPP { get; private set; }

        public uint MP { get; private set; }

        public uint MPP { get; private set; }

        public uint TP { get; private set; }

        public ushort Zone { get; private set; }

    }
}
