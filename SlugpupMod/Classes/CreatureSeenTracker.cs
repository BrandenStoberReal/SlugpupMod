using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlugpupMod.Classes
{
    public class CreatureSeenTracker
    {
        public Player AssociatedPlayer { get; set; }

        public int GreenLizardSightings { get; set; } = 0;
        public int BlueLizardSightings { get; set; } = 0;
        public int CyanLizardSightings { get; set; } = 0;
        public int PinkLizardSightings { get; set; } = 0;
        public int WhiteLizardSightings { get; set; } = 0;
        public int YellowLizardSightings { get; set; } = 0;
        public int BlackLizardSightings { get; set; } = 0;
        public int RedLizardSightings { get; set; } = 0;
        public int CentipedeSightings { get; set; } = 0;
        public int DropwingSightings { get; set; } = 0;
        public int VultureSightings { get; set; } = 0;
        public int SpitLizardSightings { get; set; } = 0;
        public int EelLizardSightings { get; set; } = 0;
        public int ZoopLizardSightings { get; set; } = 0;
        public int SalemanderSightings { get; set; } = 0;
        public int ScavSightings { get; set; } = 0;

        // Class instantiation
        public CreatureSeenTracker(Player plr)
        {
            AssociatedPlayer = plr;
        }
    }
}