using GameHelpersLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ParticleEngine
{
    public abstract class ParticleSystem : DrawableGameComponent
    {
        public int FreeParticleCount
        {
            get { return freeParticles.Count; }
        }

        private SpriteBatch spriteBatch;

        protected Game game;
        protected BlendState blendState;

        private Particle[] particles;
        private Queue<Particle> freeParticles;
        private int maxNumParticles = 10000;
        protected List<string> textureNames;
        private List<Texture2D> textures;

        public ParticleSystem(Game game)
            : base(game)
        {
            this.game = game;

            particles = new Particle[maxNumParticles];
            freeParticles = new Queue<Particle>(maxNumParticles);
            for (int i = 0; i < particles.Length; i++)
            {
                particles[i] = new Particle();
                freeParticles.Enqueue(particles[i]);
            }

            textureNames = new List<string>();
            textures = new List<Texture2D>();
            blendState = BlendState.Additive;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            foreach (string textureName in textureNames)
            {
                textures.Add(game.Content.Load<Texture2D>(textureName));
            }

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (Particle p in particles)
            {
                if (p.IsActive)
                {
                    p.Update(deltaTime);

                    if (!p.IsActive)
                    {
                        freeParticles.Enqueue(p);
                    }
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, blendState);

            foreach (Particle p in particles)
            {
                if (p.IsActive)
                {
                    p.Draw(spriteBatch);
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected void AddTextureNames(params string[] names)
        {
            foreach (string name in names)
            {
                textureNames.Add(name);
            }
        }

        protected Particle GetFreeParticle()
        {
            if (freeParticles.Count > 0)
            {
                Particle p = freeParticles.Dequeue();
                p.Initialize(textures[MathHelpers.Random.Next(0, textures.Count)]);
                return p;
            }

            return null;
        }

        public abstract void Emit(Vector2 emitterLocation);
    }
}