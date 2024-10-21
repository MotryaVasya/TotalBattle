using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TotalBattle.Interfaces;
using TotalBattle.Objects;

//TODO
//1) Создать метод создания армии
//2) Создать механизм установки связей между бойцами (враг-враг)
//3) Добавить ограничения на максимальное кол-во противников у 1 юнита

namespace TotalBattle
{
    public class GameLoop
    {
        private List<Unit> _army1;
        private List<Unit> _army2;
        private Dictionary<Unit, Unit> _battles;
        public void Start()
        {
            Console.WriteLine("first army");
            _army1 = new List<Unit>();
            CreateArmy(_army1, "Ч");
            Console.WriteLine("second army");
            _army2 = new List<Unit>();
            CreateArmy(_army2, "Б");
            _battles = new Dictionary<Unit, Unit>();

        }

        private void CreateArmy(List<Unit> army, string name)
        {
            var unitsCount = ValidIntInput("Сколько бойцов в вашей армии? " +
                "(Нужен хотя бы 1 воин!)");
            int editorChoice = ValidChoiceInput("Хотите изменить снарежение армии,\n" + "1 - да\n" + "2 - нет\n", 2);
            switch (editorChoice)
            {
                case 1: AmryEditor(army, unitsCount, name); break;
                case 2: ArmyGenerator(army, unitsCount, null, null, name); break;
            }
        }

        private void ArmyGenerator(List<Unit> army, int armyCount, IArmor armor, IWeapon weapon, string name)
        {

            int j = army.Count;
            for (int i = 0; i < armyCount; i++, j++)
            {
                if ((weapon == null) || (armor == null)) army.Add(new Unit(name + j.ToString()));
                else army.Add(new Unit(name + j.ToString(), 100, 5, weapon, armor));
            }
        }
        private void AmryEditor(List<Unit> army, int armyCount, string name)
        {
            while (true)
            {
                if (army.Count == 0)
                {
                    Console.WriteLine("нет бойцов для редактирования!");
                    return;
                }
                Weapon newWeapon = WeaponArsenal.sword;
                Armor newArmor = ArmorArsenal.leather;
                int unitsToEdit;
                do
                {
                    unitsToEdit = ValidIntInput($"сколько бойцов вы хотите изменить?\n" + $"доступные к редактированию бойцы{armyCount}");
                    if (unitsToEdit > armyCount) Console.WriteLine("ошибка ввода!");
                } while (unitsToEdit > armyCount);
                int weaponChoice = ValidChoiceInput("выберите оружие:\n" +
                    "1 - меч\n" +
                    "2 - копье\n" +
                    "3 - булава\n" +
                    "4 - топор\n", 4);
                switch (weaponChoice)
                {
                    case 1:
                        break;
                    case 2: newWeapon = WeaponArsenal.spear; break;
                    case 3: newWeapon = WeaponArsenal.mace; break;
                    case 4: newWeapon = WeaponArsenal.axe; break;
                }
                int armoChoice = ValidChoiceInput("выберите броню:\n" +
                    "1 - кожаная" +
                    "2 - кольчуга" +
                    "3 - латы", 3);
                switch (armoChoice)
                {
                    case 1:
                        break;
                    case 2: newArmor = ArmorArsenal.chainmail; break;
                    case 3: newArmor = ArmorArsenal.plate; break;
                }
                ArmyGenerator(army, unitsToEdit, newArmor, newWeapon, name);
                armyCount -= unitsToEdit;
                int editChoice = ValidChoiceInput("продолжить редактирование:\n" +
                    "1 - да\n" +
                    "2 - нет\n", 2);
                if (editChoice == 2)
                {
                    if (armyCount != 0) ArmyGenerator(army, armyCount, null, null, name); return;
                }
            }
        }

        private int ValidIntInput(string message)
        {
            while (true)
            {
                Console.WriteLine(message);
                if (int.TryParse(Console.ReadLine(), out int res))
                {
                    if (res > 0) return res;
                }
                Console.WriteLine("Ошибка ввода данных! Повторите ввод!");
            }
        }

        private int ValidChoiceInput(string message, int maxChoice)
        {
            while (true)
            {
                Console.WriteLine(message);
                if (int.TryParse(Console.ReadLine(), out int res))
                {
                    if ((res <= maxChoice) && (res > 0)) return res;
                }
                Console.WriteLine("Ошибка ввода данных!");
            }
        }
    }
}
