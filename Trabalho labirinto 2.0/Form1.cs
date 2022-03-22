using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trabalho_labirinto_2._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            numericUpDown1.Value = 3;
        }
        int contador2; bool resolver = false; int L_inicial;int C_inicial; bool resolve; 
        Random random = new Random(); int tentativas; int casasPercorridas;  List<int> caminhoCerto = new List<int>();//removi o public
        public int lin = 1; public int col = 1; public int count; public int tam; public List<int> PosPercorr = new List<int>(); public int escala;
        public int posX; int[,] matriz; public int timer; int total; bool voltar = false; int prox; public List<int> verificados = new List<int>();
        private void DesenharQuadrados(Color cor)
        {
            SolidBrush Solid = new SolidBrush(cor);
            Graphics Draw = pictureBox1.CreateGraphics();
            for (int coluna = 0; coluna < numericUpDown1.Value * escala; coluna += escala)
            {
                for (int linha = 0; linha < numericUpDown1.Value * escala; linha += escala)
                {
                    Draw.FillRectangle(Solid, linha, coluna, 1, linha);//colunas
                    Draw.FillRectangle(Solid, linha, coluna, coluna, 1);//linhas
                }
            }
        }
        private void DesenharAtual(int linha, int coluna, Color cor, int tamanho, int direcao)
        {
            SolidBrush Solid = new SolidBrush(cor);
            Graphics Draw = pictureBox1.CreateGraphics();
            Draw.FillRectangle( Solid, coluna + 1, linha + 1, tamanho, tamanho);
            if (direcao == 1) { Draw.FillRectangle(Solid, coluna + 1, linha - 2, tamanho, tamanho); }//   ↓
            else if (direcao == 2) { Draw.FillRectangle(Solid, coluna - 2, linha + 1, tamanho, tamanho); }//   →
            else if (direcao == 3) { Draw.FillRectangle(Solid, coluna + 1, linha + 2, tamanho, tamanho); }//   ↑
            else if (direcao == 4) { Draw.FillRectangle(Solid, coluna + 2, linha + 1, tamanho, tamanho); }//   ← 
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            tam = Convert.ToInt32(numericUpDown1.Value);
            matriz = new int[tam, tam];
        }
        private void VoltarCasa(int contador)
        {

            if (voltar == true)
            {
                void voltar_(int lin, int col)
                {
                    count--;
                    contador--;
                    matriz[lin, col] = 2;
                    verificados.RemoveAt(contador);
                    caminhoCerto.RemoveAt(contador); //-1 
                }
                if (contador - 1 > 0)
                {
                    if (verificados[contador - 1] == 1 & lin - 1 >= 0)
                    {
                        Console.WriteLine("• 1 ");
                        if (matriz[lin - 1, col] == 1 || matriz[lin - 1, col] == 2)
                        {
                            lin--; Console.WriteLine("tentando voltar [1]"); voltar_(lin, col);
                        }
                    }
                }
                if (contador - 1 > 0)
                {
                    if (verificados[contador - 1] == 2 & col - 1 >= 0)
                    {
                        Console.WriteLine("• 2 ");
                        if (matriz[lin, col - 1] == 1 || matriz[lin, col - 1] == 2)
                        {
                            col--; Console.WriteLine("tentando voltar [2]"); voltar_(lin, col);
                        }
                    }
                }
                if (contador - 1 > 0)
                {
                    if (verificados[contador - 1] == 3 & lin + 1 < tam)
                    {
                        Console.WriteLine("• 3");
                        if (matriz[lin + 1, col] == 1 || matriz[lin + 1, col] == 2)
                        {
                            lin++; Console.WriteLine("tentando voltar [3]"); voltar_(lin, col);
                        }
                    }
                }
                if (contador - 1 > 0)
                {
                    if (verificados[contador - 1] == 4 & col + 1 < tam)
                    {
                        Console.WriteLine("• 4 ");
                        if (matriz[lin, col + 1] == 1 || matriz[lin, col + 1] == 2)
                        {
                            col++; Console.WriteLine("tentando voltar [4]"); voltar_(lin, col);
                        }
                    }
                }
                if (contador - 1 <= 0 || contador2 == total & resolver == false)
                {
                    resolver = true;
                    Console.WriteLine(@"\\\ FIM ///");
                    timer1.Dispose();
                    return;
                }
                void ZeroEncontrado()
                {
                    voltar = false;
                    casasPercorridas = 0; tentativas = 0; PosPercorr.Clear();
                    AvancarCasa();
                }

                if (lin - 1 >= 0)
                {
                    if (matriz[lin - 1, col] == 0)
                    {
                         ZeroEncontrado();

                    }
                }
                if (col - 1 >= 0)
                {
                    if (matriz[lin, col - 1] == 0)
                    {
                        ZeroEncontrado();
                    }
                }
                if (lin + 1 < tam)
                {
                    if (matriz[lin + 1, col] == 0)
                    {
                        ZeroEncontrado();
                    }
                }
                if (col + 1 < tam)
                {
                    if (matriz[lin, col + 1] == 0)
                    {
                        ZeroEncontrado();
                    }
                }
            }
        }
        private void AvancarCasa()
        {
            total = tam * tam;
            if (voltar == false)
            {
                void ocuparCasa(int prox, int lin, int col)
                {
                    count++;
                    casasPercorridas++;
                    matriz[lin, col] = 1;
                    verificados.Add(prox);
                    caminhoCerto.Add(prox);                    
                    contador2++;
                    if (contador2 == 1) //primeira casa
                    {
                        DesenharAtual(lin * escala, col * escala, Color.NavajoWhite, escala - 1, 0); //com escala   
                        DesenharEntrada(lin * escala, col * escala, Color.ForestGreen, escala / 2); //entrada
                        L_inicial = lin; C_inicial = col;
                    }
                    else
                    {
                        DesenharAtual(lin * escala, col * escala, Color.NavajoWhite, escala - 1, prox); //com escala   
                    }
                }
                prox = random.Next(1, 5);
                if (!PosPercorr.Contains(prox))
                {
                    Console.WriteLine("Entrou em numeros sem repetir");
                    PosPercorr.Add(prox);
                    if (prox == 1 & lin + 1 < tam)
                    {
                        if (matriz[lin + 1, col] == 0)
                        {
                            lin++; ocuparCasa(prox, lin, col); //richDireita.AppendText(" ↓ ");
                        }
                    }
                    if (prox == 2 & col + 1 < tam)
                    {
                        if (matriz[lin, col + 1] == 0)
                        {
                            col++; ocuparCasa(prox, lin, col); //richDireita.AppendText(" → ");
                        }
                    }
                    if (prox == 3 & lin - 1 >= 0)
                    {
                        if (matriz[lin - 1, col] == 0)
                        {
                            lin--; ocuparCasa(prox, lin, col); //richDireita.AppendText(" ↑ ");
                        }
                    }
                    if (prox == 4 & col - 1 >= 0)
                    {
                        if (matriz[lin, col - 1] == 0)
                        {
                            col--; ocuparCasa(prox, lin, col); //richDireita.AppendText(" ← ");
                        }
                    }
                    
                    tentativas++;
                }
                if (tentativas == 4 & casasPercorridas == 0) //se tentar as 4 possiveis direções e nao conseguir andar 1 casa, está travado.
                {
                    timer = 0;
                    voltar = true;
                    Console.WriteLine("Travado! : " + casasPercorridas);
                    casasPercorridas = 0;
                    tentativas = 0;
                }
                if (casasPercorridas != 0)
                {
                    Console.WriteLine("Celula encontrada... " + Environment.NewLine);
                    casasPercorridas = 0;
                    tentativas = 0;
                    PosPercorr.Clear();
                }
            }
        }
        private void BtnIniciar_Click(object sender, EventArgs e)
        {
            if (tam <= 200)
            {
                escala = pictureBox1.Width / tam;
                pictureBox1.Height = pictureBox1.Width = Convert.ToInt32(numericUpDown1.Value * escala);
                DesenharQuadrados(Color.DarkGoldenrod);
            }
            AvancarCasa(); timer1.Start();
        }
        public int teste2 = 0;
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (resolve == false)
            {
                label1.Text = "Total: " + total + " Percorridas: " + contador2;
                total = tam * tam;
                if (checkBox1.Checked)
                {
                    timer1.Interval = 200;
                }
                else
                {
                    timer1.Interval = 1;
                }
                timer++;
                if (voltar == false)
                {
                    AvancarCasa();
                }
                else
                {
                    VoltarCasa(count);
                }
                if(resolver == true) //fim
                {
                    DesenharSaida(0, 0, Color.Purple, escala / 2);
                    matriz[L_inicial, C_inicial] = 3; 
                }
            } 
        }
        bool pause = false;
        private void BtnPause_Click(object sender, EventArgs e)
        {
            if (pause == false)
            {
                timer1.Stop();
            }
            if (pause == true)
            {
                timer1.Start();
            }
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < matriz.Length; i++)
            {
                for (int j = 0; j < matriz.Length; j++)
                {
                    matriz[i, j] = 0;
                }
            }
            tentativas = 0; casasPercorridas = 0;  contador2 = 0;
            lin = 1; col = 1; count = 0; tam = 0; ; PosPercorr.Clear(); escala = 0;
            posX = 0; ; timer = 0; total = 0; voltar = false; prox = 0; ; verificados.Clear();
        }
        private void DesenharEntrada(int linha, int coluna, Color cor, int tamanho)
        {
            SolidBrush Solid = new SolidBrush(cor);
            Graphics Draw = pictureBox1.CreateGraphics();
            Draw.FillEllipse(Solid, coluna + 1, linha + 1, tamanho, tamanho);
        }
        private void DesenharSaida(int linha, int coluna, Color cor, int tamanho)
        {
            void desenhar(int lin, int col)
            {
                SolidBrush Solid = new SolidBrush(cor);
                Graphics Draw = pictureBox1.CreateGraphics();
                matriz[lin, col] = 4; resolver = false;
                Draw.FillEllipse(Solid, col * escala, lin * escala, tamanho, tamanho);
            }
            if (resolver == true)
            {
                prox = random.Next(1, 3);
                if (prox == 1) //gerar saida na direita
                {
                    prox = random.Next(tam / 2, tam);
                    desenhar(tam - 1, prox);
                }
                if (prox == 2) //gerar saida embaixo
                {
                    prox = random.Next(0, tam);
                    desenhar(prox, tam - 1);
                }
            }
        }
        private void BtnResolver_Click(object sender, EventArgs e)
        {
            timer1.Start();
            resolve = true;
        }
    }
}
