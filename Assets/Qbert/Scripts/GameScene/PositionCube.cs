namespace Assets.Qbert.Scripts.GameScene
{
    [System.Serializable]
    public struct PositionCube
    {
        public int y;
        public int x;

        public PositionCube(int x  , int y)
        {
            this.y = y;
            this.x = x;
        }

        public static bool operator ==(PositionCube a, PositionCube b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(PositionCube a, PositionCube b)
        {
            return !(a == b);
        }

        public new string ToString()
        {
            return y + " - " + x;
        }
    }
}
