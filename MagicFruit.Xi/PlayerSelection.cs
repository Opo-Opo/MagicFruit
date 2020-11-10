using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MagicFruit.Xi
{
    public class PlayerSelection
    {
        public ObservableCollection<Process> Processes { get; set; }
            = new ObservableCollection<Process>();

        public PlayerSelection()
        {
            UpdateProcessList();
        }

        public void UpdateProcessList()
        {
            Processes.Clear();

            foreach (var process in Process.GetProcessesByName("pol"))
            {
                Processes.Add(process);
            }
        }
    }
}
