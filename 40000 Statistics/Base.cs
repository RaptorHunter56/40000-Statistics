using ConsoleTables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
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
        public UnitBase() {
            Models = new Dictionary<int, ModelBase>();
        }
        public UnitBase(ModelBase model) {
            Models = new Dictionary<int, ModelBase> { { 1, model } };
        }
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
        public int No { get; set; }
        public List<int> Attacks { get; set; }
        public AttackGroupBase(int no = 1) => No = no;
    }
    internal class AttackBase
    {
        public string Name { get; set; }
        public int Range { get; set; }
        public int Attacks { get; set; }
        public string AttacksExtra { get; set; }
        public int? BallisticSkill { get; set; }
        public int? WeaponSkill { get; set; }
        public int? AttackSkill { get => ((Modifiers & Modifiers.Torrent) == Modifiers.Torrent) ? (int?)null : BallisticSkill ?? WeaponSkill.Value; }
        public int Strength { get; set; }
        public int ArmorPenetration { get; set; }
        public int Damage { get; set; }
        public string DamageExtra { get; set; }
        public Modifiers Modifiers { get; set; }
        public int NoMax { get; set; }
        public int? NoMin { get; set; }
        public AttackBase() { }
        public override string ToString() => $"{Name} - A {AttacksExtra ?? Attacks.ToString()}";
    }
    enum Modifiers
    {
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
    internal class Combat
    {
        public ModelBase Unit1 { get; set; }
        public ModelBase Unit2 { get; set; }

        public Combat(ModelBase Unit1, ModelBase Unit2) { this.Unit1 = Unit1; this.Unit2 = Unit2; }
        public string Role(int? role) => role.HasValue ? $"{role}+" : "NA";
        public string Percent(double wound) => $"{wound * 100:F2}%";

        public double AttackNoChance(AttackBase attack, int attackNo, int weaponNo)
        {
            var attackRange = AttackRange(attack);
            if (attackRange.Item1 == attackRange.Item2) return 1;
            var parts = attack.AttacksExtra.Split('+');
            var dicePart = parts[0].Split(new string[] { "D" }, StringSplitOptions.RemoveEmptyEntries);
            return CalculateProbability(attackNo - (parts.Count() > 1 ? Int32.Parse(parts[1]) : 0), dicePart.Count() > 1 ? Int32.Parse(dicePart[0]) * weaponNo : 1 * weaponNo);
        }
        public double HitChance(AttackBase attack) => attack.AttackSkill.HasValue ? ((attack.AttackSkill.Value < 1) ? 1 : (attack.AttackSkill.Value > 6) ? 0 : (7 - attack.AttackSkill.Value) / 6.0) : 1;
        public int WoundRole(AttackBase attack, ModelBase model) => (model.Toughness <= attack.Strength / 2) ? 2 : (model.Toughness < attack.Strength) ? 3 : (model.Toughness == attack.Strength) ? 4 : (model.Toughness > attack.Strength * 2) ? 6 : 5;
        public double WoundChance(AttackBase attack, ModelBase model)
        {
            int woundrole = WoundRole(attack, model);
            double baseValue = (woundrole < 1) ? 1 : (woundrole > 6) ? 0 : (7 - woundrole) / 6.0;
            return ((attack.Modifiers & Modifiers.Twin_Linked) == Modifiers.Twin_Linked) ? (baseValue + (baseValue * (1 - baseValue))) : baseValue;
        }
        public int SaveRole(AttackBase attack, ModelBase model) => (model.ArmorSave + attack.ArmorPenetration > (model.InvulnerableSave > 0 ? model.InvulnerableSave : 100)) ? model.InvulnerableSave : (model.ArmorSave + attack.ArmorPenetration);
        public double SaveChance(AttackBase attack, ModelBase model)
        {
            int saverole = SaveRole(attack, model);
            return (1 - ((saverole < 1) ? 1 : (saverole > 6) ? 0 : (7 - saverole) / 6.0));
        }
        public double DamageNoChance(AttackBase attack, int damageNo, int woundNo)
        {
            var damageRange = DamageRange(attack);
            if (damageRange.Item1 == damageRange.Item2) return 1;
            var parts = attack.DamageExtra.Split('+');
            var dicePart = parts[0].Split(new string[] { "D" }, StringSplitOptions.RemoveEmptyEntries);
            var x =CalculateProbability(damageNo - (parts.Count() > 1 ? Int32.Parse(parts[1]) : 0), dicePart.Count() > 1 ? Int32.Parse(dicePart[0]) * woundNo : 1 * woundNo);
            return x;
        }
        public Dictionary<int, double> CalculateDeathPercentages(Tuple<int, int> damageRange, KeyValuePair<int, ModelBase> newUnit2, KeyValuePair<int, double> save)
        {

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////

        public Tuple<int, int> DamageRange(AttackBase attack) => CalculateRange(attack.Damage, attack.DamageExtra);
        public Tuple<int, int> AttackRange(AttackBase attack) => CalculateRange(attack.Attacks, attack.AttacksExtra);
        private Tuple<int, int> CalculateRange(int baseValue, string extra)
        {
            if (extra == null) return new Tuple<int, int>(baseValue, baseValue);

            var parts = extra.Split('+');
            var dicePart = parts[0].Split(new[] { "D" }, StringSplitOptions.RemoveEmptyEntries);
            int numberOfDice = dicePart.Length > 1 ? int.Parse(dicePart[0]) : 1;
            int sides = int.Parse(dicePart[dicePart.Length - 1]);

            int min = numberOfDice + (parts.Length > 1 ? int.Parse(parts[1]) : 0);
            int max = numberOfDice * sides + (parts.Length > 1 ? int.Parse(parts[1]) : 0);

            return new Tuple<int, int>(min + baseValue, max + baseValue);
        }
        public double CalculateProbability(int totalValue, int numberOfDice)
        {
            int totalOutcomes = (int)Math.Pow(6, numberOfDice);
            int[,] dp = new int[numberOfDice + 1, totalValue + 1];
            dp[0, 0] = 1;

            for (int dice = 1; dice <= numberOfDice; dice++)
                for (int value = 1; value <= totalValue; value++)
                    for (int face = 1; face <= 6; face++)
                        if (value - face >= 0)
                            dp[dice, value] += dp[dice - 1, value - face];

            return (double)dp[numberOfDice, totalValue] / totalOutcomes;
        }
        public int nCr(int n, int r) => (r > n) ? 0 : (r == 0 || r == n) ? 1 : nCr(n - 1, r - 1) + nCr(n - 1, r);
        public double CalculateChance(int tries, double chance, int hitsRequired) => nCr(tries, hitsRequired) * Math.Pow(chance, hitsRequired) * Math.Pow(1 - chance, tries - hitsRequired);

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Hit Breakdown
        public Dictionary<int, double> GetHitBreakdown(AttackBase attack, Tuple<int, int> attackRange, int weaponNo) => JoinDictonarys(ParallelEnumerable
                .Range(attackRange.Item1 * weaponNo, (attackRange.Item2 * weaponNo) - (attackRange.Item1 * weaponNo) == 0 ? 1 : (attackRange.Item2 * weaponNo) - (attackRange.Item1 * weaponNo) + 1)
                .Select(x => ParallelEnumerable.Range(0, x + 1).ToDictionary(y => y, y => CalculateChance(x, HitChance(attack), y) * AttackNoChance(attack, x, weaponNo))));
        #endregion
        #region Wound Breakdown
        public Dictionary<int, double> GetWoundBreakdown(AttackBase attack, Tuple<int, int> attackRange, int weaponNo, ModelBase model) => GetWoundBreakdown(GetHitBreakdown(attack, attackRange, weaponNo), attack, attackRange, weaponNo, model);
        public Dictionary<int, double> GetWoundBreakdown(Dictionary<int, double> hitBrakedown, AttackBase attack, Tuple<int, int> attackRange, int weaponNo, ModelBase model) => JoinDictonarys(hitBrakedown
                .Select(x => ParallelEnumerable.Range(0, x.Key + 1).ToDictionary(y => y, y => CalculateChance(x.Key, WoundChance(attack, model), y) * x.Value)));
        #endregion
        #region Save Breakdown
        public Dictionary<int, double> GetSaveBreakdown(AttackBase attack, Tuple<int, int> attackRange, int weaponNo, ModelBase model) => GetSaveBreakdown(GetWoundBreakdown(attack, attackRange, weaponNo, model), attack, attackRange, weaponNo, model);
        public Dictionary<int, double> GetSaveBreakdown(Dictionary<int, double> woundBrakedown, AttackBase attack, Tuple<int, int> attackRange, int weaponNo, ModelBase model) => JoinDictonarys(woundBrakedown
                .Select(x => ParallelEnumerable.Range(0, x.Key + 1).ToDictionary(y => y, y => CalculateChance(x.Key, SaveChance(attack, model), y) * x.Value)));
        #endregion
        #region Damage Breakdown
        public Dictionary<int, double> GetDamageBreakdown(AttackBase attack, Tuple<int, int> attackRange, Tuple<int, int> damageRange, int weaponNo, ModelBase model) => GetDamageBreakdown(GetSaveBreakdown(attack, attackRange, weaponNo, model), attack, attackRange, damageRange, weaponNo);
        public Dictionary<int, double> GetDamageBreakdown(Dictionary<int, double> saveBrakedown, AttackBase attack, Tuple<int, int> attackRange, Tuple<int, int> damageRange, int weaponNo) => JoinDictonarys(saveBrakedown
                .Select(x => ParallelEnumerable.Range(damageRange.Item1 * x.Key, (damageRange.Item2 * x.Key) - (damageRange.Item1 * x.Key) + 1).ToDictionary(y => y, y => DamageNoChance(attack, y, x.Key) * x.Value)));
        #endregion
        private Dictionary<int, double> JoinDictonarys(IEnumerable<Dictionary<int, double>> enumerable) => enumerable.SelectMany(x => x).GroupBy(x => x.Key).ToDictionary(x => x.Key, x => x.Sum(y => y.Value));

        ////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 1. Base Role Table
        /// </summary>
        internal void RoleTable()
        {
            Console.WriteLine($"{Unit1.Name} => {Unit2.Name}");
            var table = new ConsoleTable("Name", "Hr", "HrC", "S", "Wr", "WC", "AP", "Sr", "SrC", "=>");
            foreach (var item in Unit1.Attacks)
            {
                table.AddRow(
                    item.Name,
                    Role(item.AttackSkill),
                    Percent(HitChance(item)),
                    item.Strength,
                    Role(WoundRole(item, Unit2)),
                    Percent(WoundChance(item, Unit2)),
                    $"-{item.ArmorPenetration}",
                    Role(SaveRole(item, Unit2)),
                    Percent(SaveChance(item, Unit2)),
                    Percent(HitChance(item) * WoundChance(item, Unit2) * SaveChance(item, Unit2))
                );
            }

            table.Write();
            Console.WriteLine();
        }
        /// <summary>
        /// 2. Base Damage Table
        /// </summary>
        internal void AttackBreakdownTable()
        {
            Console.WriteLine($"{Unit1.Name} => {Unit2.Name}");
            var table = new ConsoleTable("Name", "WeNo", "ANo", "AC");
            foreach (var item in Unit1.Attacks)
            {
                var showAttack = true;
                for (int WeNo = item.NoMin ?? item.NoMax; WeNo <= item.NoMax; WeNo++)
                {
                    var attackRange = AttackRange(item);
                    for (int ANo = attackRange.Item1 * WeNo; ANo <= attackRange.Item2 * WeNo; ANo++)
                    {
                        if (WeNo == 0) continue;
                        table.AddRow(
                            showAttack ? item.Name : "",
                            WeNo,
                            ANo,
                            Percent(AttackNoChance(item, ANo, WeNo))
                        );
                        showAttack = false;
                    }
                }
            }

            table.Write();
            Console.WriteLine();
        }
        /// <summary>
        /// 3. Base Small Hit Table
        /// </summary>
        internal void HitBreakdownTable()
        {
            Console.WriteLine($"{Unit1.Name} => {Unit2.Name}");
            var table = new ConsoleTable("Name", "WeNo", "ANo", "AC", "HC", "HNo", "=>", "");
            foreach (var item in Unit1.Attacks)
            {
                var showAttack = true;
                for (int WeNo = item.NoMin ?? item.NoMax; WeNo <= item.NoMax; WeNo++)
                {
                    var attackRange = AttackRange(item);
                    for (int ANo = attackRange.Item1 * WeNo; ANo <= attackRange.Item2 * WeNo; ANo++)
                    {
                        if (WeNo == 0) continue;
                        table.AddRow(
                            showAttack ? item.Name : "",
                            WeNo,
                            ANo,
                            Percent(AttackNoChance(item, ANo, WeNo)),
                            Percent(HitChance(item)),
                            "",
                            "",
                            ""
                        );
                        foreach (var hitBreakdown in GetHitBreakdown(item, attackRange, WeNo))
                        {
                            table.AddRow(
                                "",
                                "",
                                "",
                                "",
                                "",
                                hitBreakdown.Key,
                                Percent(CalculateChance(ANo, HitChance(item), hitBreakdown.Key)),
                                Percent(hitBreakdown.Value)
                            );
                        }
                        showAttack = false;
                    }
                }
            }

            table.Write();
            Console.WriteLine();
        }
        /// <summary>
        /// 4. Base Wound Table
        /// </summary>
        internal void WoundBreakdownTable()
        {
            Console.WriteLine($"{Unit1.Name} => {Unit2.Name}");
            var table = new ConsoleTable("Name", "WeNo", "HNo", "HC", "WNo", "WC");
            foreach (var item in Unit1.Attacks)
            {
                var showAttack = true;
                for (int WeNo = item.NoMin ?? item.NoMax; WeNo <= item.NoMax; WeNo++)
                {
                    if (WeNo == 0) continue;
                    var attackRange = AttackRange(item);
                    table.AddRow(
                        showAttack ? item.Name : "",
                        WeNo,
                        "",
                        "",
                        "",
                        Percent(WoundChance(item, Unit2))
                    );
                    foreach (var hitBreakdown in GetHitBreakdown(item, attackRange, WeNo))
                    {
                        var woundBrakedown = GetWoundBreakdown(item, attackRange, WeNo, Unit2);
                        table.AddRow(
                            "",
                            "",
                            hitBreakdown.Key,
                            Percent(hitBreakdown.Value),
                            "",
                            ""
                        );
                        foreach (var wound in woundBrakedown)
                        {
                            table.AddRow(
                                "",
                                "",
                                "",
                                "",
                                wound.Key,
                                Percent(wound.Value)
                            );
                        }
                    }
                    showAttack = false;
                }
            }

            table.Write();
            Console.WriteLine();
        }
        /// <summary>
        /// 5. Base Save Table
        /// </summary>
        internal void SaveBreakdownTable()
        {
            Console.WriteLine($"{Unit1.Name} => {Unit2.Name}");
            var table = new ConsoleTable("Name", "WeNo", "HNo", "HC", "SNo", "SC");
            foreach (var item in Unit1.Attacks)
            {
                var showAttack = true;
                for (int WeNo = item.NoMin ?? item.NoMax; WeNo <= item.NoMax; WeNo++)
                {
                    if (WeNo == 0) continue;
                    var attackRange = AttackRange(item);
                    table.AddRow(
                        showAttack ? item.Name : "",
                        WeNo,
                        "",
                        "",
                        "",
                        Percent(SaveChance(item, Unit2))
                    );
                    foreach (var woundBreakdown in GetWoundBreakdown(item, attackRange, WeNo, Unit2))
                    {
                        var saveBrakedown = GetSaveBreakdown(item, attackRange, WeNo, Unit2);
                        table.AddRow(
                            "",
                            "",
                            woundBreakdown.Key,
                            Percent(woundBreakdown.Value),
                            "",
                            ""
                        );
                        foreach (var save in saveBrakedown)
                        {
                            table.AddRow(
                                "",
                                "",
                                "",
                                "",
                                save.Key,
                                Percent(save.Value)
                            );
                        }
                    }
                    showAttack = false;
                }
            }

            table.Write();
            Console.WriteLine();
        }
        /// <summary>
        /// 6. Base Damage Table
        /// </summary>
        internal void DamageBreakdownTable()
        {
            Console.WriteLine($"{Unit1.Name} => {Unit2.Name}");
            var table = new ConsoleTable("Name", "WeNo", "HNo", "HC", "DNo", "DC");
            foreach (var item in Unit1.Attacks)
            {
                var showAttack = true;
                for (int WeNo = item.NoMin ?? item.NoMax; WeNo <= item.NoMax; WeNo++)
                {
                    if (WeNo == 0) continue;
                    var attackRange = AttackRange(item);
                    var damageRange = DamageRange(item);
                    table.AddRow(
                        showAttack ? item.Name : "",
                        WeNo,
                        "",
                        "",
                        damageRange.ToString(),
                        item.DamageExtra
                    );
                    foreach (var woundBreakdown in GetSaveBreakdown(item, attackRange, WeNo, Unit2))
                    {
                        var damageBrakedown = GetDamageBreakdown(item, attackRange, damageRange, WeNo, Unit2);
                        table.AddRow(
                            "",
                            "",
                            woundBreakdown.Key,
                            Percent(woundBreakdown.Value),
                            "",
                            ""
                        );
                        foreach (var damage in damageBrakedown)
                        {
                            table.AddRow(
                                "",
                                "",
                                "",
                                "",
                                damage.Key,
                                Percent(damage.Value)
                            );
                        }
                    }
                    showAttack = false;
                }
            }

            table.Write();
            Console.WriteLine();
        }
        /// <summary>
        /// 7. Base Full Table
        /// </summary>
        internal void FullBreakdownTable()
        {
            Console.WriteLine($"{Unit1.Name} => {Unit2.Name}");
            var table = new ConsoleTable("Name", "Weapon No", "Hit No", "Hit Chance", "Wound No", "Wound Chance", "Save No", "Save Chance", "Damage No", "Damage Chance");
            foreach (var item in Unit1.Attacks)
            {
                var showAttack = true;
                for (int WeNo = item.NoMin ?? item.NoMax; WeNo <= item.NoMax; WeNo++)
                {
                    var showWeaponNo = true;
                    if (WeNo == 0) continue;
                    var attackRange = AttackRange(item);
                    var damageRange = DamageRange(item);
                    var hitBreakdown = GetHitBreakdown(item, attackRange, WeNo);
                    var woundBreakdown = GetWoundBreakdown(hitBreakdown, item, attackRange, WeNo, Unit2);
                    var saveBreakdown = GetSaveBreakdown(woundBreakdown, item, attackRange, WeNo, Unit2);
                    var damageBreakdown = GetDamageBreakdown(saveBreakdown, item, attackRange, damageRange, WeNo);
                    var length = new[] { hitBreakdown, woundBreakdown, saveBreakdown, damageBreakdown }.Max(d => d.Keys.Max()); ;
                    for (int i = 0; i <= length; i++)
                    {
                        table.AddRow(
                            showAttack ? item.Name : "",
                            showWeaponNo ? WeNo.ToString() : "",
                            hitBreakdown.ContainsKey(i) ? i.ToString() : "",
                            hitBreakdown.ContainsKey(i) ? Percent(hitBreakdown[i]) : "",
                            woundBreakdown.ContainsKey(i) ? i.ToString() : "",
                            woundBreakdown.ContainsKey(i) ? Percent(woundBreakdown[i]) : "",
                            saveBreakdown.ContainsKey(i) ? i.ToString() : "",
                            saveBreakdown.ContainsKey(i) ? Percent(saveBreakdown[i]) : "",
                            damageBreakdown.ContainsKey(i) ? i.ToString() : "",
                            damageBreakdown.ContainsKey(i) ? Percent(damageBreakdown[i]) : ""
                        );
                        showAttack = showWeaponNo = false;
                    }
                }
            }

            table.Write();
            Console.WriteLine();
        }
        /// <summary>
        /// 5. Base Kill Table
        /// </summary>
        internal void KillBreakdownTable()
        {
            Console.WriteLine($"{Unit1.Name} => {Unit2.Name}");
            var table = new ConsoleTable("Name", "Weapon No", "Models", "Wounds", "=>", "", "");
            foreach (var item in Unit1.Attacks)
            {
                var showAttack = true;
                for (int WeNo = item.NoMin ?? item.NoMax; WeNo <= item.NoMax; WeNo++)
                {
                    var showWeaponNo = true;
                    if (WeNo == 0) continue;
                    var attackRange = AttackRange(item);
                    var damageRange = DamageRange(item);
                    var hitBreakdown = GetHitBreakdown(item, attackRange, WeNo);

                    foreach (var newUnit2 in (Unit2 is UnitBase unitBase ? unitBase: new UnitBase(Unit2)).Models.GroupBy(model => model.Value).ToDictionary(group => group.Sum(m => m.Key), group => group.First().Value))
                    {
                        var woundBreakdown = GetWoundBreakdown(hitBreakdown, item, attackRange, WeNo, newUnit2.Value);
                        var saveBreakdown = GetSaveBreakdown(woundBreakdown, item, attackRange, WeNo, newUnit2.Value);
                        var showSaveNo = true;
                        foreach (var save in saveBreakdown)
                        {
                            var damageBreakdown = ParallelEnumerable.Range(damageRange.Item1 * save.Key, (damageRange.Item2 * save.Key) - (damageRange.Item1 * save.Key) + 1).ToDictionary(y => y, y => DamageNoChance(item, y, save.Key) * save.Value);

                            table.AddRow(
                                showAttack ? item.Name : "",
                                showWeaponNo ? WeNo.ToString() : "",
                                showSaveNo ? newUnit2.Key.ToString() : "",
                                showSaveNo ? newUnit2.Value.Wounds.ToString() : "",
                                $"{save.Key}: {Percent(save.Value)}",
                                damageRange,
                                CalculateDeathPercentages(damageRange, newUnit2, save).First()
                            );
                            showAttack = showWeaponNo = showSaveNo = false;
                        }
                    }
                }
            }

            table.Write();
            Console.WriteLine();
        }
    }
}
