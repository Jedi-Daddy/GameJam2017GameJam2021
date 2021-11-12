namespace Assets.Scripts.core
{
    public class Game1
    {
        private static Game _instance;
        public static Game Instance => _instance ?? (_instance = new Game());


    }
}
