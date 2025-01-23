
public static class GlobalData
{
   public static int EnemyCount; 
    
    public static void Initialize()
    {
        EnemyCount = 0;
    }

    public static int GetEnemyCount()
    {
        return EnemyCount;
    }

    public static void SetEnemyCount(int newEnemyCount)
    {
        EnemyCount = newEnemyCount;
    }

}