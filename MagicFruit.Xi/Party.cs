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
            var latestPartyMembers = 
                _eliteApi.Party.GetPartyMembers()
                    .Where(m => m.Active == 1)
                    .ToList();

            RemoveMissingMembers(latestPartyMembers);
            UpdateActiveMembers(latestPartyMembers);
        }

        private void UpdateActiveMembers(List<EliteAPI.PartyMember> latestPartyMembers)
        {
            foreach (var latestPartyMember in latestPartyMembers)
            {
                var existing = Members.FirstOrDefault(member => member.Equals(latestPartyMember));

                if (existing == null)
                {
                    Members.Add(new PartyMember(latestPartyMember));
                    continue;
                }

                existing.Update(latestPartyMember);
            }
        }

        private void RemoveMissingMembers(List<EliteAPI.PartyMember> latestPartyMembers)
        {
            foreach (var member in Members)
            {
                if (!latestPartyMembers.Exists(partyMember => member.Equals(partyMember)))
                {
                    Members.Remove(member);
                }
            }
        }
    }
}
