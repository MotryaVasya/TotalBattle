using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TotalBattle.Objects;

namespace TotalBattle
{
    class UnitSL : SL_Service<List<Unit>>
    {
        private Dictionary<string, Weapon> _weapons = new Dictionary<string, Weapon>()
        {
            {WeaponArsenal.sword.Name, WeaponArsenal.sword },
            {WeaponArsenal.axe.Name, WeaponArsenal.axe },
            {WeaponArsenal.spear.Name, WeaponArsenal.spear }
        };
        private Dictionary<string, Armor> _armors = new Dictionary<string, Armor>()
        {
            {ArmorArsenal.leather.Name, ArmorArsenal.leather },
            {ArmorArsenal.chainmail.Name, ArmorArsenal.chainmail },
            {ArmorArsenal.plate.Name, ArmorArsenal.plate }
        };
        private const string filePath = @"C:\Users\STUDENT\Desktop\TotalBattle\TotalBattle\Data\";
        public string FilePath => filePath;
        #region abstract methods
        protected override string[] GetDataFromObject(List<Unit> data)
        {
            if (data.Count == 0) return new string[0];
            string[] result = new string[data.Count];
            for (int i = 0; i < data.Count; i++)
            {
                result[i] = $"{data[i].Name}{SptChar}" +
                    $"{data[i].Health}{SptChar}" +
                    $"{data[i].Damage - data[i].Weapon.Damage}{SptChar}" +
                    $"{data[i].Weapon.Name}{SptChar}" +
                    $"{data[i].Armor.Name}";
            }
            return result;
        }
        protected override List<Unit> CreateObjectFromData(string[] data)
        {
            if (data.Length == 0) return new List<Unit>();
            List<Unit> result = new List<Unit>();
            foreach (var item in data)
            {
                var unit = item.Split(SptChar);
                result.Add(new Unit(unit[0], ParseFloat(unit[1]), ParseFloat(unit[2]), _weapons[unit[3]], _armors[unit[4]]));
            }
            return result;
        }
        #endregion
        private float ParseFloat(string value)
        {
            try
            {
                return float.Parse(value);
            }
            catch (SystemException)
            {
                return 0;
            }
        }
    }
}