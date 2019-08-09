namespace CrocoBrush
{
    /// <summary>
    /// Fake Scene Data containing scene name, canvas and camera position name.
    /// </summary>
    public class FakeSceneData
    {
        /// <summary>
        /// Create a fake scene data object with scene, canvas and position having the same name.
        /// </summary>
        /// <param name="name">Scene, canvas and camera position name.</param>
        public FakeSceneData(string name) : this(name, name, name)
        {
        }

        /// <summary>
        /// Create a fake scene data with scene, canvas and camera position names.
        /// </summary>
        /// <param name="scene">Fake Scene name.</param>
        /// <param name="canvas">Scene canvas name.</param>
        /// <param name="position">Scene camera position name.</param>
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

        /// <summary>
        /// Scene name.
        /// </summary>
        public string Scene { get; private set; }

        /// <summary>
        /// Scene Canvas name.
        /// </summary>
        public string Canvas { get; private set; }

        /// <summary>
        /// Scene Camera Position name.
        /// </summary>
        public string Position { get; private set; }
    }
}