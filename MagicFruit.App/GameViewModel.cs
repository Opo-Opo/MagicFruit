using EliteMMO.API;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using MagicFruit.Xi;
using MagicFruit.Xi.Annotations;
using Timer = System.Timers.Timer;

namespace MagicFruit.App
{
    public class GameViewModel : INotifyPropertyChanged
    {
        private readonly Timer _instanceRefreshTimer = new Timer { Interval = 1000 };

        public PlayerSelection PlayerSelection { get; }
            = new PlayerSelection();

        private Listener _listener;
        public Party Party { get; private set; }

        public GameViewModel()
        {
            UpdateInstanceList();
            _instanceRefreshTimer.Elapsed += (sender, args) => UpdateInstanceList();
            _instanceRefreshTimer.Start();

            _listener = new Listener(19766);
        }

        public void UpdateInstanceList()
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                PlayerSelection.UpdateProcessList();
            });
        }

        public async void SelectInstance(Process process)
        {
            _instanceRefreshTimer.Stop();
            
            Party = new Party(new EliteAPI(process.Id));
            _listener.HealthUpdate += (PartyMember member) => Application.Current.Dispatcher.Invoke(() => Party.HealthUpdate(member));
            OnPropertyChanged(nameof(Party));

            await _listener.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
