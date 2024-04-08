using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Windows.Forms;

namespace SDVD {
    public class ThankYouForWatchingTheDVD : Game {

        private const int SPEED = 1;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D dvd;

        private int x;
        private int y;
        private int mx;
        private int my;
        private bool xpos;
        private bool ypos;

        public ThankYouForWatchingTheDVD() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize() {
            base.Initialize();

            _graphics.PreferredBackBufferWidth = Screen.PrimaryScreen.Bounds.Width;
            _graphics.PreferredBackBufferHeight = Screen.PrimaryScreen.Bounds.Height;
            _graphics.HardwareModeSwitch = false;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            dvd = Content.Load<Texture2D>("dvd");

            mx = Screen.PrimaryScreen.Bounds.Width - dvd.Width;
            my = Screen.PrimaryScreen.Bounds.Height - dvd.Height;

            Random r = new Random();
            x = r.Next(0, mx);
            y = r.Next(0, my);
            xpos = r.Next() % 2 == 0;
            ypos = r.Next() % 2 == 0;
        }

        protected override void Update(GameTime gameTime) {

            var ts = TouchPanel.GetState();
            if (ts.IsConnected && ts.Count > 0) {
                Exit();
            }
            if (Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed) {
                Exit();
            }
            if (Keyboard.GetState().GetPressedKeyCount() > 0) {
                Exit();
            }

            x += SPEED * (xpos ? 1 : -1);
            y += SPEED * (ypos ? 1 : -1);

            if (x > mx) {
                x = mx;
                xpos = !xpos;
            }
            if (x < 0) {
                x = 0;
                xpos = !xpos;
            }
            if (y > my) {
                y = my;
                ypos = !ypos;
            }
            if (y < 0) {
                y = 0;
                ypos = !ypos;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            _spriteBatch.Draw(dvd, new Vector2(x, y), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
