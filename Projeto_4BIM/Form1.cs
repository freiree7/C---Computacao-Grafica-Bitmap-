
//*Colegio Técnico Antônio Teixeira Fernandes (Univap)
// * Curso Técnico em Informática - Data de Entrega: 11 / 09 / 2024
// * Autores do Projeto:Felipe Freire Rodrigues de Oliveira
// *                    José Antonio de Oliveira Rosa
// * Turma: 3°H
// * Atividade Projeto 3 Bimestre
// * Observação: < colocar se houver>
// * 
// * 
// * ******************************************************************/

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Projeto_4BIM
{
    public partial class Form1 : Form
    {
 
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Invalidate();
            pictureBox1.Load(@"C:\Imagens\A.jpg");
            pictureBox2.Load(@"C:\Imagens\B.jpg");
            pictureBox3.Load(@"C:\Imagens\C.jpg");
            pictureBox4.Load(@"C:\Imagens\D.jpg");
        }

        public Color Cores(int a,int r, int g , int b)
        {
            return Color.FromArgb(a,r, g, b);
        }

        private Bitmap CombinarImagens(Bitmap imagemBase, Bitmap imgB, Bitmap imgC, Bitmap imgD)
        {
            Bitmap imagemBaseCopia = new Bitmap(imagemBase);

            imagemBaseCopia = SobreporImagem(imagemBaseCopia, imgB, new Point(imagemBaseCopia.Width - imgB.Width - 300, imagemBaseCopia.Height / 3 - imgB.Height / 2 - 150));
            imagemBaseCopia = SobreporImagem(imagemBaseCopia, imgC, new Point(imagemBaseCopia.Width / 3 - imgC.Width, imagemBaseCopia.Height * 2 / 3 - imgC.Height / 2 + 90));
            imagemBaseCopia = SobreporImagem(imagemBaseCopia, imgD, new Point(imagemBaseCopia.Width / 3 - imgD.Width / 2 - 70, imagemBaseCopia.Height / 3 - imgD.Height / 2 - 200));

            return imagemBaseCopia;
        }





        private Bitmap SobreporImagem(Bitmap imagemBase, Bitmap sobreposicao, Point posicao)
        {
            Bitmap imagemResultado = new Bitmap(imagemBase.Width, imagemBase.Height);

            for (int x = 0; x < imagemBase.Width; x++)
            {
                for (int y = 0; y < imagemBase.Height; y++)
                {
                    if (x >= posicao.X && x < posicao.X + sobreposicao.Width && y >= posicao.Y && y < posicao.Y + sobreposicao.Height)
                    {
                        Color corSobreposicao = sobreposicao.GetPixel(x - posicao.X, y - posicao.Y);
                        if (corSobreposicao.A > 0)
                            imagemResultado.SetPixel(x, y, corSobreposicao); 
                        else
                            imagemResultado.SetPixel(x, y, imagemBase.GetPixel(x, y));
                    }
                    else
             
                        imagemResultado.SetPixel(x, y, imagemBase.GetPixel(x, y));
                    
                }
            }

            return imagemResultado;
        }


        private Bitmap RemoverFundo(string caminhoImagem, int tolerancia)
        {
            Bitmap img = new Bitmap(caminhoImagem);
            Color corFundo = img.GetPixel(0, 0);
            if (caminhoImagem == @"C:\Imagens\Balao.jpg")
           
  
                corFundo = Cores(255,84, 144, 205);

            
           
            Bitmap novaImagem = new Bitmap(img.Width, img.Height);

            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    Color pixel = img.GetPixel(x, y);

                    if (CorDentroTolerancia(pixel, corFundo, tolerancia))

                        novaImagem.SetPixel(x, y, Cores(0, 255, 255, 255));
                    
                    else
               
                        novaImagem.SetPixel(x, y, pixel);
                    
                }
            }

            return novaImagem;
        }


        private bool CorDentroTolerancia(Color pixel, Color corFundo, int tolerancia)
        {
            return Math.Abs(pixel.R - corFundo.R) <= tolerancia &&
                   Math.Abs(pixel.G - corFundo.G) <= tolerancia &&
                   Math.Abs(pixel.B - corFundo.B) <= tolerancia;
        }



        private Bitmap AjustarBrilho(string caminhoImagem, int brilho)
        {
            Bitmap img = new Bitmap(caminhoImagem);
            Bitmap imagemComBrilho = new Bitmap(img.Width, img.Height);

            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    Color pixel = img.GetPixel(x, y);

                    int r = Math.Min(Math.Max(pixel.R + brilho, 0), 255);
                    int g = Math.Min(Math.Max(pixel.G + brilho, 0), 255);
                    int b = Math.Min(Math.Max(pixel.B + brilho, 0), 255);

                    imagemComBrilho.SetPixel(x, y, Cores(255,r, g, b));
                }
            }

            return imagemComBrilho;
        }





        private Bitmap GreyLevel(string caminhoImagem)
        {
            Bitmap img = new Bitmap(caminhoImagem);
            int largura = img.Width;
            int altura = img.Height;

            Bitmap imagemCinza = new Bitmap(largura, altura);

            for (int x = 0; x < largura; x++)
            {
                for (int y = 0; y < altura; y++)
                {
                    Color pixel = img.GetPixel(x, y);
                    int valorCinza = (int)(pixel.R * 0.3 + pixel.G * 0.59 + pixel.B * 0.11);
                    imagemCinza.SetPixel(x, y, Cores(255, valorCinza, valorCinza, valorCinza));
                }
            }

            return imagemCinza;
        }


        private Bitmap RotacionarImagem(string caminhoImagem)
        {
            Bitmap img = new Bitmap(caminhoImagem);
            int largura = img.Height;
            int altura = img.Width;

            Bitmap imagemRotacionada = new Bitmap(largura, altura);

            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    imagemRotacionada.SetPixel(y, img.Width - 1 - x, img.GetPixel(x, y));
                }
            }

            return imagemRotacionada;
        }




        private Bitmap Limiarizacao(string caminhoImagem, int thresholding)
        {
            Bitmap img = new Bitmap(caminhoImagem);
            Bitmap imagemLimiarizada = new Bitmap(img.Width, img.Height);

            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    Color pixel = img.GetPixel(x, y);
                    int valorCinza = pixel.R;  

                    Color novaCor = valorCinza >= thresholding ? Cores(255, 255, 255, 255) : Cores(255, 0, 0, 0);
                    imagemLimiarizada.SetPixel(x, y, novaCor);
                }
            }

            return imagemLimiarizada;
        }




        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap imagemBSemFundo = RemoverFundo(@"C:\Imagens\B.jpg",20);
            Bitmap imagemCSemFundo = RemoverFundo(@"C:\Imagens\C.jpg", 8);
            Bitmap imagemDSemFundo = RemoverFundo(@"C:\Imagens\D.jpg", 5);

            Bitmap imagemBase = new Bitmap(@"C:\Imagens\A.jpg");
            Bitmap imagemResultado = CombinarImagens(imagemBase, imagemBSemFundo, imagemCSemFundo, imagemDSemFundo);
            imagemResultado.Save(@"C:\Imagens\E_Colorida.jpg");

            Bitmap imagemComBrilho = AjustarBrilho(@"C:\Imagens\E_Colorida.jpg", 85);
            imagemComBrilho.Save(@"C:\Imagens\E_Brilho85.jpg");

            Bitmap imagemCinza = GreyLevel(@"C:\Imagens\E_Colorida.jpg");
            imagemCinza.Save(@"C:\Imagens\E_Cinza.jpg");

            Bitmap imagemRotacionada90 = RotacionarImagem(@"C:\Imagens\E_Brilho85.jpg");
            imagemRotacionada90.Save(@"C:\Imagens\E_Rotacionada90.jpg");

            Bitmap imagemLimiarizada = Limiarizacao(@"C:\Imagens\E_Cinza.jpg", 126);
            imagemLimiarizada.Save(@"C:\Imagens\E_Limiarizada.jpg");

            
        }


    }
}