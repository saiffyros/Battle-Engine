using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Battle_Engine
{
    public class Animation
    {
        public Texture2D sprite;
        public int Colunas;
        public int Linhas;
        public int Largura;
        public int Altura;
        public int NumeroAnimations;
        public List<Rectangle> ListaRetangulos;
        int Duration;
        bool Loop;
        float Cronometro;
        public int FrameAtual;
        public bool Ativa;
        private Rectangle Position = new Rectangle(0, 0, 0, 0);
        public bool Drawing = false;

        public Animation(Texture2D texture, int largura, int altura, int duration, bool looping)
        {
            sprite = texture;
            Largura = largura;
            Altura = altura;
            Loop = looping;
            Duration = duration;
            Cronometro = 0;
            FrameAtual = 0;
            Ativa = false;
            ListaRetangulos = new List<Rectangle>();

            Colunas = texture.Width / largura;
            Linhas = texture.Height / altura;

            NumeroAnimations = Colunas * Linhas;

            for (int y = 0; y < Linhas; y++)
            {
                for (int x = 0; x < Colunas; x++)
                {
                    ListaRetangulos.Add(new Rectangle(x * Largura, y * Altura, Largura, Altura));
                }
            }
        }

        public Animation(Texture2D texture, int largura, int altura, int duration, bool looping, Rectangle position, bool drawing)
        {
            sprite = texture;
            Largura = largura;
            Altura = altura;
            Loop = looping;
            Duration = duration;
            Cronometro = 0;
            FrameAtual = 0;
            Ativa = false;
            ListaRetangulos = new List<Rectangle>();
            Position = position;
            Drawing = drawing;

            Colunas = texture.Width / largura;
            Linhas = texture.Height / altura;

            NumeroAnimations = Colunas * Linhas;

            for (int y = 0; y < Linhas; y++)
            {
                for (int x = 0; x < Colunas; x++)
                {
                    ListaRetangulos.Add(new Rectangle(x * Largura, y * Altura, Largura, Altura));
                }
            }
        }

        public Animation(Texture2D texture, int col, int lin, int lar, int alt, int dur, bool looping, int animations)
        {
            sprite = texture;
            Colunas = col;
            Linhas = lin;
            Largura = lar;
            Altura = alt;
            Loop = looping;
            Duration = dur;
            Cronometro = 0;
            FrameAtual = 0;
            Ativa = false;
            ListaRetangulos = new List<Rectangle>();

            Colunas = texture.Width / lar;
            Linhas = texture.Height / alt;

            NumeroAnimations = animations;

            for (int x = 0; x < Linhas; x++)
            {
                ListaRetangulos.Add(new Rectangle(col * Largura, Altura * x, Largura, Altura));
            }
        }


        public void Update(GameTime gameTime)
        {
            if (Ativa)
            {
                Cronometro += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (Cronometro > Duration)
                {
                    if (FrameAtual == NumeroAnimations - 1)
                    {
                        if (Loop)
                        {
                            FrameAtual = 0;
                        }
                        else
                        {
                            Ativa = false;
                        }
                    }
                    else
                    {
                        FrameAtual += 1;

                    }
                    Cronometro = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            if (Drawing == true)
            {
                spriteBatch.Draw(sprite, (Rectangle)Position, ListaRetangulos[FrameAtual], Color.White);
            }

        }
    }
}

