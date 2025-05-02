using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace _40000_Statistics
{
    static class Tau
    {
        public static List<ModelBase> Units = new List<ModelBase>()
        {
            new ModelBase(){ModelNo = 1, Name = "Cadre Fireblade", Movement = 6, Toughness = 3, ArmorSave = 4, Wounds = 3, Leadership = 7, ObjectiveControl = 1,
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() {Name = "Fireblade pulse rifle", Range = 30, Attacks = 1, BallisticSkill = 3, Strength = 5, ArmorPenetration = 0, Damage = 2, Modifiers = Modifiers.Rapid_Fire_1 },
                    /*1*/ new AttackBase() {Name = "Close combat weapon",               Attacks = 3, WeaponSkill = 4,    Strength = 3, ArmorPenetration = 0, Damage = 1 },
                    /*2*/ new AttackBase() {Name = "Twin pulse carbine",    Range = 20, Attacks = 2, BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Assault | Modifiers.Twin_Linked }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackGroupBase(1) { Attacks = new int[] { 0, 1 } },
                    new WargearGroupBase(1, 0) { Attacks = new int[] { 2 } }
                }
            }
        };
    }
}
