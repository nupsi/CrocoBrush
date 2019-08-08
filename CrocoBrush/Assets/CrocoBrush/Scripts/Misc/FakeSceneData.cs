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

        /// <summary>
        /// Fill empty fields with data from another fake scene.
        /// </summary>
        /// <param name="data">Source scene to copy data from.</param>
        public void Fill(FakeSceneData data)
        {
            Scene = Scene.Length == 0 ? data.Scene : Scene;
            Canvas = Canvas.Length == 0 ? data.Canvas : Canvas;
            Position = Position.Length == 0 ? data.Position : Position;
        }

        public string Scene { get; private set; }
        public string Canvas { get; private set; }
        public string Position { get; private set; }
    }
}