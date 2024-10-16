using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TotalBattle.Interfaces;
/* TODO
* 1) создать метод создания армии
* 2) создать механизм установки связей между бойцами (враг-враг)
* 3) добавить ограничения на максимальное кол-во противников у 1 юнита
*/
namespace TotalBattle
{
    public class GameLoop
    {
        private List<IUnit> _army1;
        private List<IUnit> _army2;


        public void Start()
        {
            _army1 = new List<IUnit>();
            _army2 = new List<IUnit>();
        }
        private void UnitGenerator(List<IUnit> army)
        {
            var unitCounts = ValidIntInput("сколько бойцов в вашей армии? (нужен хотя бы 1 воин!)");

        }
        private int ValidIntInput(string message)
        {
            while (true)
            {
                Console.WriteLine(message);
                if (int.TryParse(Console.ReadLine(), out int res))
                {
                    if (res > 0)
                    {
                        return res;
                    }
                    Console.WriteLine("Ошибка ввода данных! Повторите ввод!");
                }
            }
        }
        private int ValidChiceInput(string message, int maxCgoice)
        {
            while (true)
            {
                Console.WriteLine(message);
                if (int.TryParse(Console.ReadLine(), out int res))
                {
                    if ((res <= maxCgoice) && (res > 0)) return res;
                    Console.WriteLine("ошибка ввода данных");
                }
            }
        }
    }
}
