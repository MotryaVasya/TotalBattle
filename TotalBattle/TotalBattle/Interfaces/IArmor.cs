namespace TotalBattle.Interfaces
{
    interface IArmor
    {
        string Name { get; }
        float Defense { get; }
        Armors Type { get; }
    }
}
