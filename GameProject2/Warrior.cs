using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject2.Collisions;

namespace GameProject2
{
    public enum Direction
    {
        Up = 0,
        Left = 1,
        Down = 2,
        Right = 3
    }

    /// <summary>
    /// enumeration of actions
    /// </summary>
    public enum Action
    {
        Stand,
        Walk,
        Bow,
        Slash
    }

    /// <summary>
    /// class representing the warrior sprite
    /// </summary>
    public class Warrior
    {
        private KeyboardState keyboardState;

        private Texture2D body;
        private Texture2D feet;
        private Texture2D head1;
        private Texture2D head2;
        private Texture2D legs;
        private Texture2D torso1;
        private Texture2D torso2;
        private Texture2D belt;

        private Texture2D bowBody;
        private Texture2D bowFeet;
        private Texture2D bowHead1;
        private Texture2D bowHead2;
        private Texture2D bowLegs;
        private Texture2D bowTorso1;
        private Texture2D bowTorso2;
        private Texture2D bowBelt;
        private Texture2D bowWeapon1;
        private Texture2D bowWeapon2;

        private Texture2D slashBody;
        private Texture2D slashFeet;
        private Texture2D slashHead1;
        private Texture2D slashHead2;
        private Texture2D slashLegs;
        private Texture2D slashTorso1;
        private Texture2D slashTorso2;
        private Texture2D slashBelt;
        private Texture2D slashWeapon;

        private double animationTimer;
        private short animationFrame = 0;

        /// <summary>
        /// action of the warrior
        /// </summary>
        public Action action;

        /// <summary>
        /// direction of the sprite
        /// </summary>
        public Direction Direction = Direction.Down;

        /// <summary>
        /// position of the sprite
        /// </summary>
        public Vector2 Position = new Vector2(350, 350);


        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(350, 350), 64, 64);

        /// <summary>
        /// The bounding volume of the sprite
        /// </summary>
        public BoundingRectangle Bounds => bounds;

        /// <summary>
        /// loads the knight texture
        /// </summary>
        /// <param name="content">the ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            body = content.Load<Texture2D>("walkcycle/BODY_male");
            feet = content.Load<Texture2D>("walkcycle/FEET_shoes_brown");
            head1 = content.Load<Texture2D>("walkcycle/HEAD_chain_armor_hood");
            head2 = content.Load<Texture2D>("walkcycle/HEAD_chain_armor_helmet");
            legs = content.Load<Texture2D>("walkcycle/LEGS_pants_greenish");
            torso1 = content.Load<Texture2D>("walkcycle/TORSO_chain_armor_torso");
            torso2 = content.Load<Texture2D>("walkcycle/TORSO_chain_armor_jacket_purple");
            belt = content.Load<Texture2D>("walkcycle/BELT_leather");

            slashBody = content.Load<Texture2D>("slash/BODY_human");
            slashFeet = content.Load<Texture2D>("slash/FEET_shoes_brown");
            slashHead1 = content.Load<Texture2D>("slash/HEAD_chain_armor_hood");
            slashHead2 = content.Load<Texture2D>("slash/HEAD_chain_armor_helmet");
            slashLegs = content.Load<Texture2D>("slash/LEGS_pants_greenish");
            slashTorso1 = content.Load<Texture2D>("slash/TORSO_chain_armor_torso");
            slashTorso2 = content.Load<Texture2D>("slash/TORSO_chain_armor_jacket_purple");
            slashBelt = content.Load<Texture2D>("slash/BELT_leather");
            slashWeapon = content.Load<Texture2D>("slash/WEAPON_dagger");
        }

        /// <summary>
        /// updates the sprite's position based on user input
        /// </summary>
        /// <param name="gameTime">the game time</param>
        public void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left) && keyboardState.IsKeyDown(Keys.Down))
            {
                Position += new Vector2(-.5f, .5f);
                Direction = Direction.Left;
                action = Action.Walk;
            }
            else if (keyboardState.IsKeyDown(Keys.Left) && keyboardState.IsKeyDown(Keys.Up))
            {
                Position += new Vector2(-.5f, -.5f);
                Direction = Direction.Left;
                action = Action.Walk;
            }
            else if (keyboardState.IsKeyDown(Keys.Right) && keyboardState.IsKeyDown(Keys.Down))
            {
                Position += new Vector2(.5f, .5f);
                Direction = Direction.Right;
                action = Action.Walk;
            }
            else if (keyboardState.IsKeyDown(Keys.Right) && keyboardState.IsKeyDown(Keys.Up))
            {
                Position += new Vector2(.5f, -.5f);
                Direction = Direction.Right;
                action = Action.Walk;
            }
            else if (keyboardState.IsKeyDown(Keys.Up))
            {
                Position += new Vector2(0, -1);
                Direction = Direction.Up;
                action = Action.Walk;
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                Position += new Vector2(0, 1);
                Direction = Direction.Down;
                action = Action.Walk;
            }
            else if (keyboardState.IsKeyDown(Keys.Left))
            {
                Position += new Vector2(-1, 0);
                Direction = Direction.Left;
                action = Action.Walk;
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                Position += new Vector2(1, 0);
                Direction = Direction.Right;
                action = Action.Walk;
            }
            else if (keyboardState.IsKeyDown(Keys.Space))
            {
                action = Action.Slash;
            }
            else
            {
                action = Action.Stand;
            }

            bounds.X = Position.X;
            bounds.Y = Position.Y;
        }

        /// <summary>
        /// draws the sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">the game time</param>
        /// <param name="spriteBatch">the spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            switch (action)
            {
                case Action.Stand:
                    var source = new Rectangle(1, (int)Direction * 64, 64, 64);
                    spriteBatch.Draw(body, Position, source, Color.White);
                    spriteBatch.Draw(feet, Position, source, Color.White);
                    spriteBatch.Draw(legs, Position, source, Color.White);
                    spriteBatch.Draw(torso1, Position, source, Color.White);
                    spriteBatch.Draw(torso2, Position, source, Color.White);
                    spriteBatch.Draw(belt, Position, source, Color.White);
                    spriteBatch.Draw(head1, Position, source, Color.White);
                    spriteBatch.Draw(head2, Position, source, Color.White);
                    break;

                case Action.Walk:
                    //update animation timer
                    animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

                    //update animation frame
                    if (animationTimer > 0.1)
                    {
                        animationFrame++;
                        if (animationFrame > 8) animationFrame = 1;
                        animationTimer -= 0.1;
                    }

                    //draw the sprite
                    source = new Rectangle(animationFrame * 64, (int)Direction * 64, 64, 64);
                    spriteBatch.Draw(body, Position, source, Color.White);
                    spriteBatch.Draw(feet, Position, source, Color.White);
                    spriteBatch.Draw(legs, Position, source, Color.White);
                    spriteBatch.Draw(torso1, Position, source, Color.White);
                    spriteBatch.Draw(torso2, Position, source, Color.White);
                    spriteBatch.Draw(belt, Position, source, Color.White);
                    spriteBatch.Draw(head1, Position, source, Color.White);
                    spriteBatch.Draw(head2, Position, source, Color.White);
                    break;

                case Action.Slash:
                    animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
                    if (animationTimer > 0.1)
                    {
                        animationFrame++;
                        if (animationFrame > 5)
                        {
                            animationFrame = 0;
                        } 
                        animationTimer -= 0.1;
                    }

                    source = new Rectangle(animationFrame * 64, (int)Direction * 64, 64, 64);
                    spriteBatch.Draw(slashBody, Position, source, Color.White);
                    spriteBatch.Draw(slashFeet, Position, source, Color.White);
                    spriteBatch.Draw(slashLegs, Position, source, Color.White);
                    spriteBatch.Draw(slashTorso1, Position, source, Color.White);
                    spriteBatch.Draw(slashTorso2, Position, source, Color.White);
                    spriteBatch.Draw(slashBelt, Position, source, Color.White);
                    spriteBatch.Draw(slashHead1, Position, source, Color.White);
                    spriteBatch.Draw(slashHead2, Position, source, Color.White);
                    spriteBatch.Draw(slashWeapon, Position, source, Color.White);
                    break;

                case Action.Bow:
                    animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

                    if (animationTimer > 0.1)
                    {
                        animationFrame++;
                        if (animationFrame > 10)
                        {
                            animationFrame = 1;
                        }
                        animationTimer -= 0.1;
                    }

                    source = new Rectangle(animationFrame * 64, (int)Direction * 64, 64, 64);
                    spriteBatch.Draw(bowBody, Position, source, Color.White);
                    spriteBatch.Draw(bowFeet, Position, source, Color.White);
                    spriteBatch.Draw(bowLegs, Position, source, Color.White);
                    spriteBatch.Draw(bowTorso1, Position, source, Color.White);
                    spriteBatch.Draw(bowTorso2, Position, source, Color.White);
                    spriteBatch.Draw(bowBelt, Position, source, Color.White);
                    spriteBatch.Draw(bowHead1, Position, source, Color.White);
                    spriteBatch.Draw(bowHead2, Position, source, Color.White);
                    spriteBatch.Draw(bowWeapon1, Position, source, Color.White);
                    spriteBatch.Draw(bowWeapon2, Position, source, Color.White);
                    
                    break;
            }
        }
    }
}
