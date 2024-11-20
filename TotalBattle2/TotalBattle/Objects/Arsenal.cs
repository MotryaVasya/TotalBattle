namespace TotalBattle.Objects
{
    static class WeaponArsenal
    {
        static public readonly Weapon sword =
            new Weapon("Sword", 3, Weapons.slashing);

        static public readonly Weapon spear =
            new Weapon("Spear", 2, Weapons.piercing);

        static public readonly Weapon mace =
            new Weapon("Mace", 5, Weapons.bludgeoning);

        static public readonly Weapon axe =
            new Weapon("Axe", 5, Weapons.slashing);
    }

    static class ArmorArsenal
    {
        static public readonly Armor leather =
            new Armor("Leather", 0.2f, Armors.light);

        static public readonly Armor chainmail =
            new Armor("Сhainmail", 0.4f, Armors.medium);

        static public readonly Armor plate =
            new Armor("Plate", 0.6f, Armors.heavy);
    }
}
