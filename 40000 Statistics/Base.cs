using ConsoleTables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace _40000_Statistics
{
    internal class ModelBase
    {
        public string Name { get; set; }
        public virtual int Movement { get; set; }
        public virtual int Toughness { get; set; }
        public virtual int ArmorSave { get; set; }
        public virtual int InvulnerableSave { get; set; }
        public virtual int Wounds { get; set; }
        public virtual int Leadership { get; set; }
        public virtual int ObjectiveControl { get; set; }
        public List<AttackBase> Attacks { get; set; }
        public List<AttackGroupBase> AttackGroups { get; set; }
        public ModelBase() { 
            Attacks = new List<AttackBase>();
            AttackGroups = new List<AttackGroupBase>();
        }
        public override string ToString() => Name;
        public override bool Equals(object obj)
        {
            if (obj is ModelBase other)
            {
                return Toughness == other.Toughness &&
                       ArmorSave == other.ArmorSave &&
                       InvulnerableSave == other.InvulnerableSave &&
                       Wounds == other.Wounds;
            }
            return false;
        }
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + Toughness.GetHashCode();
            hash = hash * 31 + ArmorSave.GetHashCode();
            hash = hash * 31 + InvulnerableSave.GetHashCode();
            hash = hash * 31 + Wounds.GetHashCode();
            return hash;
        }
        public static bool operator ==(ModelBase left, ModelBase right) => left is null ? right is null : left.Equals(right);
        public static bool operator !=(ModelBase left, ModelBase right) => !(left == right);
    }
    internal class UnitBase : ModelBase 
    { 
        public Dictionary<int, ModelBase> Models { get; set; }
        public UnitBase() => Models = new Dictionary<int, ModelBase>();
        public UnitBase(ModelBase model) => Models = new Dictionary<int, ModelBase> { { 1, model } };
        public override string ToString() => Name;
        public override int Movement => Models.Values.FirstOrDefault()?.Movement ?? 0;
        public override int Toughness => Models.Values.FirstOrDefault()?.Toughness ?? 0;
        public override int ArmorSave => Models.Values.FirstOrDefault()?.ArmorSave ?? 0;
        public override int InvulnerableSave => Models.Values.FirstOrDefault()?.InvulnerableSave ?? 0;
        public override int Wounds => Models.Values.FirstOrDefault()?.Wounds ?? 0;
        public override int Leadership => Models.Values.FirstOrDefault()?.Leadership ?? 0;
        public override int ObjectiveControl => Models.Values.FirstOrDefault()?.ObjectiveControl ?? 0;
    }

    internal class AttackGroupBase
    {
        public int MinNo { get; set; }
        public int MaxNo { get; set; }
        public int[] Attacks { get; set; }

        public AttackGroupBase(int maxNo = 1, int? minNo = null) { MinNo = minNo??maxNo; MaxNo = maxNo; }
    }
    internal class AttackOptionGroupBase : AttackGroupBase
    {
        public AttackOptionGroupBase(int maxNo = 1, int? minNo = null) : base(maxNo, minNo) { }
        public List<AttackGroupBase> AttackOption { get; set; }
        public AttackOptionGroupBase() => AttackOption = new List<AttackGroupBase>();
        public AttackOptionGroupBase(AttackGroupBase attackGroup)
        {
            AttackOption = new List<AttackGroupBase> { attackGroup };
            this.MinNo = attackGroup.MinNo;
            this.MaxNo = attackGroup.MaxNo;
        }
    }

    internal class AttackBase
    {
        public virtual string Name { get; set; }
        public virtual int Range { get; set; }
        public virtual int Attacks { get; set; }
        public virtual string AttacksExtra { get; set; }
        public virtual int? BallisticSkill { get; set; }
        public virtual int? WeaponSkill { get; set; }
        public virtual int? AttackSkill { get => ((Modifiers & Modifiers.Torrent) == Modifiers.Torrent) ? (int?)null : BallisticSkill ?? WeaponSkill.Value; }
        public virtual int Strength { get; set; }
        public virtual int ArmorPenetration { get; set; }
        public virtual int Damage { get; set; }
        public virtual string DamageExtra { get; set; }
        public virtual Modifiers Modifiers { get; set; }
        public AttackBase() { }
        public override string ToString() => $"{Name} - A {AttacksExtra ?? Attacks.ToString()}";
    }
    internal class AttackOption : AttackBase
    {
        public string SubType { get; set; }
        public List<AttackBase> AttacksTypes { get; set; }
        public AttackOption() => AttacksTypes = new List<AttackBase>();
        public AttackOption(AttackBase attack) => AttacksTypes = new List<AttackBase> { attack };
        public override string ToString() => Name;
        public override string Name => AttacksTypes.FirstOrDefault()?.Name ?? null;
        public override int Range => AttacksTypes.FirstOrDefault()?.Range ?? 0;
        public override int Attacks => AttacksTypes.FirstOrDefault()?.Attacks ?? 0;
        public override string AttacksExtra => AttacksTypes.FirstOrDefault()?.AttacksExtra ?? null;
        public override int? BallisticSkill => AttacksTypes.FirstOrDefault()?.BallisticSkill ?? null;
        public override int? WeaponSkill => AttacksTypes.FirstOrDefault()?.WeaponSkill ?? null;
        public override int? AttackSkill => AttacksTypes.FirstOrDefault()?.AttackSkill ?? null;
        public override int Strength => AttacksTypes.FirstOrDefault()?.Strength ?? 0;
        public override int ArmorPenetration => AttacksTypes.FirstOrDefault()?.ArmorPenetration ?? 0;
        public override int Damage => AttacksTypes.FirstOrDefault()?.Damage ?? 0;
        public override string DamageExtra => AttacksTypes.FirstOrDefault()?.DamageExtra ?? null;
        public override Modifiers Modifiers => AttacksTypes.FirstOrDefault()?.Modifiers ?? Modifiers.None;
    }

    enum Modifiers
    {
        None            = 0,
        Anti_Monster_4  = 1 << 0,
        Assault         = 1 << 1,
        Hazardous       = 1 << 2,
        Melta_2         = 1 << 3,
        Pistol          = 1 << 4,
        Rapid_Fire_1    = 1 << 5,
        Twin_Linked     = 1 << 6,
        Blast           = 1 << 7,
        Indirect_Fire   = 1 << 8,
        Ignores_Cover   = 1 << 9,
        Torrent         = 1 << 10
    }
}
