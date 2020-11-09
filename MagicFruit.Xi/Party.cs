using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EliteMMO.API;

namespace MagicFruit.Xi
{
    public class Party
    {
        private readonly EliteAPI _eliteApi;

        public ObservableCollection<PartyMember> Members { get; }
            = new ObservableCollection<PartyMember>();

        public Party(EliteAPI eliteApi)
        {
            _eliteApi = eliteApi;
        }

        public void UpdatePartyMemberList()
        {
            var latestPartyMembers = 
                _eliteApi.Party.GetPartyMembers()
                    .Where(m => m.Active == 1)
                    .ToList();

            RemoveMissingPartyMembers(latestPartyMembers);
            AddNewPartyMembers(latestPartyMembers);
            UpdatePartyMembers(latestPartyMembers);
        }

        private void UpdatePartyMembers(List<EliteAPI.PartyMember> latestPartyMembers)
        {
            foreach (var latestMember in latestPartyMembers)
            {
                Members
                    .First(member => member.Equals(latestMember))
                    .Update(latestMember);
            }
        }

        private void AddNewPartyMembers(List<EliteAPI.PartyMember> latestPartyMembers)
        {
            var existingPartyMembers = Members.ToList();
            var newPartyMembers = latestPartyMembers
                .Where(newMember =>
                    !existingPartyMembers.Exists(
                        existingMember => existingMember.Equals(newMember)
                    )
                );

            foreach (var member in newPartyMembers)
            {
                var partyMember = new PartyMember(member);
                Members.Add(partyMember);
            }
        }

        private void RemoveMissingPartyMembers(List<EliteAPI.PartyMember> latestPartyMembers)
        {
            var missingMembers = Members
                .Where(existingMember =>
                    !latestPartyMembers.Exists(existingMember.Equals));

            foreach (var missingMember in missingMembers)
            {
                Members.Remove(missingMember);
            }
        }
    }
}
