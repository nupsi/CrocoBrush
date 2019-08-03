namespace CrocoBrush
{
    public class FakeSceneData
    {
        public FakeSceneData(string name) : this(name, name, name)
        {
        }

        public FakeSceneData(string scene, string canvas, string position)
        {
            Scene = scene;
            Canvas = canvas;
            Position = position;
        }

        public string Scene { get; private set; }
        public string Canvas { get; private set; }
        public string Position { get; private set; }
    }
}