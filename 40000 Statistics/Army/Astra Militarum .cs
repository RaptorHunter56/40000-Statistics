using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _40000_Statistics
{
    static class AstraMilitarum
    {
        public static List<ModelBase> Units = new List<ModelBase>()
        {
            #region Cadian Shock Troops
            new UnitBase() { Name = "Cadian Shock Troops - 10",
                Models = new Dictionary<int, ModelBase>()
                {
                    { 9, new ModelBase() { Name = "Shock Troopers", Movement = 6, Toughness = 3, ArmorSave = 5, Wounds = 1, Leadership = 7, ObjectiveControl = 2,
                        Attacks = new List<AttackBase>()
                        {
                            /*0*/ new AttackBase() { Name = "Flamer", Range = 12, AttacksExtra = "D6", Strength = 4, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Ignores_Cover | Modifiers.Torrent },
                            /*1*/ new AttackOption() { SubType = "Grenade launcher",
                                    AttacksTypes = new List<AttackBase>()
                                    {
                                        new AttackBase() { Name = "Grenade launcher – frag", Range = 24, AttacksExtra = "D3", BallisticSkill = 4, Strength = 4, ArmorPenetration = 0, Damage = 1,        Modifiers = Modifiers.Blast },
                                        new AttackBase() { Name = "Grenade launcher – krak", Range = 24, Attacks = 1,         BallisticSkill = 4, Strength = 9, ArmorPenetration = 2, DamageExtra = "D3" }
                                    }
                                  },
                            /*2*/ new AttackBase() { Name = "Meltagun", Range = 12, Attacks = 1, BallisticSkill = 4, Strength = 9, ArmorPenetration = 4, DamageExtra = "D6", Modifiers = Modifiers.Melta_2 },
                            /*3*/ new AttackBase() { Name = "Lasgun",   Range = 24, Attacks = 1, BallisticSkill = 4, Strength = 3, ArmorPenetration = 0, Damage = 1,         Modifiers = Modifiers.Rapid_Fire_1 },
                            /*4*/ new AttackOption() { SubType = "Plasma gun",
                                    AttacksTypes = new List<AttackBase>()
                                    {
                                        new AttackBase() { Name = "Plasma gun – standard",    Range = 24, Attacks = 1, BallisticSkill = 4, Strength = 7, ArmorPenetration = 2, Damage = 1, Modifiers = Modifiers.Rapid_Fire_1 },
                                        new AttackBase() { Name = "Plasma gun – supercharge", Range = 24, Attacks = 1, BallisticSkill = 4, Strength = 8, ArmorPenetration = 3, Damage = 2, Modifiers = Modifiers.Rapid_Fire_1 | Modifiers.Hazardous }
                                    }
                                  },
                            /*5*/ new AttackBase() { Name = "Close combat weapon", Attacks = 1, WeaponSkill = 4,    Strength = 3, ArmorPenetration = 0, Damage = 1 }
                        },
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(2, 0)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 0, 5 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 1, 5 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 2, 5 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 4, 5 } }
                                }

                            },
                            new AttackGroupBase(9, 7) { Attacks = new int[] { 3, 5 } }
                        }
                    } },
                    { 1, new ModelBase() { Name = "Shock Trooper Sergeant", Movement = 6, Toughness = 3, ArmorSave = 5, Wounds = 1, Leadership = 7, ObjectiveControl = 2,
                        Attacks = new List<AttackBase>()
                        {
                            /*0*/ new AttackBase() { Name = "Bolt pistol",        Range = 12, Attacks = 1, BallisticSkill = 4, Strength = 4, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Pistol },
                            /*1*/ new AttackBase() { Name = "Laspistol",          Range = 12, Attacks = 1, BallisticSkill = 4, Strength = 3, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Pistol },
                            /*2*/ new AttackBase() { Name = "Sergeant's autogun", Range = 24, Attacks = 2, BallisticSkill = 4, Strength = 3, ArmorPenetration = 0, Damage = 1 },
                            /*3*/ new AttackBase() { Name = "Chainsword",                     Attacks = 3, WeaponSkill = 4,    Strength = 3, ArmorPenetration = 0, Damage = 1 },
                            /*4*/ new AttackBase() { Name = "Close combat weapon",            Attacks = 1, WeaponSkill = 4,    Strength = 3, ArmorPenetration = 0, Damage = 1 }
                        },
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 1, 3 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 0, 3 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 2, 4 } }
                        }
                    } }
                }
            }
            #endregion
        };
    }
}
