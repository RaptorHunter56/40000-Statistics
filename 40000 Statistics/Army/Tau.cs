using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _40000_Statistics
{
    static class Tau
    {
        public static Dictionary<string, int> Point = new Dictionary<string, int>()
        {
            // Epic Hero
            {"Commander Farsight",               95},
            {"Commander Shadowsun",              100},
            {"Darkstrider",                      60},
            // Characters
            {"Cadre Fireblade",                  50},
            {"Commander in Coldstar Battlesuit", 95},
            {"Commander in Enforcer Battlesuit", 80},
            {"Ethereal",                         50},
            {"Firesight Team",                   70},
            {"Kroot Flesh Shaper",               45},
            {"Kroot Lone-Spear",                 80},
            {"Kroot Trail Shaper",               55},
            {"Kroot War Shaper",                 50},
            // Battleline
            {"Breacher Team",                    100},
            {"Strike Team",                      75},
            // Infantry
            {"Kroot Carnivores - 10",            65},
            {"Kroot Carnivores - 20",            130},
            {"Kroot Farstalkers",                85},
            {"Pathfinder Team",                  90},
            {"Stealth Battlesuits - 3",          60},
            {"Stealth Battlesuits - 5",          60},
            {"Vespid Stingwings - 5",            65},
            {"Vespid Stingwings - 10",           65},
        };
        public static List<ModelBase> Units = new List<ModelBase>()
        {
            #region Epic Hero
            new ModelBase() { ModelNo = 1, Name = "Commander Farsight", Movement = 10, Toughness = 5, ArmorSave = 2, InvulnerableSave = 4, Wounds = 8, Leadership = 6, ObjectiveControl = 2,
                Keywords = ModelBase.CreateKeywordList(Keywords.Vehicle, Keywords.Walker, Keywords.Fly, Keywords.Character, Keywords.Epic_Hero, Keywords.Battlesuit, "Commander Farsight"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "High-intensity plasma rifle", Range = 24, Attacks = 2, BallisticSkill = 2, Strength = 8,  ArmorPenetration = 3, Damage = 3 },
                    /*1*/ new AttackOption() { SubType = "Dawn Blade",
                            AttacksTypes = new List<AttackBase>()
                            {
                                new AttackBase() { Name = "Dawn Blade – strike", Attacks = 4, WeaponSkill = 2, Strength = 10, ArmorPenetration = 2, Damage = 3 },
                                new AttackBase() { Name = "Dawn Blade – sweep",  Attacks = 8, WeaponSkill = 2, Strength = 6,  ArmorPenetration = 1, Damage = 1 }
                            }
                          }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackGroupBase(1) { Attacks = new int[] { 0, 1 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Commander Shadowsun", Movement = 10, Toughness = 4, ArmorSave = 3, InvulnerableSave = 5, Wounds = 6, Leadership = 6, ObjectiveControl = 1,
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Fly, Keywords.Character, Keywords.Epic_Hero, Keywords.Battlesuit, "Commander Shadowsun"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Flechette launcher",         Range = 18, Attacks = 5, BallisticSkill = 2, Strength = 3,  ArmorPenetration = 3, Damage = 3 },
                    /*1*/ new AttackBase() { Name = "High-energy fusion blaster", Range = 18, Attacks = 1, BallisticSkill = 2, Strength = 10, ArmorPenetration = 3, Damage = 3, Modifiers = Modifiers.Melta_2 },
                    /*2*/ new AttackBase() { Name = "Light missile pod",          Range = 24, Attacks = 2, BallisticSkill = 2, Strength = 7,  ArmorPenetration = 3, Damage = 3 },
                    /*3*/ new AttackBase() { Name = "Pulse pistol",               Range = 12, Attacks = 1, BallisticSkill = 3, Strength = 5,  ArmorPenetration = 3, Damage = 3, Modifiers = Modifiers.Pistol },
                    /*4*/ new AttackBase() { Name = "Battlesuit fists",                       Attacks = 3, WeaponSkill = 4,    Strength = 5,  ArmorPenetration = 0, Damage = 1 }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackGroupBase(1) { Attacks = new int[] { 0, 1, 1, 2, 3, 4 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Darkstrider", Movement = 7, Toughness = 3, ArmorSave = 4, Wounds = 3, Leadership = 7, ObjectiveControl = 1,
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Character, Keywords.Epic_Hero, Keywords.Markerlight, "Darkstrider"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Shade",               Range = 18, Attacks = 2, BallisticSkill = 2, Strength = 5, ArmorPenetration = 0, Damage = 2 },
                    /*1*/ new AttackBase() { Name = "Close combat weapon",             Attacks = 3, WeaponSkill = 4,    Strength = 3, ArmorPenetration = 0, Damage = 1 }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackGroupBase(1) { Attacks = new int[] { 0, 1 } }
                }
            },
            #endregion
            #region Characters
            new ModelBase(){ ModelNo = 1, Name = "Cadre Fireblade", Movement = 6, Toughness = 3, ArmorSave = 4, Wounds = 3, Leadership = 7, ObjectiveControl = 1,
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Character, Keywords.Grenades, "Cadre Fireblade"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Fireblade pulse rifle", Range = 30, Attacks = 1, BallisticSkill = 3, Strength = 5, ArmorPenetration = 0, Damage = 2, Modifiers = Modifiers.Rapid_Fire_1 },
                    /*1*/ new AttackBase() { Name = "Close combat weapon",               Attacks = 3, WeaponSkill = 4,    Strength = 3, ArmorPenetration = 0, Damage = 1 },
                    /*2*/ new AttackBase() { Name = "Twin pulse carbine",    Range = 20, Attacks = 2, BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Assault | Modifiers.Twin_Linked }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackGroupBase(1) { Attacks = new int[] { 0, 1 } },
                    new WargearGroupBase(1, 0) { Attacks = new int[] { 2 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Commander in Coldstar Battlesuit", Movement = 12, Toughness = 5, ArmorSave = 3, Wounds = 6, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                Keywords = ModelBase.CreateKeywordList(Keywords.Vehicle, Keywords.Walker, Keywords.Fly, Keywords.Character, Keywords.Battlesuit, "Commander In Coldstar Battlesuit"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() {Name = "Airbursting fragmentation projector", Range = 24, AttacksExtra = "D6", BallisticSkill = 3, Strength = 3, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Blast | Modifiers.Indirect_Fire},
                    /*1*/ new AttackBase() {Name = "Burst cannon",                        Range = 18, Attacks = 4,         BallisticSkill = 3, Strength = 5, ArmorPenetration = 0, Damage = 1 },
                    /*2*/ new AttackOption() { SubType = "Cyclic ion blaster",
                            AttacksTypes = new List<AttackBase>()
                            {
                                new AttackBase() { Name = "Cyclic ion blaster – standard",   Range = 18, Attacks = 3, BallisticSkill = 3, Strength = 7, ArmorPenetration = 1, Damage = 1 },
                                new AttackBase() { Name = "Cyclic ion blaster – overcharge", Range = 18, Attacks = 3, BallisticSkill = 3, Strength = 8, ArmorPenetration = 2, Damage = 2, Modifiers = Modifiers.Hazardous }
                            }
                          },
                    /*3*/ new AttackBase() {Name = "Fusion blaster",           Range = 12, Attacks = 1,         BallisticSkill = 3, Strength = 9, ArmorPenetration = 4, DamageExtra = "D6", Modifiers = Modifiers.Melta_2 },
                    /*4*/ new AttackBase() {Name = "High-output burst cannon", Range = 18, Attacks = 8,         BallisticSkill = 3, Strength = 5, ArmorPenetration = 0, Damage = 1 },
                    /*5*/ new AttackBase() {Name = "Missile pod",              Range = 30, Attacks = 2,         BallisticSkill = 3, Strength = 7, ArmorPenetration = 1, Damage = 2 },
                    /*6*/ new AttackBase() {Name = "Plasma rifle",             Range = 18, Attacks = 1,         BallisticSkill = 3, Strength = 8, ArmorPenetration = 3, Damage = 3 },
                    /*7*/ new AttackBase() {Name = "T’au flamer",              Range = 12, AttacksExtra = "D6",                     Strength = 4, ArmorPenetration = 0, Damage = 1,         Modifiers = Modifiers.Ignores_Cover | Modifiers.Torrent },
                    /*8*/ new AttackBase() {Name = "Battlesuit fists",                     Attacks = 3,         WeaponSkill = 4,    Strength = 5, ArmorPenetration = 0, Damage = 1 },
                    /*9*/ new AttackBase() {Name = "Twin pulse carbine",       Range = 20, Attacks = 2,         BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1,         Modifiers = Modifiers.Assault | Modifiers.Twin_Linked }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackOptionGroupBase(5, 1)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 0 } },
                            new AttackGroupBase(4, 0) { Attacks = new int[] { 1 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 2 } },
                            new AttackGroupBase(4, 0) { Attacks = new int[] { 3 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 4 } },
                            new AttackGroupBase(4, 0) { Attacks = new int[] { 5 } },
                            new AttackGroupBase(4, 0) { Attacks = new int[] { 6 } },
                            new AttackGroupBase(4, 0) { Attacks = new int[] { 7 } }
                        }
                    },
                    new AttackGroupBase(1) { Attacks = new int[] { 8 } },
                    new WargearGroupBase(2, 0) { Attacks = new int[] { 9 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Commander in Enforcer Battlesuit", Movement = 8, Toughness = 5, ArmorSave = 2, Wounds = 6, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                Keywords = ModelBase.CreateKeywordList(Keywords.Vehicle, Keywords.Walker, Keywords.Fly, Keywords.Character, Keywords.Battlesuit, "Commander In Enforcer Battlesuit"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() {Name = "Airbursting fragmentation projector", Range = 24, AttacksExtra = "D6", BallisticSkill = 3, Strength = 3, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Blast | Modifiers.Indirect_Fire},
                    /*1*/ new AttackBase() {Name = "Burst cannon",                        Range = 18, Attacks = 4,         BallisticSkill = 3, Strength = 5, ArmorPenetration = 0, Damage = 1 },
                    /*2*/ new AttackOption() { SubType = "Cyclic ion blaster",
                            AttacksTypes = new List<AttackBase>()
                            {
                                new AttackBase() { Name = "Cyclic ion blaster – standard",   Range = 18, Attacks = 3, BallisticSkill = 3, Strength = 7, ArmorPenetration = 1, Damage = 1 },
                                new AttackBase() { Name = "Cyclic ion blaster – overcharge", Range = 18, Attacks = 3, BallisticSkill = 3, Strength = 8, ArmorPenetration = 2, Damage = 2, Modifiers = Modifiers.Hazardous }
                            }
                          },
                    /*3*/ new AttackBase() {Name = "Fusion blaster",           Range = 12, Attacks = 1,         BallisticSkill = 3, Strength = 9, ArmorPenetration = 4, DamageExtra = "D6", Modifiers = Modifiers.Melta_2 },
                    /*4*/ new AttackBase() {Name = "Missile pod",              Range = 30, Attacks = 2,         BallisticSkill = 3, Strength = 7, ArmorPenetration = 1, Damage = 2 },
                    /*5*/ new AttackBase() {Name = "Plasma rifle",             Range = 18, Attacks = 1,         BallisticSkill = 3, Strength = 8, ArmorPenetration = 3, Damage = 3 },
                    /*6*/ new AttackBase() {Name = "T’au flamer",              Range = 12, AttacksExtra = "D6",                     Strength = 4, ArmorPenetration = 0, Damage = 1,         Modifiers = Modifiers.Ignores_Cover | Modifiers.Torrent },
                    /*7*/ new AttackBase() {Name = "Battlesuit fists",                     Attacks = 3,         WeaponSkill = 4,    Strength = 5, ArmorPenetration = 0, Damage = 1 },
                    /*8*/ new AttackBase() {Name = "Twin pulse carbine",       Range = 20, Attacks = 2,         BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1,         Modifiers = Modifiers.Assault | Modifiers.Twin_Linked }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackOptionGroupBase(4, 0)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 0 } },
                            new AttackGroupBase(4, 0) { Attacks = new int[] { 1 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 2 } },
                            new AttackGroupBase(4, 0) { Attacks = new int[] { 3 } },
                            new AttackGroupBase(4, 0) { Attacks = new int[] { 4 } },
                            new AttackGroupBase(4, 0) { Attacks = new int[] { 5 } },
                            new AttackGroupBase(4, 0) { Attacks = new int[] { 6 } }
                        }
                    },
                    new AttackGroupBase(1) { Attacks = new int[] { 7 } },
                    new WargearGroupBase(2, 0) { Attacks = new int[] { 8 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Ethereal", Movement = 6, Toughness = 3, ArmorSave = 5, InvulnerableSave = 5, Wounds = 3, Leadership = 6, ObjectiveControl = 1,
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Character, "Ethereal"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Honour stave",                   Attacks = 2, WeaponSkill = 4,    Strength = 5, ArmorPenetration = 0, Damage = 1},
                    /*1*/ new AttackBase() { Name = "Twin pulse carbine", Range = 20, Attacks = 2, BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Assault | Modifiers.Twin_Linked }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackGroupBase(1) { Attacks = new int[] { 0 } },
                    new WargearGroupBase(2, 0) { Attacks = new int[] { 1 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Firesight Team", Movement = 6, Toughness = 3, ArmorSave = 4, Wounds = 4, Leadership = 7, ObjectiveControl = 3,
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Character, Keywords.Markerlight, "Firesight Team"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Longshot pulse rifles", Range = 36, Attacks = 3, BallisticSkill = 4, Strength = 5, ArmorPenetration = 1, Damage = 2, Modifiers = Modifiers.Heavy | Modifiers.Precision },
                    /*1*/ new AttackBase() { Name = "Pulse pistol",          Range = 12, Attacks = 1, BallisticSkill = 3, Strength = 5, ArmorPenetration = 0, Damage = 1 },
                    /*2*/ new AttackBase() { Name = "Close combat weapons",              Attacks = 4, WeaponSkill = 5,    Strength = 3, ArmorPenetration = 0, Damage = 1}
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackGroupBase(1) { Attacks = new int[] { 0, 1, 2 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Kroot Flesh Shaper", Movement = 7, Toughness = 3, ArmorSave = 6, Wounds = 3, Leadership = 7, ObjectiveControl = 1,
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Character, Keywords.Kroot, Keywords.Shaper, "Flesh Shaper"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Kroot scattergun", Range = 12, Attacks = 2, BallisticSkill = 4, Strength = 4, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Assault },
                    /*1*/ new AttackBase() { Name = "Twin ritualistic blades",      Attacks = 4, WeaponSkill = 2,    Strength = 5, ArmorPenetration = 1, Damage = 1, Modifiers = Modifiers.Twin_Linked }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackGroupBase(1) { Attacks = new int[] { 0, 1 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Kroot Lone-Spear", Movement = 12, Toughness = 5, ArmorSave = 5, Wounds = 6, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                Keywords = ModelBase.CreateKeywordList(Keywords.Mounted, Keywords.Character, Keywords.Kroot, "Lone-Spear"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Blast javelin",  Range = 18, AttacksExtra = "D6", BallisticSkill = 4, Strength = 10, ArmorPenetration = 2, Damage = 2, Modifiers = Modifiers.Assault | Modifiers.Blast },
                    /*1*/ new AttackBase() { Name = "Kroot long gun", Range = 36, Attacks = 1,         BallisticSkill = 3, Strength = 6,  ArmorPenetration = 2, Damage = 3, Modifiers = Modifiers.Heavy | Modifiers.Precision },
                    /*2*/ new AttackBase() { Name = "Close combat weapon",        Attacks = 3,         WeaponSkill = 3,    Strength = 4,  ArmorPenetration = 1, Damage = 1 },
                    /*3*/ new AttackBase() { Name = "Hunting javelin",            Attacks = 3,         WeaponSkill = 3,    Strength = 4,  ArmorPenetration = 1, Damage = 1, Modifiers = Modifiers.Lance },
                    /*4*/ new AttackBase() { Name = "Kalamandra’s bite",          Attacks = 4,         WeaponSkill = 4,    Strength = 5,  ArmorPenetration = 1, Damage = 1, Modifiers = Modifiers.Extra_Attacks }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackOptionGroupBase(1)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 1 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 0, 3 } }
                        }
                    },
                    new AttackGroupBase(1) { Attacks = new int[] { 2, 4 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Kroot Trail Shaper", Movement = 7, Toughness = 3, ArmorSave = 6, Wounds = 3, Leadership = 7, ObjectiveControl = 1,
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Character, Keywords.Kroot, Keywords.Shaper, "Trail Shaper"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Kroot rifle", Range = 24, Attacks = 1, BallisticSkill = 4, Strength = 4, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Rapid_Fire_1 },
                    /*1*/ new AttackBase() { Name = "Shaper’s blade",          Attacks = 4, WeaponSkill = 2,    Strength = 5, ArmorPenetration = 1, Damage = 1}
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackGroupBase(1) { Attacks = new int[] { 0, 1 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Kroot War Shaper", Movement = 7, Toughness = 3, ArmorSave = 6, Wounds = 3, Leadership = 7, ObjectiveControl = 1, ComplexAttacks = true,
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Character, Keywords.Kroot, Keywords.Shaper, "War Shaper"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Dart-bow and tri-blade", Range = 24, AttacksExtra = "D3+1", BallisticSkill = 4, Strength = 4, ArmorPenetration = 0, Damage = 2, Modifiers = Modifiers.Anti_Infantry_3 | Modifiers.Assault| Modifiers.Heavy },
                    /*1*/ new AttackBase() { Name = "Kroot pistol",           Range = 12, Attacks = 1,           BallisticSkill = 4, Strength = 4, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Pistol },
                    /*2*/ new AttackBase() { Name = "Bladestave and prey-hook",           Attacks = 4,           WeaponSkill = 2,    Strength = 5, ArmorPenetration = 1, Damage = 2, Modifiers = Modifiers.Lethal_Hits },
                    /*3*/ new AttackBase() { Name = "Shaper’s blade",                     Attacks = 4,           WeaponSkill = 2,    Strength = 5, ArmorPenetration = 0, Damage = 1 }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackOptionGroupBase(1)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 0 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 2 } }
                        }
                    },
                    new AttackGroupBase(1) { Attacks = new int[] { 1, 3 } }
                }
            },
            #endregion
            #region Battleline
            new UnitBase() { Name = "Breacher Team",
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Battleline, Keywords.Grenades, Keywords.Markerlight, Keywords.Fire_Warrior, "Breacher Team"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Pulse blaster",      Range = 10, Attacks = 2, BallisticSkill = 3, Strength = 6, ArmorPenetration = 1, Damage = 1, Modifiers = Modifiers.Assault },
                    /*1*/ new AttackBase() { Name = "Pulse pistol",       Range = 12, Attacks = 1, BallisticSkill = 4, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Pistol },
                    /*2*/ new AttackBase() { Name = "Support turret",     Range = 30, Attacks = 2, BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Indirect_Fire | Modifiers.Twin_Linked },
                    /*3*/ new AttackBase() { Name = "Twin pulse carbine", Range = 20, Attacks = 2, BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Assault | Modifiers.Twin_Linked },
                    /*4*/ new AttackBase() { Name = "Close combat weapon",            Attacks = 1, WeaponSkill = 5,    Strength = 3, ArmorPenetration = 0, Damage = 1 }
                },
                Models = new Dictionary<int, ModelBase>()
                {
                    { 9, new ModelBase() { Name = "Breacher Fire Warriors", Movement = 6, Toughness = 3, ArmorSave = 4, Wounds = 1, Leadership = 7, ObjectiveControl = 2,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(9) { Attacks = new int[] { 0, 1, 4 } }
                        }
                    } },
                    { 1, new ModelBase() { Name = "Breacher Fire Warrior Shas’ui", Movement = 6, Toughness = 3, ArmorSave = 4, Wounds = 1, Leadership = 7, ObjectiveControl = 2,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1) { Attacks = new int[] { 0, 1, 4 } },
                            new WargearGroupBase(2, 0) { Attacks = new int[] { 3 } }
                        }
                    } }
                }
            },
            new UnitBase() { Name = "Strike Team",
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Battleline, Keywords.Grenades, Keywords.Markerlight, Keywords.Fire_Warrior, "Breacher Team"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Pulse carbine",      Range = 20, Attacks = 2, BallisticSkill = 4, Strength = 5, ArmorPenetration = 0, Damage = 1 },
                    /*1*/ new AttackBase() { Name = "Pulse pistol",       Range = 12, Attacks = 1, BallisticSkill = 4, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Pistol },
                    /*2*/ new AttackBase() { Name = "Pulse rifle",        Range = 30, Attacks = 1, BallisticSkill = 4, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Rapid_Fire_1 },
                    /*3*/ new AttackBase() { Name = "Support turret",     Range = 30, Attacks = 2, BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Indirect_Fire | Modifiers.Twin_Linked },
                    /*4*/ new AttackBase() { Name = "Twin pulse carbine", Range = 20, Attacks = 2, BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Assault | Modifiers.Twin_Linked },
                    /*5*/ new AttackBase() { Name = "Close combat weapon",            Attacks = 1, WeaponSkill = 5,    Strength = 3, ArmorPenetration = 0, Damage = 1 }
                },
                Models = new Dictionary<int, ModelBase>()
                {
                    { 9, new ModelBase() { Name = "Fire Warriors", Movement = 6, Toughness = 3, ArmorSave = 4, Wounds = 1, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(9)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(9, 0) { Attacks = new int[] { 0 } },
                                    new AttackGroupBase(9, 0) { Attacks = new int[] { 2 } }
                                }
                            },
                            new AttackGroupBase(9) { Attacks = new int[] { 1, 5 } }
                        }
                    } },
                    { 1, new ModelBase() { Name = "Fire Warrior Shas’ui", Movement = 6, Toughness = 3, ArmorSave = 4, Wounds = 1, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(1)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 0 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 2 } }
                                }
                            },
                            new AttackGroupBase(1) { Attacks = new int[] { 1, 5 } },
                            new WargearGroupBase(2, 0) { Attacks = new int[] { 3 } }
                        }
                    } }
                }
            },
            #endregion
            #region Infantry
            new UnitBase() { Name = "Kroot Carnivores - 10",
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Grenades, Keywords.Kroot, "Carnivores"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Tanglebomb launcher", Range = 24, AttacksExtra = "D3", BallisticSkill = 4, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Blast },
                    /*1*/ new AttackBase() { Name = "Kroot pistol",        Range = 12, Attacks = 1,         BallisticSkill = 4, Strength = 4, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Pistol },
                    /*2*/ new AttackBase() { Name = "Kroot rifle",         Range = 24, Attacks = 1,         BallisticSkill = 4, Strength = 4, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Rapid_Fire_1 },
                    /*3*/ new AttackBase() { Name = "Kroot carbine",       Range = 18, Attacks = 1,         BallisticSkill = 4, Strength = 4, ArmorPenetration = 0, Damage = 2 },
                    /*4*/ new AttackBase() { Name = "Close combat weapon",             Attacks = 2,         WeaponSkill = 3,    Strength = 4, ArmorPenetration = 0, Damage = 1 }
                },
                Models = new Dictionary<int, ModelBase>()
                {
                    { 9, new ModelBase() { Name = "Kroot Carnivores", Movement = 7, Toughness = 3, ArmorSave = 6, Wounds = 1, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(9)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(9, 8) { Attacks = new int[] { 2 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 0 } }
                                }
                            },
                            new AttackGroupBase(9) { Attacks = new int[] { 4 } }
                        }
                    } },
                    { 1, new ModelBase() { Name = "Long-quill", Movement = 7, Toughness = 3, ArmorSave = 6, Wounds = 1, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(1)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 2 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 3 } }
                                }
                            },
                            new AttackGroupBase(1) { Attacks = new int[] { 1, 4 } }
                        }
                    } }
                }
            },
            new UnitBase() { Name = "Kroot Carnivores - 20",
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Grenades, Keywords.Kroot, "Carnivores"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Tanglebomb launcher", Range = 24, AttacksExtra = "D3", BallisticSkill = 4, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Blast },
                    /*1*/ new AttackBase() { Name = "Kroot pistol",        Range = 12, Attacks = 1,         BallisticSkill = 4, Strength = 4, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Pistol },
                    /*2*/ new AttackBase() { Name = "Kroot rifle",         Range = 24, Attacks = 1,         BallisticSkill = 4, Strength = 4, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Rapid_Fire_1 },
                    /*3*/ new AttackBase() { Name = "Kroot carbine",       Range = 18, Attacks = 1,         BallisticSkill = 4, Strength = 4, ArmorPenetration = 0, Damage = 2 },
                    /*4*/ new AttackBase() { Name = "Close combat weapon",             Attacks = 2,         WeaponSkill = 3,    Strength = 4, ArmorPenetration = 0, Damage = 1 }
                },
                Models = new Dictionary<int, ModelBase>()
                {
                    { 19, new ModelBase() { Name = "Kroot Carnivores", Movement = 7, Toughness = 3, ArmorSave = 6, Wounds = 1, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(19)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(19, 17) { Attacks = new int[] { 2 } },
                                    new AttackGroupBase(2, 0) { Attacks = new int[] { 0 } }
                                }
                            },
                            new AttackGroupBase(19) { Attacks = new int[] { 4 } }
                        }
                    } },
                    { 1, new ModelBase() { Name = "Long-quill", Movement = 7, Toughness = 3, ArmorSave = 6, Wounds = 1, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(1)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 2 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 3 } }
                                }
                            },
                            new AttackGroupBase(1) { Attacks = new int[] { 1, 4 } }
                        }
                    } }
                }
            },
            new UnitBase() { Name = "Kroot Farstalkers",
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Kroot, Keywords.Grenades, "Farstalkers"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Dvorgite skinner",   Range = 12, AttacksExtra = "D6",             Strength = 4, ArmorPenetration = 1, Damage = 1, Modifiers = Modifiers.Ignores_Cover | Modifiers.Torrent },
                    /*1*/ new AttackBase() { Name = "Farstalker firearm", Range = 24, Attacks = 1, BallisticSkill = 4, Strength = 4, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Rapid_Fire_1 },
                    /*2*/ new AttackBase() { Name = "Kroot pistol",       Range = 12, Attacks = 1, BallisticSkill = 4, Strength = 4, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Pistol },
                    /*3*/ new AttackBase() { Name = "Londaxi tribalest",  Range = 18, Attacks = 3, BallisticSkill = 5, Strength = 7, ArmorPenetration = 1, Damage = 1, Modifiers = Modifiers.Anti_Vehicle_4 | Modifiers.Devastating_Wounds | Modifiers.Heavy },
                    /*4*/ new AttackBase() { Name = "T’au-tech rifle",    Range = 30, Attacks = 1, BallisticSkill = 4, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Rapid_Fire_1 },
                    /*5*/ new AttackBase() { Name = "Close combat weapon",            Attacks = 2, WeaponSkill = 3,    Strength = 4, ArmorPenetration = 0, Damage = 1 },
                    /*6*/ new AttackBase() { Name = "Ripping fangs",                  Attacks = 3, WeaponSkill = 3,    Strength = 3, ArmorPenetration = 0, Damage = 1 },
                    /*7*/ new AttackBase() { Name = "Ritual blade",                   Attacks = 3, WeaponSkill = 3,    Strength = 5, ArmorPenetration = 0, Damage = 1 }
                },
                Models = new Dictionary<int, ModelBase>()
                {
                    { 9, new ModelBase() { Name = "Kroot Farstalkers", Movement = 7, Toughness = 3, ArmorSave = 6, Wounds = 1, Leadership = 7, ObjectiveControl = 1, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(9)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(9, 7) { Attacks = new int[] { 1 } },
                                    new AttackOptionGroupBase(1, 0)
                                    {
                                        AttackOption = new List<AttackGroupBase>()
                                        {
                                            new AttackGroupBase(1, 0) { Attacks = new int[] { 0 } },
                                            new AttackGroupBase(1, 0) { Attacks = new int[] { 3 } }
                                        }
                                    }
                                }
                            },
                            new AttackGroupBase(9) { Attacks = new int[] { 2, 5 } }
                        }
                    } },
                    { 1, new ModelBase() { Name = "Kroot Kill-broker", Movement = 7, Toughness = 3, ArmorSave = 6, Wounds = 1, Leadership = 7, ObjectiveControl = 1, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(1)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 1 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 4 } }
                                }
                            },
                            new AttackGroupBase(1) { Attacks = new int[] { 2, 7 } }
                        }
                    } },
                    { 2, new ModelBase() { Name = "Kroot Hounds", Movement = 12, Toughness = 3, ArmorSave = 6, Wounds = 1, Leadership = 7, ObjectiveControl = 0,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(2) { Attacks = new int[] {6 } }
                        }
                    } }
                }
            },
            new UnitBase() { Name = "Pathfinder Team",
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Grenades, Keywords.Markerlight, "Pathfinder Team"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Drone burst cannon", Range = 18, Attacks = 4, BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1 },
                    /*1*/ new AttackOption() { SubType = "Ion rifle",
                            AttacksTypes = new List<AttackBase>()
                            {
                                new AttackBase() { Name = "Ion rifle – standard",   Range = 30, Attacks = 3, BallisticSkill = 5, Strength = 7, ArmorPenetration = 1, Damage = 1, Modifiers = Modifiers.Heavy },
                                new AttackBase() { Name = "Ion rifle – overcharge", Range = 30, Attacks = 3, BallisticSkill = 5, Strength = 8, ArmorPenetration = 2, Damage = 2, Modifiers = Modifiers.Hazardous | Modifiers.Heavy }
                            }
                          },
                    /*2*/ new AttackBase() { Name = "Pulse carbine", Range = 20, Attacks = 2, BallisticSkill = 4, Strength = 5,  ArmorPenetration = 0, Damage = 1 },
                    /*3*/ new AttackBase() { Name = "Pulse pistol",  Range = 12, Attacks = 1, BallisticSkill = 4, Strength = 5,  ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Pistol },
                    /*4*/ new AttackBase() { Name = "Rail rifle",    Range = 30, Attacks = 1, BallisticSkill = 5, Strength = 10, ArmorPenetration = 4, Damage = 3, Modifiers = Modifiers.Devastating_Wounds | Modifiers.Heavy },
                    /*5*/ new AttackOption() { SubType = "Semi-automatic grenade launcher",
                            AttacksTypes = new List<AttackBase>()
                            {
                                new AttackBase() { Name = "Semi-automatic grenade launcher – EMP",    Range = 18, Attacks = 1, BallisticSkill = 4, Strength = 3, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Anti_Vehicle_4 | Modifiers.Devastating_Wounds },
                                new AttackBase() { Name = "Semi-automatic grenade launcher – fusion", Range = 18, Attacks = 1, BallisticSkill = 4, Strength = 6, ArmorPenetration = 1, Damage = 3 }
                            }
                          },
                    /*6*/ new AttackBase() { Name = "Close combat weapon",            Attacks = 2, WeaponSkill = 5,    Strength = 3, ArmorPenetration = 0, Damage = 1 },
                    /*7*/ new AttackBase() { Name = "Twin pulse carbine", Range = 20, Attacks = 2, BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Assault | Modifiers.Twin_Linked }
                },
                Models = new Dictionary<int, ModelBase>()
                {
                    { 9, new ModelBase() { Name = "Pathfinders", Movement = 7, Toughness = 3, ArmorSave = 4, Wounds = 1, Leadership = 7, ObjectiveControl = 1, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(9)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(9, 5) { Attacks = new int[] { 2 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 2, 5 } },
                                    new AttackOptionGroupBase(3, 0)
                                    {
                                        AttackOption = new List<AttackGroupBase>()
                                        {
                                            new AttackGroupBase(3, 0) { Attacks = new int[] { 1 } },
                                            new AttackGroupBase(3, 0) { Attacks = new int[] { 4 } }
                                        }
                                    }
                                }
                            },
                            new AttackGroupBase(9) { Attacks = new int[] { 3, 6 } }
                        }
                    } },
                    { 1, new ModelBase() { Name = "Pathfinder Shas’ui", Movement = 7, Toughness = 3, ArmorSave = 4, Wounds = 1, Leadership = 7, ObjectiveControl = 1, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(1)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 2 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 2, 5 } },
                                }
                            },
                            new AttackGroupBase(1) { Attacks = new int[] { 3, 6 } },
                            new WargearGroupBase(1, 0) { Attacks = new int[] { 0 } },
                            new WargearGroupBase(2, 0) { Attacks = new int[] { 7 } }
                        }
                    } }
                },
                CreateEffects = new KeyValuePair<EffectsType, List<Delegate>>(
                    EffectsType.AttackOption,
                    new List<Delegate>()
                    {
                        /// Make Attack 5 limit 1
                        (Func<List<Tuple<int, Dictionary<int, int>>>, List<Tuple<int, Dictionary<int, int>>>>)((outAttackGroupsList) => outAttackGroupsList.Where(x => !x.Item2.ContainsKey(5) || x.Item2[5] <= 1).ToList())
                    }
                )
            },
            new UnitBase() { Name = "Stealth Battlesuits - 3",
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Fly, Keywords.Battlesuit, "Stealth"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Burst cannon",       Range = 18, Attacks = 4, BallisticSkill = 4, Strength = 5, ArmorPenetration = 0, Damage = 1 },
                    /*1*/ new AttackBase() { Name = "Fusion blaster",     Range = 12, Attacks = 1, BallisticSkill = 4, Strength = 9, ArmorPenetration = 4, DamageExtra = "D6", Modifiers = Modifiers.Melta_2 },
                    /*2*/ new AttackBase() { Name = "Battlesuit fists",               Attacks = 2, WeaponSkill = 5,    Strength = 4, ArmorPenetration = 0, Damage = 1 },
                    /*3*/ new AttackBase() { Name = "Twin pulse carbine", Range = 20, Attacks = 2, BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1,         Modifiers = Modifiers.Assault | Modifiers.Twin_Linked }
                },
                Models = new Dictionary<int, ModelBase>()
                {
                    { 2, new ModelBase() { Name = "Stealth Shas’ui", Movement = 8, Toughness = 4, ArmorSave = 3, Wounds = 2, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(2)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(2, 1) { Attacks = new int[] { 0 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 1 } }
                                }
                            },
                            new AttackGroupBase(2) { Attacks = new int[] { 2 } }
                        }
                    } },
                    { 1, new ModelBase() { Name = "Stealth Shas’vre", Movement = 8, Toughness = 4, ArmorSave = 3, Wounds = 2, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(1)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 0 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 1 } }
                                }
                            },
                            new AttackGroupBase(1) { Attacks = new int[] { 2 } },
                            new WargearGroupBase(2, 0) { Attacks = new int[] { 3 } }
                        }
                    } }
                },
                CreateEffects = new KeyValuePair<EffectsType, List<Delegate>>(
                    EffectsType.AttackOption,
                    new List<Delegate>()
                    {
                        /// Make Attack 1 limit 1
                        (Func<List<Tuple<int, Dictionary<int, int>>>, List<Tuple<int, Dictionary<int, int>>>>)((outAttackGroupsList) => outAttackGroupsList.Where(x => !x.Item2.ContainsKey(1) || x.Item2[1] <= 1).ToList())
                    }
                )
            },
            new UnitBase() { Name = "Stealth Battlesuits - 5",
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Fly, Keywords.Battlesuit, "Stealth"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Burst cannon",       Range = 18, Attacks = 4, BallisticSkill = 4, Strength = 5, ArmorPenetration = 0, Damage = 1 },
                    /*1*/ new AttackBase() { Name = "Fusion blaster",     Range = 12, Attacks = 1, BallisticSkill = 4, Strength = 9, ArmorPenetration = 4, DamageExtra = "D6", Modifiers = Modifiers.Melta_2 },
                    /*2*/ new AttackBase() { Name = "Battlesuit fists",               Attacks = 2, WeaponSkill = 5,    Strength = 4, ArmorPenetration = 0, Damage = 1 },
                    /*3*/ new AttackBase() { Name = "Twin pulse carbine", Range = 20, Attacks = 2, BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1,         Modifiers = Modifiers.Assault | Modifiers.Twin_Linked }
                },
                Models = new Dictionary<int, ModelBase>()
                {
                    { 4, new ModelBase() { Name = "Stealth Shas’ui", Movement = 8, Toughness = 4, ArmorSave = 3, Wounds = 2, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(4)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(4, 2) { Attacks = new int[] { 0 } },
                                    new AttackGroupBase(2, 0) { Attacks = new int[] { 1 } }
                                }
                            },
                            new AttackGroupBase(4) { Attacks = new int[] { 2 } }
                        }
                    } },
                    { 1, new ModelBase() { Name = "Stealth Shas’vre", Movement = 8, Toughness = 4, ArmorSave = 3, Wounds = 2, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(1)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 0 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 1 } }
                                }
                            },
                            new AttackGroupBase(1) { Attacks = new int[] { 2 } },
                            new WargearGroupBase(2, 0) { Attacks = new int[] { 3 } }
                        }
                    } }
                },
                CreateEffects = new KeyValuePair<EffectsType, List<Delegate>>(
                    EffectsType.AttackOption,
                    new List<Delegate>()
                    {
                        /// Make Attack 1 limit 1
                        (Func<List<Tuple<int, Dictionary<int, int>>>, List<Tuple<int, Dictionary<int, int>>>>)((outAttackGroupsList) => outAttackGroupsList.Where(x => !x.Item2.ContainsKey(1) || x.Item2[1] <= 2).ToList())
                    }
                )
            },
            new UnitBase() { Name = "Vespid Stingwings - 5",
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Fly, "Vespid Stingwings"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Neutron blaster", Range = 18, Attacks = 4, BallisticSkill = 4, Strength = 5,  ArmorPenetration = 2, Damage = 2, Modifiers = Modifiers.Assault },
                    /*1*/ new AttackBase() { Name = "Stingwing claws",             Attacks = 1, WeaponSkill = 4,    Strength = 4,  ArmorPenetration = 1, Damage = 1 }
                },
                Models = new Dictionary<int, ModelBase>()
                {
                    { 4, new ModelBase() { Name = "Vespid Stingwings", Movement = 12, Toughness = 4, ArmorSave = 4, Wounds = 1, Leadership = 7, ObjectiveControl = 1
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(4) { Attacks = new int[] { 0, 1 } }
                        }
                    } },
                    { 1, new ModelBase() { Name = "Vespid Strain Leader", Movement = 12, Toughness = 4, ArmorSave = 4, Wounds = 1, Leadership = 7, ObjectiveControl = 1,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1) { Attacks = new int[] { 0, 1 } }
                        }
                    } }
                }
            },
            new UnitBase() { Name = "Vespid Stingwings - 10",
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Fly, "Vespid Stingwings"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Neutron blaster",          Range = 18, Attacks = 4,         BallisticSkill = 4, Strength = 5,  ArmorPenetration = 2, Damage = 2, Modifiers = Modifiers.Assault },
                    /*1*/ new AttackBase() { Name = "Neutron grenade launcher", Range = 18, AttacksExtra = "D6", BallisticSkill = 4, Strength = 4,  ArmorPenetration = 1, Damage = 2, Modifiers = Modifiers.Anti_Infantry_3 | Modifiers.Blast},
                    /*2*/ new AttackBase() { Name = "Neutron rail rifle",       Range = 30, Attacks = 1,         BallisticSkill = 4, Strength = 10, ArmorPenetration = 4, Damage = 3, Modifiers = Modifiers.Melta_2 },
                    /*3*/ new AttackBase() { Name = "T’au flamer",              Range = 12, AttacksExtra = "D6",                     Strength = 4,  ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Melta_2 },
                    /*4*/ new AttackBase() { Name = "Stingwing claws",                      Attacks = 1,         WeaponSkill = 4,    Strength = 4,  ArmorPenetration = 1, Damage = 1 }
                },
                Models = new Dictionary<int, ModelBase>()
                {
                    { 9, new ModelBase() { Name = "Vespid Stingwings", Movement = 12, Toughness = 4, ArmorSave = 4, Wounds = 1, Leadership = 7, ObjectiveControl = 1, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(9)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(9, 6) { Attacks = new int[] { 0 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 1 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 2 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 3 } }
                                }
                            },
                            new AttackGroupBase(9) { Attacks = new int[] { 4 } }
                        }
                    } },
                    { 1, new ModelBase() { Name = "Vespid Strain Leader", Movement = 12, Toughness = 4, ArmorSave = 4, Wounds = 1, Leadership = 7, ObjectiveControl = 1,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1) { Attacks = new int[] { 0, 4 } }
                        }
                    } }
                }
            },
            #endregion
        };
    }
}
