using System;
using System.Collections.Generic;
using System.Linq;
using TotalBattle.Interfaces;
using TotalBattle.Objects;
//Исправить момент, когда юнит, бьющий в спину ставит блок
//Проблема с блоком
namespace TotalBattle
{
    public class GameLoop
    {
        private List<Unit> _army1;
        private List<Unit> _army2;
        private Dictionary<Unit, Unit> _battles;
        public void Start()
        {
            UnitManager manager = new UnitManager();

            Console.WriteLine("Первая армия: ");
            _army1 = new List<Unit>();
            CreateArmy(_army1, "Ч");

            Console.WriteLine("Вторая армия: ");
            _army2 = new List<Unit>();
            CreateArmy(_army2, "Б");

            _battles = new Dictionary<Unit, Unit>();
            MakeLinks(_army1, _army2, _battles);

            Subscribe(_army1, _army2);

            GameTurn();
        }

        #region ArmyCreation
        private void CreateArmy(List<Unit> army, string name)
        {
            int unitsCount = ValidIntInput("Сколько бойцов в вашей армии? " +
                "(Нужен хотя бы 1 воин!)");

            int editorChoice = ValidChoiceInput("Хотите изменить снаряжение армии?\n" +
                "1 - да\n" +
                "2 - нет\n", 2);

            switch (editorChoice)
            {
                case 1: ArmyEditor(army, unitsCount, name); break;
                case 2: ArmyGenerator(army, unitsCount, null, null, name); break;
            }
        }
        private void ArmyEditor(List<Unit> army, int armyCount, string name)
        {
            while (true)
            {
                if (armyCount == 0)
                {
                    Console.WriteLine("Нет бойцов для редактирования!");
                    return;
                }

                Weapon newWeapon = WeaponArsenal.sword;
                Armor newArmor = ArmorArsenal.leather;

                int unitsToEdit;
                do
                {
                    unitsToEdit = ValidIntInput($"Сколько бойцов вы хотите изменить?\n" +
                    $"Доступные к редактированию бойцы: {armyCount}");
                    if (unitsToEdit > armyCount) Console.WriteLine("Ошибка ввода!");
                } while (unitsToEdit > armyCount);

                int weaponChoice = ValidChoiceInput("Выберите оружие:\n" +
                    "1 - меч\n" +
                    "2 - копье\n" +
                    "3 - булава\n" +
                    "4 - топор\n", 4);
                switch (weaponChoice)
                {
                    case 1: break;
                    case 2: newWeapon = WeaponArsenal.spear; break;
                    case 3: newWeapon = WeaponArsenal.mace; break;
                    case 4: newWeapon = WeaponArsenal.axe; break;
                }

                int armorChoice = ValidChoiceInput("Выберите броню:\n" +
                    "1 - кожаная\n" +
                    "2 - кольчуга\n" +
                    "3 - латы\n", 3);
                switch (armorChoice)
                {
                    case 1: break;
                    case 2: newArmor = ArmorArsenal.chainmail; break;
                    case 3: newArmor = ArmorArsenal.plate; break;
                }

                ArmyGenerator(army, unitsToEdit, newArmor, newWeapon, name);
                armyCount -= unitsToEdit;

                int editChoice = ValidChoiceInput("Продолжить редактирование?\n" +
                    "1 - да\n" +
                    "2 - нет\n", 2);

                if (editChoice == 2)
                {
                    if (armyCount != 0)
                    {
                        ArmyGenerator(army, armyCount, null, null, name);
                        return;
                    }
                }
            }
        }
        private void ArmyGenerator(List<Unit> army, int armyCount, IArmor armor, IWeapon weapon, string name)
        {
            int j = army.Count;
            for (int i = 0; i < armyCount; i++, j++)
            {
                if ((weapon == null) || (armor == null)) army.Add(new Unit(name + j.ToString()));
                else army.Add(new Unit(name + j.ToString(), 25, 5, weapon, armor));
            }
        }
        #endregion

        #region LinksOperations
        private void MakeLinks(List<Unit> team1, List<Unit> team2,
            Dictionary<Unit, Unit> links)
        {
            if (team1.Count == team2.Count)
            {
                for (int i = 0; i < team1.Count; i++)
                {
                    links.Add(team1[i], team2[i]);
                    links.Add(team2[i], team1[i]);
                }
            }
            else if (team1.Count > team2.Count)
            {
                if (team1.Count <= team2.Count * 3) 
                {
                    for (int i = 0; i < team2.Count; i++)
                    {
                        links.Add(team1[i], team2[i]);
                        links.Add(team2[i], team1[i]);
                    }

                    for (int i = 0; i < team1.Count / team2.Count; i++)
                    {
                        for (int j = 0; j < team2.Count; j++)
                        {
                            if (j + team2.Count * (i + 1) == team1.Count) break;
                            links.Add(team1[j + team2.Count * (i + 1)], team2[j]);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < team2.Count; i++)
                    {
                        links.Add(team1[i], team2[i]);
                        links.Add(team2[i], team1[i]);
                    }

                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < team2.Count; j++)
                        {
                            links.Add(team1[j + team2.Count * (i + 1)], team2[j]);
                        }
                    }

                    for (int i = 0; i < team1.Count - team2.Count * 3; i++)
                    {
                        links.Add(team1[i + team2.Count * 3], null);
                    }

                }
            }
            else
            {
                if (team2.Count <= team1.Count * 3)
                {
                    for (int i = 0; i < team1.Count; i++)
                    {
                        links.Add(team2[i], team1[i]);
                        links.Add(team1[i], team2[i]);
                    }

                    for (int i = 0; i < team2.Count / team1.Count; i++)
                    {
                        for (int j = 0; j < team1.Count; j++)
                        {
                            if (j + team1.Count * (i + 1) == team2.Count) break;
                            links.Add(team2[j + team1.Count * (i + 1)], team1[j]);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < team1.Count; i++)
                    {
                        links.Add(team2[i], team1[i]);
                        links.Add(team1[i], team2[i]);
                    }

                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < team1.Count; j++)
                        {
                            links.Add(team2[j + team1.Count * (i + 1)], team1[j]);
                        }
                    }

                    for (int i = 0; i < team2.Count - team1.Count * 3; i++)
                    {
                        links.Add(team2[i + team1.Count * 3], null);
                    }

                }
            }
        }
        private void Subscribe(List<Unit> team1, List<Unit> team2)
        {
            foreach (Unit unit in team1) unit.onKill += BreakLink; 
            foreach (Unit unit in team2) unit.onKill += BreakLink;
        }
        private void BreakLink(IUnit enemy)
        {
            foreach (var unit in _battles.ToList())
            {
                if (unit.Value == null) continue;
                if (unit.Value.Name == enemy.Name) 
                {
                    switch (enemy.Name[0])
                    {
                        case 'Ч': _army1.Remove(unit.Value); break;
                        case 'Б': _army2.Remove(unit.Value); break;
                    }
                    _battles.Remove(unit.Value);
                    unit.Value.onKill -= BreakLink;
                    _battles[unit.Key] = null;
                    UpdateLink(unit.Key);
                }
            }
            FindNewLinks();
        }
        private void UpdateLink(Unit freeLinkUnit) 
        {
            foreach (var unit in _battles.ToList())
            {
                if (freeLinkUnit == unit.Value) _battles[freeLinkUnit] = unit.Key;
                else _battles[freeLinkUnit] = FindForEnemy(freeLinkUnit);
            }
        }
        private void FindNewLinks()
        {
            foreach (var unit in _battles.ToList())
            {
                if (unit.Value == null) _battles[unit.Key] = FindForEnemy(unit.Key);
            }
        }
        private Unit FindForEnemy(Unit enemy)
        {
            int enemyCounter = 0;
            foreach (var unit in _battles.ToList())
            {
                foreach (var unitToCheck in _battles.ToList())
                {
                    if (unit.Key == unitToCheck.Value) enemyCounter++;
                }
                if ((enemyCounter < 3) &&
                    (enemy.Name[0] != unit.Key.Name[0])) return unit.Key;
                enemyCounter = 0;
            }
            return null;
        }
        #endregion

        #region InputMethods
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
        #endregion

        #region GameMethods
        private void GameTurn()
        {
            int Turn = 0;
            while ((_army1.Count != 0) && (_army2.Count != 0))
            {
                Turn++;
                Console.WriteLine($"-------День {Turn}-------\n");

                foreach (var unit in _battles.ToList())
                {
                    if (unit.Value == null) continue;

                    unit.Key.Block();

                    if (unit.Key.Attack(unit.Value))
                    {
                        Console.WriteLine($"{unit.Key.Name} " +
                            $"({Math.Round(unit.Key.Health, 1)}) " +
                            $"наносит атаку {unit.Value.Name} " +
                            $"({Math.Round(unit.Value.Health, 1)})");
                    }
                    else if (unit.Key.IsBlocks)
                    {
                        Console.WriteLine($"{unit.Key.Name} ставит блок " +
                            $"против {unit.Value.Name}");
                    }
                    else
                    {
                        Console.WriteLine($"{unit.Key.Name} не смог " +
                            $"нанести урон {unit.Value.Name}");
                    }
                }
                Console.WriteLine($"\nПервая армия {_army1.Count} vs " +
                    $"Вторая армия {_army2.Count}");
            }
        }
        #endregion

        private void SaveArmy(List<Unit> units, UnitManager manager, string saveFile)
        {
            manager.Data = units;
            manager.fileName = saveFile;
            manager.Save();
        }
    }
}
