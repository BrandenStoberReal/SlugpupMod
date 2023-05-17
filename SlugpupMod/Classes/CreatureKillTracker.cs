using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlugpupMod.Classes
{
    public class CreatureKillTracker
    {
        Player AssociatedPlayer { get; set; }

        int GreenLizardKills { get; set; } = 0;
        int BlueLizardKills { get; set; } = 0;
        int PinkLizardKills { get; set; } = 0;
        int WhiteLizardKills { get; set; } = 0;
        int YellowLizardKills { get; set; } = 0;
        int CentipedeKills { get; set; } = 0;
        int DropwingKills { get; set; } = 0;
        int VultureKills { get; set; } = 0;

        // Class instantiation
        public CreatureKillTracker(Player plr)
        {
            AssociatedPlayer = plr;
        }
    }
}
