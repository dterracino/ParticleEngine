#region License Information (GPL v3)

/*
    Copyright (C) Jaex

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v3)

using GameHelpersLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ParticleEngine
{
    public enum ParticleRectangleLimitAction
    {
        None, Kill, Bounce
    }

    public class Particle
    {
        public Texture2D Texture { get; private set; }

        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public float Speed { get; set; }
        public Rectangle RectangleLimit { get; set; }
        public ParticleRectangleLimitAction RectangleLimitAction { get; set; }

        public float Angle { get; set; }
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }
        public bool AutoAngle { get; set; }

        public float Lifetime { get; set; }
        public float Scale { get; set; }
        public Vector3 Color { get; set; }
        public float Opacity { get; set; }

        public float TimeSinceStart { get; private set; }
        public float TimePercentage { get { return TimeSinceStart / Lifetime; } }
        public bool IsActive { get; private set; }
        public Rectangle SourceRectangle { get; private set; }

        public List<IModifier> Modifiers { get; set; }

        public void Initialize(Texture2D texture)
        {
            ChangeTexture(texture);
            RectangleLimit = Rectangle.Empty;
            RectangleLimitAction = ParticleRectangleLimitAction.None;
            Lifetime = 1;
            Scale = 1;
            Opacity = 1;
            Modifiers = new List<IModifier>();
            TimeSinceStart = 0;
            IsActive = true;
        }

        public void ChangeTexture(Texture2D texture)
        {
            Texture = texture;
            Origin = new Vector2((float)Texture.Width / 2, (float)Texture.Height / 2);
            SourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
        }

        public void Update(float deltaTime)
        {
            TimeSinceStart += deltaTime;

            foreach (IModifier modifier in Modifiers)
            {
                modifier.Apply(this);
            }

            IsActive = TimeSinceStart < Lifetime && Scale > 0;

            if (IsActive)
            {
                Position += Direction * Speed * deltaTime;

                CheckRectangleLimit();

                if (!AutoAngle)
                {
                    Angle += Rotation * deltaTime;
                }
                else
                {
                    Angle = MathHelpers.Vector2ToRadian(Direction) + MathHelpers.DegreeToRadian(90);
                }
            }
        }

        public void CheckRectangleLimit()
        {
            if (RectangleLimitAction != ParticleRectangleLimitAction.None && !RectangleLimit.IsEmpty)
            {
                Vector2 properOrigin = new Vector2((float)Texture.Width * Scale / 2, (float)Texture.Height * Scale / 2);
                Vector2 properPosition = Position - properOrigin;
                float properWidth = SourceRectangle.Width * Scale;
                float properHeight = SourceRectangle.Height * Scale;

                if (RectangleLimitAction == ParticleRectangleLimitAction.Kill)
                {
                    if (properPosition.X < -properWidth || properPosition.X > RectangleLimit.Width || properPosition.Y < -properHeight || properPosition.Y > RectangleLimit.Height)
                    {
                        IsActive = false;
                    }
                }
                else if (RectangleLimitAction == ParticleRectangleLimitAction.Bounce)
                {
                    if (properPosition.X <= 0)
                    {
                        Position = new Vector2(properOrigin.X, Position.Y);
                        Direction = new Vector2(-Direction.X, Direction.Y);
                    }
                    else if (properPosition.X >= RectangleLimit.Width - properWidth)
                    {
                        Position = new Vector2(RectangleLimit.Width - properOrigin.X, Position.Y);
                        Direction = new Vector2(-Direction.X, Direction.Y);
                    }

                    if (properPosition.Y <= 0)
                    {
                        Position = new Vector2(Position.X, properOrigin.Y);
                        Direction = new Vector2(Direction.X, -Direction.Y);
                    }
                    else if (properPosition.Y >= RectangleLimit.Height - properHeight)
                    {
                        Position = new Vector2(Position.X, RectangleLimit.Height - properOrigin.Y);
                        Direction = new Vector2(Direction.X, -Direction.Y);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, SourceRectangle, new Color(Color.X, Color.Y, Color.Z, Opacity), Angle, Origin, Scale, SpriteEffects.None, 0);
        }
    }
}