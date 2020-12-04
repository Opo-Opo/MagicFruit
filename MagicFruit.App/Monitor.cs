using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;
using MagicFruit.Xi;

namespace MagicFruit.App
{
    public class Monitor
    {
        private readonly Party _party;
        private readonly Player _player;

        private bool _running;

        private List<(string spell, int hp)> _cureList = new List<(string spell, int hp)>()
        {
            ("Cure VI", 2000),
            ("Cure V", 1300),
            ("Cure IV", 1100),
            ("Cure III", 600),
        };

        public Monitor(Party party, Player player)
        {
            _party = party;
            _player = player;
        }

        public void Run()
        {
            _running = true;

            while (_running)
            {
                var member = _party.Members.OrderBy(m => m.HPMissing).First();

                var (spell, _) = _cureList.First(c => member.HPMissing > c.hp);

                if (spell != null)
                {
                    _player.Cast(spell, member.Name);
                }
            }
        }

        public void Stop()
        {
            _running = false;
        }
    }
}
