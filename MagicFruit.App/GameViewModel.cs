using EliteMMO.API;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Windows;
using MagicFruit.Xi;

namespace MagicFruit.App
{
    public class GameViewModel
    {
        private EliteAPI _eliteApi;
        private readonly Timer _instanceRefreshTimer = new Timer { Interval = 5000 };
        private readonly Timer _partyRefreshTimer = new Timer { Interval = 5000 };

        public GameViewModel()
        {
            Instances = new ObservableCollection<Process>();
            PartyMembers = new ObservableCollection<PartyMember>();

            UpdateInstanceList();
            _instanceRefreshTimer.Elapsed += (sender, args) => UpdateInstanceList();
            _instanceRefreshTimer.Start();
        }

        public void UpdateInstanceList()
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                Instances.Clear();

                foreach (var process in Process.GetProcessesByName("pol"))
                {
                    Instances.Add(process);
                }
            });
        }

        public void SelectInstance(Process process)
        {
            _instanceRefreshTimer.Stop();
            
            _eliteApi = new EliteAPI(process.Id);

            UpdatePartyMemberList();
            _partyRefreshTimer.Elapsed += (sender, args) => UpdatePartyMemberList();
            _partyRefreshTimer.Start();
        }

        public void UpdatePartyMemberList()
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                var members = _eliteApi.Party.GetPartyMembers().Where(m => m.Active == 1).ToList();
                PartyMembers.Clear();

                foreach (var member in members)
                {
                    PartyMembers.Add(new PartyMember(member));
                }
            });
        }

        public ObservableCollection<Process> Instances { get; set; }

        public ObservableCollection<PartyMember> PartyMembers { get; set; }
    }
}
