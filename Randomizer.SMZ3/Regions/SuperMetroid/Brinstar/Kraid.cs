﻿using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.SuperMetroid.Brinstar {

    class Kraid : Region, Reward {

        public override string Name => "Brinstar Kraid";
        public override string Area => "Brinstar";

        public RewardType Reward { get; set; } = RewardType.GoldenFourBoss;

        public Kraid(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 43, 0x7899C, LocationType.Hidden, "Energy Tank, Kraid",
                    items => !Config.Keysanity || items.Has(KraidKey)),
                new Location(this, 48, 0x78ACA, LocationType.Chozo, "Varia Suit",
                    items => !Config.Keysanity || items.Has(KraidKey)),
                new Location(this, 44, 0x789EC, LocationType.Hidden, "Missile (Kraid)", Config.Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs())
                }),
            };
        }

        public override bool CanEnter(List<Item> items) {
            return (items.CanDestroyBombWalls() || items.Has(SpeedBooster) || items.CanAccessNorfairUpperPortal()) &&
                items.Has(Super) && items.CanPassBombPassages();
        }

        public bool CanComplete(List<Item> items) {
            return Locations.Get("Varia Suit").Available(items);
        }

    }

}
