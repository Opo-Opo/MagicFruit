using EliteMMO.API;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows;
using MagicFruit.Xi;
using MagicFruit.Xi.Annotations;

namespace MagicFruit.App
{
    public class GameViewModel : INotifyPropertyChanged
    {
        private readonly Timer _instanceRefreshTimer = new Timer { Interval = 1000 };
        private readonly Timer _partyRefreshTimer = new Timer { Interval = 1000 };

        public PlayerSelection PlayerSelection { get; }
            = new PlayerSelection();

        public Party Party { get; private set; }

        public GameViewModel()
        {
            UpdateInstanceList();
            _instanceRefreshTimer.Elapsed += (sender, args) => UpdateInstanceList();
            _instanceRefreshTimer.Start();
        }

        public void UpdateInstanceList()
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                PlayerSelection.UpdateProcessList();
            });
        }

        public void SelectInstance(Process process)
        {
            _instanceRefreshTimer.Stop();
            
            Party = new Party(new EliteAPI(process.Id));
            OnPropertyChanged(nameof(Party));

            _partyRefreshTimer.Elapsed += (sender, args) =>
                Application.Current.Dispatcher.Invoke(delegate
                {
                    Party.Update();
                });

            _partyRefreshTimer.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
