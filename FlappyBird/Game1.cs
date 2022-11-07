using FlappyBird.states;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace FlappyBird
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private RenderTarget2D _renderTarget;

        private const int WINDOW_WIDTH = 1280;
        private const int WINDOW_HEIGHT = 720;
        private const int BACKGROUND_SCROLL_SPEED = 30;
        private const int GROUND_SCROLL_SPEED = 60;
        private const int BACKGROUND_LOOPING_POINT = 413;
        private const int GROUND_LOOPING_POINT = 413;

        private Texture2D background;
        private Texture2D ground;

        private float dt;
        private float backgroundScroll;
        private float groundScroll;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Window.Title = "Fifty Bird";

            _graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            _graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            _graphics.SynchronizeWithVerticalRetrace = true;
            _graphics.ApplyChanges();

            _renderTarget = new RenderTarget2D(GraphicsDevice, Globals.VIRTUAL_WIDTH, Globals.VIRTUAL_HEIGHT);

            Globals.random = new Random();

            backgroundScroll = 0;
            groundScroll = 0;

            Bird.Init(Content.Load<Texture2D>("Images/bird"));
            Pipe.Init(Content.Load<Texture2D>("Images/pipe"));

            Globals.stateMachine = new StateMachine(new System.Collections.Generic.Dictionary<string, State>(){
                { "title", new TitleScreenState() },
                { "countdown", new CountdownState() },
                { "play", new PlayState() },
                { "score", new ScoreState() }
            });
            Globals.stateMachine.Change("title");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            background = Content.Load<Texture2D>("Images/background");
            ground = Content.Load<Texture2D>("Images/ground");

            Globals.smallFont = Content.Load<SpriteFont>("Fonts/smallFont");
            Globals.mediumFont = Content.Load<SpriteFont>("Fonts/mediumFont");
            Globals.flappyFont = Content.Load<SpriteFont>("Fonts/flappyFont");
            Globals.hugeFont = Content.Load<SpriteFont>("Fonts/hugeFont");

            Globals.jumpSfx = Content.Load<SoundEffect>("Sounds/Sfx/jump");
            Globals.explosionSfx = Content.Load<SoundEffect>("Sounds/Sfx/explosion");
            Globals.hurtSfx = Content.Load<SoundEffect>("Sounds/Sfx/hurt");
            Globals.scoreSfx = Content.Load<SoundEffect>("Sounds/Sfx/score");

            Globals.musicSong = Content.Load<Song>("Sounds/Music/marios_way");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(Globals.musicSong);
        }

        protected override void Update(GameTime gameTime)
        {

            KeyboardState kState = Keyboard.GetState();
            if (kState.IsKeyDown(Keys.Escape))
                Exit();

            dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            backgroundScroll += BACKGROUND_SCROLL_SPEED * dt;
            backgroundScroll = backgroundScroll % BACKGROUND_LOOPING_POINT;

            groundScroll += GROUND_SCROLL_SPEED * dt;
            groundScroll = groundScroll % GROUND_LOOPING_POINT;

            Globals.stateMachine.Update(dt);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();

            _spriteBatch.Draw(background, new Vector2(-backgroundScroll, 0), Color.White);
            Globals.stateMachine.Draw(_spriteBatch);
            _spriteBatch.Draw(ground, new Vector2(-groundScroll, Globals.VIRTUAL_HEIGHT - 16), Color.White);

            _spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(_renderTarget, new Rectangle(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}