using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using System.Security.Permissions;
using MoreSlugcats;
using RWCustom;
using IL;
using On;

[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]

namespace BetterSlugPups
{
    [BepInPlugin("brandenstober.better_slugpuppies", "Better Slugpups", "0.1.1")] // (GUID, mod name, mod version)
    public class BetterSlugPups : BaseUnityPlugin
    {
        // Functions

        /// <summary>
        /// Gets the owner of a slugpup
        /// </summary>
        /// <param name="slugpuppy"></param>
        /// <returns></returns>
        private Player GetPlayerFromSlugpup(AbstractCreature slugpuppy)
        {
            return slugpuppy.abstractAI.parent.realizedCreature as Player;
        }

        /// <summary>
        /// Checks if a given character is a slugpup
        /// </summary>
        /// <param name="creature"></param>
        /// <returns></returns>
        private bool IsCharacterSlugpup(AbstractCreature creature)
        {
            return (creature != null && creature.creatureTemplate.type == MoreSlugcatsEnums.CreatureTemplateType.SlugNPC); // What the fuck?
        }

        /// <summary>
        /// Checks if a creature is friends with a given lizard
        /// </summary>
        /// <param name="lizard"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        private bool IsLizardFriends(LizardAI lizard, Creature entity)
        {
            return (lizard.friendTracker.friend == entity);
        }

        /// <summary>
        /// Basic RNG generator for "random" events
        /// </summary>
        /// <param name="probability"></param>
        /// <returns>A bool determining whether the probability succeeded</returns>
        private bool RandomChance(double probability)
        {
            Random rng = new Random();
            if (rng.NextDouble() < probability)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Begin hooks here
        public void OnEnable()
        {
            On.TempleGuardAI.ThrowOutScore += ThrowOutScoreHook;
            //On.LizardAI.DoIWantToBiteThisCreature += BiteCreatureHook;
            On.LizardAI.Update += ModifiedLizardUpdate;
            On.Player.Jump += PlayerJump;
        }

        // 1st argument is a reference to the original function, 2nd argument is a reference to the parent class calling the function, and anything after is the base function's arguments
        // The function must return the same type as the original function
        private float ThrowOutScoreHook(On.TempleGuardAI.orig_ThrowOutScore orig, TempleGuardAI self, Tracker.CreatureRepresentation creature)
        {
            // Check if the targeted creature is a Slugpup
            if (IsCharacterSlugpup(creature.representedCreature))
            {
                // Check player's Karma
                if ((self.guard.room.game.session as StoryGameSession).saveState.deathPersistentSaveData.karma >= 9)
                {
                    // Make Guardians not aggressive if karma is high enough
                    return 0f;
                }
            }
            // Original function here
            return orig(self, creature); // Return value of original game's code
        }

        /*
        bool BiteCreatureHook(On.LizardAI.orig_DoIWantToBiteThisCreature orig, LizardAI self, Tracker.CreatureRepresentation creature)
        {
            // If it is a slugpup
            if (IsCharacterSlugpup(creature.representedCreature))
            {
                // Why does this game mode exist
                if (ModManager.CoopAvailable && Custom.rainWorld.options.friendlyLizards)
                {
                    bool playersFriendly = false; // Will turn true if any player is the lizard's friend

                    // Loop through all alive players
                    foreach (AbstractCreature checkCrit in self.lizard.abstractCreature.world.game.NonPermaDeadPlayers)
                    {
                        // If the player is the lizard's friend, break the loop and set the variable
                        if (self.friendTracker.friend == checkCrit.realizedCreature) {
                            playersFriendly = true;
                            break;
                        }
                    }
                    // If any of the players are a lizard's friend, dont eat the child
                    if (playersFriendly)
                    {
                        return false;
                    }
                }

                // If the parent is a player
                if (creature.representedCreature.abstractAI.parent.realizedCreature is Player)
                {
                    // Get player with my cool function
                    Player player = GetPlayerFromSlugpup(creature.representedCreature);

                    // If the lizard is friends with our player, don't eat his child. He will eat other children tho.
                    if (IsLizardFriends(self, player))
                    {
                        return false;
                    }
                }
            }
            return orig(self, creature);
        }
        */

        private void ModifiedLizardUpdate(On.LizardAI.orig_Update orig, LizardAI self)
        {
            // Loop through all the creature's prey
            foreach (PreyTracker.TrackedPrey prey in self.preyTracker.prey)
            {
                if (IsCharacterSlugpup(prey.critRep.representedCreature))
                {
                    Player owner = GetPlayerFromSlugpup(prey.critRep.representedCreature);

                    if (IsLizardFriends(self, owner))
                    {
                        self.preyTracker.ForgetPrey(prey.critRep.representedCreature);
                        self.preyTracker.Update(); // Apparently forgetprey doesn't run this on its own?
                    }
                }
            }
            orig(self);
        }

        private void PlayerJump(On.Player.orig_Jump orig, Player self)
        {
            // 0.05% chance to get food by jumping if your karma is maxed out. Why? Because good deeds should be rewarded.
            if (RandomChance(0.0005) && (self.room.game.session as StoryGameSession).saveState.deathPersistentSaveData.karma >= 9)
            {
                self.AddFood(1);
            }

            // 0.05% chance to turn the current room into a snowy blizzard when you jump if your karma is the lowest possible.
            if (RandomChance(0.0005) && (self.room.game.session as StoryGameSession).saveState.deathPersistentSaveData.karma <= 1)
            {
                if (self.room.IsGateRoom() == false && self.room.abstractRoom.isAncientShelter == false && self.room.blizzard == false)
                {
                    self.room.AddSnow();
                    self.room.blizzard = true;
                    self.room.abstractRoom.AddEntity(new AbstractWorldEntity(self.room.game.world, self.room.abstractRoom.RandomNodeInRoom(), EntityID.FromString("WhiteLizard"))); // Spawns a white lizard
                }
            }
            orig(self);
        }
    }
}