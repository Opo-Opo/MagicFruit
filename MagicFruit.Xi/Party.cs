using System.Collections.ObjectModel;
using System.Linq;
using EliteMMO.API;

namespace MagicFruit.Xi
{
    public class Party
    {
        private readonly EliteAPI _eliteApi;

        public ObservableCollection<PartyMember> Members { get; }

        public Party(EliteAPI eliteApi)
        {
            _eliteApi = eliteApi;

            // Fetch Existing Party Members
            Members = new ObservableCollection<PartyMember>(
                _eliteApi.Party.GetPartyMembers()
                    .Where(m => m.Active == 1)
                    .Select(member => new PartyMember(member))
                    .ToList()
            );
        }

        public void HealthUpdate(PartyMember member)
        {
            for (int i = 0; i < Members.Count; i++)
            {
                if (Members[i].Id == member.Id)
                {// Update Existing Party Member
                    Members[i].Update(member);
                    return;
                }
            }

            // New Party Member
            var newMemberName = _eliteApi.Entity.GetEntity((int) member.Index).Name;

            Members.Add(new PartyMember(member.Id, newMemberName).Update(member));
        }
    }
}
