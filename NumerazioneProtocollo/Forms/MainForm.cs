using Newtonsoft.Json.Linq;
using NumerazioneProtocollo.Model;
using NumerazioneProtocollo.Model.Docs;
using System.Data;

namespace NumerazioneProtocollo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        readonly DataTable dataTable = new();
        private int categoryIdSelected = 0;
        private bool toRefreshDocs = true;

        private void Form1_Load(object sender, EventArgs e)
        {
            numericUpDown_search_anno.Value = DateTime.Now.Year;

            Data.GlobalVariables.docs ??= new Rif<Docs>();
            Data.GlobalVariables.categories ??= new Rif<Model.Cat.Categories>();

            LoadFiles();



            Data.GlobalVariables.docs.obj ??= new Docs();
            Data.GlobalVariables.categories.obj ??= new  Model.Cat.Categories();

            LoadCategories();
            LoadDocuments();
        }

  

        private void LoadDocuments()
        {

            foreach (var docHead in Document.headList)
            {
                dataTable.Columns.Add(docHead.GetName());
            }

            Refresh_docs();

            //dataGridView_doc.RowsAdded += new DataGridViewRowsAddedEventHandler(DocsModified);
            dataGridView_doc.CellEndEdit += new DataGridViewCellEventHandler(CellEditEnded);
            

        }

        private void CellEditEnded(object? sender, DataGridViewCellEventArgs e)
        {
            var row = e.RowIndex;
            RowEdit(row);
            Refresh_docs();
        }

        private void DocsModified(object? sender, DataGridViewRowsAddedEventArgs e)
        {
            RowEdit(e.RowIndex);
        }

        private void RowEdit(int rowIndex)
        {
            DataGridViewRow rowAdded = dataGridView_doc.Rows[rowIndex];

            Data.GlobalVariables.docs ??= new Rif<Docs>();
            Data.GlobalVariables.docs.obj ??= new Docs();
            Data.GlobalVariables.docs.obj.documents ??= new List<Document>();
      


            Document doc = Model.Docs.Document.Get(rowAdded, dataGridView_doc);
           

            if (doc.creationDate == null)
            {
                doc.creationDate = DateTime.Now;

            }

            if (doc.year == null)
            {
                doc.year = (int)numericUpDown_search_anno.Value;

            }

            if (doc.category == null)
            {
                doc.category = 0;
  
            }

            if (doc.id == null)
            {
                int id = GetNewId();
                doc.id = id;
  
            }

            Data.GlobalVariables.docs.obj.HandleEdit(doc);

            Utils.Files.SaveFile(Data.GlobalVariables.docs, Data.Constants.PathDocs);
                    
        }

        private int GetNewId()
        {

            Data.GlobalVariables.docs ??= new Rif<Docs>();
            Data.GlobalVariables.docs.obj ??= new Docs();
            Data.GlobalVariables.docs.obj.documents ??= new List<Document>();

            var x = Data.GlobalVariables.docs.obj.documents
                .Where(x => (x.year == numericUpDown_search_anno.Value || checkBox_search_year.Checked == false ))
                .Where(x => (( x.category == this.categoryIdSelected) && x.category != null))
                .Select(x => x.id)
                .Where(x => x != null)
                .ToList();

            if (x.Count == 0)
                return 1;

            var max = x.Max();
            if (max == null) 
                return 1;

            return max.Value + 1;
        }

        private void LoadCategories()
        {
            Data.GlobalVariables.categories ??= new Rif<Model.Cat.Categories>();
            Data.GlobalVariables.categories.obj ??= new Model.Cat.Categories();
            Data.GlobalVariables.categories.obj.categories ??= new List<Model.Cat.Category>();
 

            if (Data.GlobalVariables.categories.obj.categories.Count == 0)
            {
                Model.Cat.Category category = new()
                {
                    creationDate = DateTime.Now,
                    Id = 0, 
                    Name = "Generale", 
                    Description = "Categoria generale di default"
                };
                Data.GlobalVariables.categories.obj.categories.Add(category);
            }

            Refresh_categories();

            Utils.Files.SaveFile(Data.GlobalVariables.categories, Data.Constants.PathCategories);

            this.listBox_cat.SelectedIndexChanged += new EventHandler(Changed_category);

            
        }

        private void Changed_category(object? sender, EventArgs e)
        {
            var cat = (Model.Cat.Category)this.listBox_cat.Items[this.listBox_cat.SelectedIndex];
            if (cat.Id != null)
            {
                this.categoryIdSelected = cat.Id.Value;
                Refresh_docs();
            }
        }

        private void Refresh_categories()
        {
            Data.GlobalVariables.categories ??= new Rif<Model.Cat.Categories>();
            Data.GlobalVariables.categories.obj ??= new Model.Cat.Categories();
            Data.GlobalVariables.categories.obj.categories ??= new List<Model.Cat.Category>();

            listBox_cat.Items.Clear();  

            foreach (var cat in Data.GlobalVariables.categories.obj.categories)
            {
                if (cat != null)
                {
                    var catString = cat.ToString();
                    if (!string.IsNullOrEmpty(catString))
                        listBox_cat.Items.Add(cat);
                }
            }

            Model.Cat.Category cat_null = new() { Id= null, Name = "[all]", Description = "", creationDate = null };
            listBox_cat.Items.Insert(0, cat_null);
        }

        private static void LoadFiles()
        {  
            Utils.Files.LoadFile<Docs>(Data.GlobalVariables.docs, Data.Constants.PathDocs);
            Utils.Files.LoadFile<Model.Cat.Categories>(Data.GlobalVariables.categories, Data.Constants.PathCategories);
        }

        private void TextBox_search_TextChanged(object sender, EventArgs e)
        {
            Refresh_docs();
        }


        private void Refresh_docs()
        {
            string text = textBox_search.Text.ToLower();

            toRefreshDocs = false;
            dataTable.Rows.Clear();
            toRefreshDocs = true;

            if (Data.GlobalVariables.docs != null)
                if (Data.GlobalVariables.docs.obj != null)
                    if (Data.GlobalVariables.docs.obj.documents != null)
                        for (int i = 0; i < Data.GlobalVariables.docs.obj.documents.Count; i++)
                        {
                            Document? row = Data.GlobalVariables.docs.obj.documents[i];
                            if (row.id != null)
                            {
                                var contained = row.fileName?.ToLower().Contains(text);
                                if (string.IsNullOrEmpty(text) || (contained != null && contained.Value))
                                {
                                    if (row.category == categoryIdSelected)
                                    {
                                        if (this.checkBox_search_year.Checked == false || this.numericUpDown_search_anno.Value == row.year || row.year == null)
                                        {
                                            DataRow row2 = dataTable.NewRow();
                                            foreach (var docHead in Document.headList)
                                            {
                                                row2[docHead.GetName()] = docHead.GetValue(row);
                                            }


                                            dataTable.Rows.Add(row2);
                                        }
                                    }
                                }
                            }
                        }

            dataGridView_doc.DataSource = dataTable;
        }

        private void NumericUpDown_search_anno_ValueChanged(object sender, EventArgs e)
        {
            Refresh_docs();
        }

        private void CheckBox_search_year_CheckedChanged(object sender, EventArgs e)
        {
            Refresh_docs();
        }

        private void Button_doc_ricarica_Click(object sender, EventArgs e)
        {
            Refresh_docs();
        }
    }
}