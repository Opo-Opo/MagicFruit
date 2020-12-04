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
            Update();
        }

        public void Update()
        {
            var updatedMembers = 
                _eliteApi.Party.GetPartyMembers()
                    .Where(m => m.Active == 1)
                    .ToList();

            RemoveMissingMembers(updatedMembers);
            UpdateActiveMembers(updatedMembers);
        }

        private void UpdateActiveMembers(List<EliteAPI.PartyMember> updatedMembers)
        {
            foreach (var updateMember in updatedMembers)
            {
                var existingMember = Members.FirstOrDefault(member => member.Equals(updateMember));

                if (existingMember == null)
                {
                    Members.Add(new PartyMember(updateMember));
                    continue;
                }

                existingMember.Update(updateMember);
            }
        }

        private void RemoveMissingMembers(List<EliteAPI.PartyMember> updatedMembers)
        {
            foreach (var member in Members)
            {
                if (!updatedMembers.Exists(partyMember => member.Equals(partyMember)))
                {
                    Members.Remove(member);
                }
            }
        }
    }
}
