namespace Scripts.GameScene
{
    [System.Serializable]
    public struct PositionCube
    {
        public int line;
        public int position;

        public PositionCube(int line, int position)
        {
            this.line = line;
            this.position = position;
        }

        public static bool operator ==(PositionCube a, PositionCube b)
        {
            return a.position == b.position && a.line == b.line;
        }

        public static bool operator !=(PositionCube a, PositionCube b)
        {
            return !(a == b);
        }

        public new string ToString()
        {
            return line + " - " + position;
        }
    }
}
