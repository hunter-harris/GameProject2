using GameProject2.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameProject2
{
    /// <summary>
    /// a class representing a skeleton
    /// </summary>
    public class Skeleton
    {
        private Texture2D skeleton;
        private Texture2D deadSkeleton;

        private double animationTimer;
        private short animationFrame = 0;

        private Vector2 position;

        private BoundingRectangle bounds;

        /// <summary>
        /// whether the skeleton is alive or not
        /// </summary>
        public bool Alive { get; set; } = true;

        /// <summary>
        /// The bounding volume of the sprite
        /// </summary>
        public BoundingRectangle Bounds => bounds;

        /// <summary>
        /// Creates a new skeleton sprite
        /// </summary>
        /// <param name="position">The position of the sprite in the game</param>
        public Skeleton(Vector2 position)
        {
            this.position = position;
            this.bounds = new BoundingRectangle(position, 64, 64);
        }

        /// <summary>
        /// loads the skeleton texture
        /// </summary>
        /// <param name="content">the ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            skeleton = content.Load<Texture2D>("walkcycle/BODY_skeleton");
            deadSkeleton = content.Load<Texture2D>("hurt/BODY_skeleton");
        }

        /// <summary>
        /// Draws the animated sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Alive)
            {
                var source = new Rectangle(0, 128, 64, 64);
                spriteBatch.Draw(skeleton, position, source, Color.White);
            }
            else
            {
                animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (animationTimer > .2)
                {
                    animationFrame++;
                    if (animationFrame > 5)
                    {
                        animationFrame = 5;
                    }
                    animationTimer -= .2;
                }
                var source = new Rectangle(animationFrame * 64, 0, 64, 64);
                spriteBatch.Draw(deadSkeleton, position, source, Color.White);
            }
        }
    }
}
