using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Pong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        private RenderTarget2D renderTarget;

        private Texture2D colorTexture;
        private SpriteFont menuFont;
        private SpriteFont scoreFont;

        private SoundEffect paddleHitSfx;
        private SoundEffect scoreSfx;
        private SoundEffect wallHitSfx;

        private Random rnd;

        private Paddle player1;
        private Paddle player2;

        private Ball ball;

        private int servingPlayer;
        private int winningPlayer;

        private int player1Score;
        private int player2Score;

        private GameState gameState;

        private KeyboardState lastKState;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.Title = "Pong";
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = Globals.WINDOW_WIDTH;
            _graphics.PreferredBackBufferHeight = Globals.WINDOW_HEIGHT;
            _graphics.SynchronizeWithVerticalRetrace = true;
            _graphics.IsFullScreen = true;
            _graphics.HardwareModeSwitch = false;
            _graphics.ApplyChanges();

            renderTarget = new RenderTarget2D(GraphicsDevice, Globals.VIRTUAL_WIDTH, Globals.VIRTUAL_HEIGHT);

            colorTexture = new Texture2D(GraphicsDevice, 1, 1);

            rnd = new Random();

            player1 = new Paddle(10, 30, 5, 20);
            player2 = new Paddle(Globals.VIRTUAL_WIDTH - 10, Globals.VIRTUAL_HEIGHT - 30, 5, 20);

            servingPlayer = rnd.Next(1, 3);

            ball = new Ball((Globals.VIRTUAL_WIDTH / 2) - 2, (Globals.VIRTUAL_HEIGHT / 2) - 2, 4, 4, servingPlayer);

            player1Score = 0;
            player2Score = 0;

            gameState = GameState.Start;
            lastKState = Keyboard.GetState();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            menuFont = Content.Load<SpriteFont>("MenuFont");
            scoreFont = Content.Load<SpriteFont>("ScoreFont");

            paddleHitSfx = Content.Load<SoundEffect>("Sounds/PaddleHit");
            scoreSfx = Content.Load<SoundEffect>("Sounds/Score");
            wallHitSfx = Content.Load<SoundEffect>("Sounds/WallHit");

            Debug.Initialize(menuFont);
        }

        protected override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            KeyboardState kState = Keyboard.GetState();

            if (kState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            else if (kState.IsKeyDown(Keys.Enter) && !lastKState.IsKeyDown(Keys.Enter))
            {
                if (gameState == GameState.Start)
                {
                    gameState = GameState.Play;
                }
                else if (gameState == GameState.Serve)
                {
                    gameState = GameState.Play;
                }
                else if (gameState == GameState.Done)
                {
                    gameState = GameState.Serve;

                    ball.Reset(servingPlayer);

                    player1Score = 0;
                    player2Score = 0;

                    servingPlayer = winningPlayer == 1 ? 2 : 1;
                }
            }

            if (gameState == GameState.Serve)
            {
                ball.Dy = rnd.Next(-50, 51);
                int dx = rnd.Next(140, 201);
                ball.Dx = servingPlayer == 1 ? dx : -dx;
            }
            else if (gameState == GameState.Play)
            {
                // Player Collision
                if (ball.Collides(player1))
                {
                    ball.Dx *= -1.03f;
                    ball.X = player1.X + player1.Width;

                    if (ball.Dy < 0)
                    {
                        ball.Dy = -rnd.Next(10, 151);
                    }
                    else
                    {
                        ball.Dy = rnd.Next(10, 151);
                    }

                    paddleHitSfx.Play();
                }
                else if (ball.Collides(player2))
                {
                    ball.Dx *= -1.03f;
                    ball.X = player2.X - ball.Width;

                    if (ball.Dy < 0)
                    {
                        ball.Dy = -rnd.Next(10, 151);
                    }
                    else
                    {
                        ball.Dy = rnd.Next(10, 151);
                    }

                    paddleHitSfx.Play();
                }

                // Screen Collision
                if (ball.Y <= 0)
                {
                    ball.Y = 0;
                    ball.Dy = -ball.Dy;

                    wallHitSfx.Play();
                }
                else if (ball.Y >= Globals.VIRTUAL_HEIGHT - ball.Height)
                {
                    ball.Y = Globals.VIRTUAL_HEIGHT - ball.Height;
                    ball.Dy = -ball.Dy;

                    wallHitSfx.Play();
                }

                // Scoring
                if (ball.X < -ball.Width)
                {
                    player2Score += 1;
                    ball.Reset(servingPlayer);

                    scoreSfx.Play();

                    // Victory
                    if (player2Score == 10)
                    {
                        winningPlayer = 2;
                        gameState = GameState.Done;
                    }
                    // Serve
                    else
                    {
                        servingPlayer = 1;
                        gameState = GameState.Serve;
                    }


                }
                else if (ball.X > Globals.VIRTUAL_WIDTH)
                {
                    player1Score += 1;
                    ball.Reset(servingPlayer);

                    scoreSfx.Play();

                    // Victory
                    if (player1Score == 10)
                    {
                        winningPlayer = 1;
                        gameState = GameState.Done;
                    }
                    // Serve
                    else
                    {
                        servingPlayer = 2;
                        gameState = GameState.Serve;
                    }
                }

                // Update ball
                ball.Update(dt);
            }

            // Paddle Movement
            if (kState.IsKeyDown(Keys.W))
            {
                player1.MoveUp();
            }
            else if (kState.IsKeyDown(Keys.S))
            {
                player1.MoveDown();
            }
            else
            {
                player1.Stop();
            }

            if (kState.IsKeyDown(Keys.Up))
            {
                player2.MoveUp();
            }
            else if (kState.IsKeyDown(Keys.Down))
            {
                player2.MoveDown();
            }
            else
            {
                player2.Stop();
            }

            // Update Paddles
            player1.Update(dt);
            player2.Update(dt);

            lastKState = kState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(new Color(40, 45, 52));
            _spriteBatch.Begin();

            if (gameState == GameState.Start)
            {
                string startMessage = "Hello Pong!";
                Vector2 startMessageSize = menuFont.MeasureString(startMessage);
                _spriteBatch.DrawString(menuFont, startMessage, new Vector2((Globals.VIRTUAL_WIDTH / 2) - (startMessageSize.X / 2), 20), Color.White);

                string serveMessage = $"Player {servingPlayer} to serve";
                Vector2 serveMessageSize = menuFont.MeasureString(serveMessage);
                _spriteBatch.DrawString(menuFont, serveMessage, new Vector2((Globals.VIRTUAL_WIDTH / 2) - (serveMessageSize.X / 2), 30), Color.White);

                string restartMessge = "Press Enter to serve!";
                Vector2 restartMessageSize = menuFont.MeasureString(restartMessge);
                _spriteBatch.DrawString(menuFont, restartMessge, new Vector2((Globals.VIRTUAL_WIDTH / 2) - (restartMessageSize.X / 2), 40), Color.White);
            }
            else if (gameState == GameState.Serve)
            {
                string serveMessage = $"Player {servingPlayer} to serve";
                Vector2 serveMessageSize = menuFont.MeasureString(serveMessage);
                _spriteBatch.DrawString(menuFont, serveMessage, new Vector2((Globals.VIRTUAL_WIDTH / 2) - (serveMessageSize.X / 2), 20), Color.White);

                string restartMessge = "Press Enter to serve!";
                Vector2 restartMessageSize = menuFont.MeasureString(restartMessge);
                _spriteBatch.DrawString(menuFont, restartMessge, new Vector2((Globals.VIRTUAL_WIDTH / 2) - (restartMessageSize.X / 2), 30), Color.White);
            }
            else if (gameState == GameState.Done)
            {
                string victoryMessage = $"Player {winningPlayer} wins!";
                Vector2 victoryMessageSize = scoreFont.MeasureString(victoryMessage);
                _spriteBatch.DrawString(scoreFont, victoryMessage, new Vector2((Globals.VIRTUAL_WIDTH / 2) - (victoryMessageSize.X / 2), 10), Color.White);

                string restartMessge = "Press Enter to restart!";
                Vector2 restartMessageSize = menuFont.MeasureString(restartMessge);
                _spriteBatch.DrawString(menuFont, restartMessge, new Vector2((Globals.VIRTUAL_WIDTH / 2) - (restartMessageSize.X / 2), 30), Color.White);
            }

            _spriteBatch.DrawString(scoreFont, player1Score.ToString(), new Vector2((Globals.VIRTUAL_WIDTH / 2) - 50, Globals.VIRTUAL_HEIGHT / 3), Color.White);
            _spriteBatch.DrawString(scoreFont, player2Score.ToString(), new Vector2((Globals.VIRTUAL_WIDTH / 2) + 30, Globals.VIRTUAL_HEIGHT / 3), Color.White);

            colorTexture.SetData(new Color[] { Color.White });
            player1.Render(_spriteBatch, colorTexture);
            player2.Render(_spriteBatch, colorTexture);
            ball.Render(_spriteBatch, colorTexture);

            Debug.DisplayFPS(_spriteBatch, (float)gameTime.ElapsedGameTime.TotalSeconds);

            _spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(renderTarget, new Rectangle(0, 0, Globals.WINDOW_WIDTH, Globals.WINDOW_HEIGHT), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}