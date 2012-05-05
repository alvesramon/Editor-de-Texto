using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Editor_de_Texto
{
    /// <summary>
    /// Desenvolvido por Lucas S.Wundervald, Ramon Alves.
    /// </summary>


    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();

        }

        //objeto do tipo opem file dialog.
        //usado para abrir ou salvar o txt.
        OpenFileDialog opdialog = new OpenFileDialog();

        //clique na opção abrir do Menu
        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //diz para o metodo filtro qual tipo de arquivo ele pode abrir.
            opdialog.Filter = "Text Files|*.txt";

            //se for selecionado o arquivo correto.
            //faz com que ele só mostre os arquivos de texto tb.
            if (opdialog.ShowDialog() == DialogResult.OK)
            {
                //passa o arquivo de texto para o txtTexto.
                txtTexto.Text = File.ReadAllText(opdialog.FileName);
            }
        }

        //clique na opção salvar do Menu
        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamWriter Writer;

            SaveFileDialog Salvar = new SaveFileDialog();

            //se for um novo arquivo
            if (opdialog.FileName == "")
            {
                //defini o tipo de arquivo
                Salvar.DefaultExt = "*.txt";
                Salvar.Filter = "Text Files|*.txt";
                
                if (Salvar.ShowDialog() == DialogResult.OK)
                {
                    
                    //passa o nome do arquivo para ser salvo
                    Writer = File.CreateText(Salvar.FileName);
                    //escreve no arquivo o texto que foi criado
                    Writer.Write(txtTexto.Text);
                    //para de escrever no arquivo
                    Writer.Close();

                    MessageBox.Show("Novo Arquivo Salvo com sucesso", "Salvo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
            else
            {
                //metodo que salva o arquivo caso ele já esteja aberto.
                File.WriteAllText(opdialog.FileName, txtTexto.Text);

                MessageBox.Show("Arquivo Salvo com sucesso", "Salvo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //limpa o texto
            txtTexto.Clear();
            //limpa o nome do arquivo caso tenha sido aberto algum.
            opdialog.FileName = "";

        }

        //formatar texto selecionado
        private void formatarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Show the dialog.
            FontDialog fontDialog1 = new FontDialog();

            DialogResult result = fontDialog1.ShowDialog();
            // See if OK was pressed.
            if (result == DialogResult.OK)
            {
                // Get Font.
                Font font = fontDialog1.Font;
                // Set TextBox properties.
                txtTexto.SelectionFont = font;
            }
        }
        //botão visualizar pagina
        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //Associa PrintPrev com PrintDocm.
            printPreviewDialog1.Document = printDocument1;

            //Mostra a janela de visualização
            printPreviewDialog1.ShowDialog();

            printPreviewDialog1.Dispose();

        }
        //Configura pagina de impressão
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(txtTexto.Text, txtTexto.Font, Brushes.Black, 100, 20);
            e.Graphics.PageUnit = GraphicsUnit.Inch; 
        }

        //Botão Imprimir
        private void visualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog1.Document = printDocument1;
            string texto = this.txtTexto.Text;
            StringReader  leitor = new StringReader(texto);

            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                this.printDocument1.Print();
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
