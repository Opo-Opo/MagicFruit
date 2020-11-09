using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EliteMMO.API;
using MagicFruit.Xi.Annotations;
using MagicFruit.Xi.Res;

namespace MagicFruit.Xi
{
    public class PartyMember : INotifyPropertyChanged
    {
        public PartyMember(EliteAPI.PartyMember member)
        {
            Index = member.Index;
            Name = member.Name;

            Update(member);
        }

        public void Update(EliteAPI.PartyMember member)
        {
            MemberNumber = member.MemberNumber;
            Id = member.ID;
            TargetIndex = member.TargetIndex;

            MainJob = (Job)Enum.Parse(typeof(Job), member.MainJob.ToString());
            MainJobLevel = member.MainJobLvl;

            SubJob = (Job)Enum.Parse(typeof(Job), member.SubJob.ToString());
            SubJobLevel = member.SubJobLvl;

            HP = member.CurrentHP;
            HPPercent = member.CurrentHPP;

            MP = member.CurrentMP;
            MPPercent = member.CurrentMPP;

            Zone = (Zone)Enum.Parse(typeof(Zone), member.Zone.ToString());
        }

        public bool Equals(EliteAPI.PartyMember member)
        {
            return member.Index == Index;
        }

        private bool _active = true;

        public bool Active
        {
            get => _active;
            set
            {
                if (_active == value) return;

                _active = value;
                OnPropertyChanged();
            }
        }

        private uint _index;

        /// <summary>
        /// Entity's Index in Memory
        /// </summary>
        public uint Index { 
            get => _index;
            private set
            {
                if (_index == value) return;
                
                _index = value;
                OnPropertyChanged();
            }
        }

        private uint _memberNumber;

        /// <summary>
        /// Index of the Party Member in the Alliance
        /// </summary>
        public uint MemberNumber { 
            get => _memberNumber;
            private set
            {
                if (_memberNumber == value) return;

                _memberNumber = value;
                OnPropertyChanged();
            }
        }

        private uint _id;

        /// <summary>
        /// Entity ID
        /// </summary>
        public uint Id
        {
            get => _id;
            private set
            {
                if (_id == value) return;

                _id = value;
                OnPropertyChanged();
            }
        }

        private uint _targetIndex;

        /// <summary>
        /// Index of the Party Member's Target
        /// </summary>
        public uint TargetIndex
        {
            get => _targetIndex;
            private set
            {
                if (_targetIndex == value) return;

                _targetIndex = value;
                OnPropertyChanged();
            }
        }

        private string _name;

        /// <summary>
        /// Player Name
        /// </summary>
        public string Name
        {
            get => _name;
            private set
            {
                if (_name == value) return;

                _name = value;
                OnPropertyChanged();
            }
        }

        private Job _mainJob;

        /// <summary>
        /// Main Job
        /// </summary>
        public Job MainJob
        {
            get => _mainJob;
            private set
            {
                if (_mainJob == value) return;

                _mainJob = value;
                OnPropertyChanged();
            }
        }

        private uint _mainJobLevel;

        /// <summary>
        /// Main Job Level
        /// </summary>
        public uint MainJobLevel
        {
            get => _mainJobLevel;
            private set
            {
                if (_mainJobLevel == value) return;

                _mainJobLevel = value;
                OnPropertyChanged();
            }
        }

        private Job _subJob;

        /// <summary>
        /// Support Job
        /// </summary>
        public Job SubJob
        {
            get => _subJob;
            private set
            {
                if (_subJob == value) return;

                _subJob = value;
                OnPropertyChanged();
            }
        }

        private uint _subJobLevel;

        /// <summary>
        /// Support Job Level
        /// </summary>
        public uint SubJobLevel
        {
            get => _subJobLevel;
            private set
            {
                if (_subJobLevel == value) return;

                _subJobLevel = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Estimate Full Health Points
        /// </summary>
        public uint MaxHP => (uint)Math.Ceiling(100.0 * HP / HPPercent);

        private uint _hp;

        /// <summary>
        /// Health Points
        /// </summary>
        public uint HP
        {
            get => _hp;
            private set
            {
                if (_hp == value) return;

                _hp = value;
                OnPropertyChanged();
            }
        }

        private uint _hpPercent;

        /// <summary>
        /// Percentage of Total Health Points
        /// </summary>
        public uint HPPercent
        {
            get => _hpPercent;
            private set
            {
                if (_hpPercent == value) return;

                _hpPercent = value;
                OnPropertyChanged();
            }
        }

        public uint HPMissing => MaxHP - HP;

        private uint _mp;

        /// <summary>
        /// Mana Points
        /// </summary>
        public uint MP
        {
            get => _mp;
            private set
            {
                if (_mp == value) return;

                _mp = value;
                OnPropertyChanged();
            }
        }

        private uint _mpPercent;

        /// <summary>
        /// Percentage of Total Mana Points
        /// </summary>
        public uint MPPercent
        {
            get => _mpPercent;
            private set
            {
                if (_mpPercent == value) return;

                _mpPercent = value;
                OnPropertyChanged();
            }
        }

        private Zone _zone;

        /// <summary>
        /// Zone the Player is in
        /// </summary>
        public Zone Zone
        {
            get => _zone;
            private set
            {
                if (_zone == value) return; 

                _zone = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
