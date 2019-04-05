﻿using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;
using static Randomizer.SMZ3.Logic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.NorfairLower {

    class East : Region, Reward {

        public override string Name => "Norfair Lower East";
        public override string Area => "Norfair Lower";

        public RewardType Reward { get; set; } = RewardType.GoldenFourBoss;

        public East(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 73, 0x78F30, LocationType.Visible, "Missile (Mickey Mouse room)", Config.Logic switch {
                    Casual => new Requirement(items => true),
                    _ => items => items.Has(Morph) && items.CanDestroyBombWalls()
                }),
                new Location(this, 74, 0x78FCA, LocationType.Visible, "Missile (lower Norfair above fire flea room)"),
                new Location(this, 75, 0x78FD2, LocationType.Visible, "Power Bomb (lower Norfair above fire flea room)", Config.Logic switch {
                    Casual => new Requirement(items => true),
                    _ => items => items.CanPassBombPassages()
                }),
                new Location(this, 76, 0x790C0, LocationType.Visible, "Power Bomb (Power Bombs of shame)", Config.Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs())
                }),
                new Location(this, 77, 0x79100, LocationType.Visible, "Missile (lower Norfair near Wave Beam)", Config.Logic switch {
                    Casual => new Requirement(items => true),
                    _ => items => items.Has(Morph) && items.CanDestroyBombWalls()
                }),
                new Location(this, 78, 0x79108, LocationType.Hidden, "Energy Tank, Ridley", Config.Logic switch {
                    _ => new Requirement(items => (!Config.Keysanity || items.Has(RidleyKey)) && items.CanUsePowerBombs() && items.Has(Super))
                }),
                new Location(this, 80, 0x79184, LocationType.Visible, "Energy Tank, Firefleas")
            };
        }

        public override bool CanEnter(List<Item> items) {
            return Config.Logic switch {
                Casual =>
                    items.Has(Varia) && (
                        World.CanEnter("Norfair Upper East", items) && items.CanUsePowerBombs() && items.Has(SpaceJump) && items.Has(Gravity) ||
                        items.CanAccessNorfairLowerPortal() && items.CanDestroyBombWalls() && items.Has(Super) && items.CanUsePowerBombs() && items.CanFly()),
                _ =>
                    items.Has(Varia) && (
                        World.CanEnter("Norfair Upper East", items) && items.CanUsePowerBombs() && (items.Has(HiJump) || items.Has(Gravity)) ||
                        items.CanAccessNorfairLowerPortal() && items.CanDestroyBombWalls() && items.Has(Super) && (items.CanFly() || items.CanSpringBallJump() || items.Has(SpeedBooster))
                    ) &&
                    (items.CanFly() || items.Has(HiJump) || items.CanSpringBallJump() || items.Has(Ice) && items.Has(Charge)) &&
                    (items.CanPassBombPassages() || items.Has(ScrewAttack) && items.Has(SpaceJump)) &&
                    (items.Has(Morph) || items.HasEnergyReserves(5))
            };
        }

        public bool CanComplete(List<Item> items) {
            return Locations.Get("Energy Tank, Ridley").Available(items);
        }

    }

}
