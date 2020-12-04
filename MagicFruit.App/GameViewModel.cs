using System;
using System.Collections.Generic;
using EliteMMO.API;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows;
using MagicFruit.Xi;
using MagicFruit.Xi.Annotations;
using MagicFruit.Xi.Res;
using Timer = System.Timers.Timer;

namespace MagicFruit.App
{
    public class GameViewModel : INotifyPropertyChanged
    {
        private readonly Timer _instanceRefreshTimer = new Timer { Interval = 1000 };
        private readonly Timer _partyRefreshTimer = new Timer { Interval = 1000 };

        private BackgroundWorker _addonListener = new BackgroundWorker();

        public PlayerSelection PlayerSelection { get; }
            = new PlayerSelection();

        public Party Party { get; private set; }

        public GameViewModel()
        {
            UpdateInstanceList();
            _instanceRefreshTimer.Elapsed += (sender, args) => UpdateInstanceList();
            _instanceRefreshTimer.Start();

            _addonListener.DoWork += GameListener;
            _addonListener.RunWorkerCompleted += (sender, args) => _addonListener.RunWorkerAsync();
            _addonListener.RunWorkerAsync();
        }

        private void GameListener(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            var listener = new UdpClient(19766);
            var endPoint = new IPEndPoint(IPAddress.Any, 19766);

            try
            {
                while (true)
                {
                    var receive = listener.Receive(ref endPoint);

                    if (!Enum.TryParse<PackageType>(receive[0].ToString(), out var packageType)) 
                        continue;

                    if (packageType == PackageType.CharacterHealth)
                    {

                        File.AppendAllLines("packets.txt", new [] { BitConverter.ToString(receive) });
                        //
                    }
                }
            }
            finally
            {
                listener.Close();
                Thread.Sleep(TimeSpan.FromSeconds(0.3));
            }
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
