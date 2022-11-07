namespace NumerazioneProtocollo
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_search = new System.Windows.Forms.TextBox();
            this.numericUpDown_search_anno = new System.Windows.Forms.NumericUpDown();
            this.listBox_cat = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_cat = new System.Windows.Forms.TextBox();
            this.button_cat_elimina = new System.Windows.Forms.Button();
            this.button_cat_modifica = new System.Windows.Forms.Button();
            this.button_cat_crea = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button_doc_ricarica = new System.Windows.Forms.Button();
            this.button_doc_elimina = new System.Windows.Forms.Button();
            this.dataGridView_doc = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_search_anno)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_doc)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox_search
            // 
            this.textBox_search.Location = new System.Drawing.Point(16, 22);
            this.textBox_search.Name = "textBox_search";
            this.textBox_search.Size = new System.Drawing.Size(302, 23);
            this.textBox_search.TabIndex = 0;
            this.textBox_search.TextChanged += new System.EventHandler(this.TextBox_search_TextChanged);
            // 
            // numericUpDown_search_anno
            // 
            this.numericUpDown_search_anno.Location = new System.Drawing.Point(58, 71);
            this.numericUpDown_search_anno.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown_search_anno.Name = "numericUpDown_search_anno";
            this.numericUpDown_search_anno.Size = new System.Drawing.Size(120, 23);
            this.numericUpDown_search_anno.TabIndex = 1;
            this.numericUpDown_search_anno.ValueChanged += new System.EventHandler(this.NumericUpDown_search_anno_ValueChanged);
            // 
            // listBox_cat
            // 
            this.listBox_cat.FormattingEnabled = true;
            this.listBox_cat.ItemHeight = 15;
            this.listBox_cat.Location = new System.Drawing.Point(6, 22);
            this.listBox_cat.Name = "listBox_cat";
            this.listBox_cat.Size = new System.Drawing.Size(324, 199);
            this.listBox_cat.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox_cat);
            this.groupBox1.Controls.Add(this.button_cat_elimina);
            this.groupBox1.Controls.Add(this.button_cat_modifica);
            this.groupBox1.Controls.Add(this.button_cat_crea);
            this.groupBox1.Controls.Add(this.listBox_cat);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 283);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Categorie";
            // 
            // textBox_cat
            // 
            this.textBox_cat.Location = new System.Drawing.Point(6, 227);
            this.textBox_cat.Name = "textBox_cat";
            this.textBox_cat.Size = new System.Drawing.Size(324, 23);
            this.textBox_cat.TabIndex = 6;
            // 
            // button_cat_elimina
            // 
            this.button_cat_elimina.Location = new System.Drawing.Point(255, 254);
            this.button_cat_elimina.Name = "button_cat_elimina";
            this.button_cat_elimina.Size = new System.Drawing.Size(75, 23);
            this.button_cat_elimina.TabIndex = 5;
            this.button_cat_elimina.Text = "Elimina";
            this.button_cat_elimina.UseVisualStyleBackColor = true;
            // 
            // button_cat_modifica
            // 
            this.button_cat_modifica.Location = new System.Drawing.Point(132, 254);
            this.button_cat_modifica.Name = "button_cat_modifica";
            this.button_cat_modifica.Size = new System.Drawing.Size(75, 23);
            this.button_cat_modifica.TabIndex = 4;
            this.button_cat_modifica.Text = "Modifica";
            this.button_cat_modifica.UseVisualStyleBackColor = true;
            // 
            // button_cat_crea
            // 
            this.button_cat_crea.Location = new System.Drawing.Point(6, 254);
            this.button_cat_crea.Name = "button_cat_crea";
            this.button_cat_crea.Size = new System.Drawing.Size(75, 23);
            this.button_cat_crea.TabIndex = 3;
            this.button_cat_crea.Text = "Crea";
            this.button_cat_crea.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textBox_search);
            this.groupBox2.Controls.Add(this.numericUpDown_search_anno);
            this.groupBox2.Location = new System.Drawing.Point(18, 312);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(324, 126);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ricerca";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Anno";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button_doc_ricarica);
            this.groupBox3.Controls.Add(this.button_doc_elimina);
            this.groupBox3.Controls.Add(this.dataGridView_doc);
            this.groupBox3.Location = new System.Drawing.Point(354, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(771, 426);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Documenti";
            // 
            // button_doc_ricarica
            // 
            this.button_doc_ricarica.Location = new System.Drawing.Point(531, 394);
            this.button_doc_ricarica.Name = "button_doc_ricarica";
            this.button_doc_ricarica.Size = new System.Drawing.Size(75, 23);
            this.button_doc_ricarica.TabIndex = 4;
            this.button_doc_ricarica.Text = "Ricarica";
            this.button_doc_ricarica.UseVisualStyleBackColor = true;
            this.button_doc_ricarica.Click += new System.EventHandler(this.Button_doc_ricarica_Click);
            // 
            // button_doc_elimina
            // 
            this.button_doc_elimina.Location = new System.Drawing.Point(17, 394);
            this.button_doc_elimina.Name = "button_doc_elimina";
            this.button_doc_elimina.Size = new System.Drawing.Size(75, 23);
            this.button_doc_elimina.TabIndex = 3;
            this.button_doc_elimina.Text = "Elimina";
            this.button_doc_elimina.UseVisualStyleBackColor = true;
            this.button_doc_elimina.Click += new System.EventHandler(this.Button_doc_elimina_Click);
            // 
            // dataGridView_doc
            // 
            this.dataGridView_doc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_doc.Location = new System.Drawing.Point(6, 22);
            this.dataGridView_doc.Name = "dataGridView_doc";
            this.dataGridView_doc.RowTemplate.Height = 25;
            this.dataGridView_doc.Size = new System.Drawing.Size(759, 366);
            this.dataGridView_doc.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1137, 450);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "Numero Protocollo - PoliNetwork";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_search_anno)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_doc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TextBox textBox_search;
        private NumericUpDown numericUpDown_search_anno;
        private ListBox listBox_cat;
        private GroupBox groupBox1;
        private TextBox textBox_cat;
        private Button button_cat_elimina;
        private Button button_cat_modifica;
        private Button button_cat_crea;
        private GroupBox groupBox2;
        private Label label1;
        private GroupBox groupBox3;
        private Button button_doc_elimina;
        private DataGridView dataGridView_doc;
        private Button button_doc_ricarica;
    }
}