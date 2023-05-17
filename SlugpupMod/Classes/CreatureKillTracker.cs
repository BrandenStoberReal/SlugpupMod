using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlugpupMod.Classes
{
    public class CreatureKillTracker
    {
        public Player AssociatedPlayer { get; set; }

        public int GreenLizardKills { get; set; } = 0;
        public int BlueLizardKills { get; set; } = 0;
        public int CyanLizardKills { get; set; } = 0;
        public int PinkLizardKills { get; set; } = 0;
        public int WhiteLizardKills { get; set; } = 0;
        public int YellowLizardKills { get; set; } = 0;
        public int BlackLizardKills { get; set; } = 0;
        public int RedLizardKills { get; set; } = 0;
        public int CentipedeKills { get; set; } = 0;
        public int DropwingKills { get; set; } = 0;
        public int VultureKills { get; set; } = 0;
        public int SpitLizardKills { get; set; } = 0;
        public int EelLizardKills { get; set; } = 0;
        public int ZoopLizardKills { get; set; } = 0;
        public int SalemanderKills { get; set; } = 0;
        public int ScavKills { get; set; } = 0;

        // Class instantiation
        public CreatureKillTracker(Player plr)
        {
            AssociatedPlayer = plr;
        }
    }
}