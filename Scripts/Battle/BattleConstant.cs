namespace Battle
{
    /// <summary>
    /// ユニットがパネルの上に乗ってるかを確認する
    /// </summary>
    public enum RideState
    {
        None = 0,
        Ride = 1,
    }

    public enum PlayerType
    {
        Player,
        Enemy
    }

    public enum UnitState
    {
        Alive,
        Move,
        Death,
    }

    public enum PanelType
    {
        Normal,
        NearCastle,
    }

    public enum BattleResult
    {
        Win,
        Lose,
        Draw,
    }
}