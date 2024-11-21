using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("Name: ADITYA RAJ \n Enrollment No: 22SOECE11171");
            OpenFileDialog o = new OpenFileDialog();
            o.Title = "Open a Text File";
            o.Filter = "Text Files|*.txt|All Files|*.*";
            DialogResult dr = o.ShowDialog();
            if (dr == DialogResult.OK)
            {
                TextReader tr = new StreamReader(o.FileName);
                textBox1.Text = tr.ReadToEnd();
                tr.Close();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Title = "Save a File";
            sf.Filter = "Text Files|*.txt|PDF Files|*.pdf|All Files|*.*";
            DialogResult dr = sf.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string extension = Path.GetExtension(sf.FileName);
                if (extension.ToLower() == ".txt")
                {
                    // Save as a text file
                    TextWriter tr = new StreamWriter(sf.FileName);
                    tr.Write(textBox1.Text);
                    tr.Close();
                }
                else if (extension.ToLower() == ".pdf")
                {
                    // Save as a PDF file
                    using (FileStream fs = new FileStream(sf.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        iTextSharp.text.Document doc = new iTextSharp.text.Document();
                        PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                        doc.Open();
                        doc.Add(new iTextSharp.text.Paragraph(textBox1.Text));
                        doc.Close();
                        writer.Close();
                    }
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                DialogResult dr = MessageBox.Show("Do you want to save?", "Confirmation", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    SaveFileDialog sf = new SaveFileDialog();
                    sf.Title = "Save a Text File";
                    sf.Filter = "Text Files|*.txt|PDF Files|*.pdf|All Files|*.*";
                    DialogResult drr = sf.ShowDialog();
                    if (drr == DialogResult.OK)
                    {
                        TextWriter tr = new StreamWriter(sf.FileName);
                        tr.Write(textBox1.Text);
                        tr.Close();
                    }
                    textBox1.Text = "";
                }
                if (dr == DialogResult.Cancel)
                {
                    textBox1.Text = "";
                }
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.SelectedText);
            textBox1.SelectedText = "";
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.SelectedText);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = Clipboard.GetText();
            textBox1.Text = textBox1.Text.Insert(textBox1.SelectionStart, str);
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(textBox1.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new PointF(10, 10));
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            if (printPreviewDialog2.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to exit?", "Confirmation", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void IJ(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
