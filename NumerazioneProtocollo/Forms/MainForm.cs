using Newtonsoft.Json.Linq;
using NumerazioneProtocollo.Model;
using NumerazioneProtocollo.Model.Docs;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

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
            Data.GlobalVariables.docs ??= new Rif<Docs>();
            Data.GlobalVariables.docs.obj ??= new Docs();
            Data.GlobalVariables.docs.obj.documents ??= new List<Document>();


            Document doc = GetDoc(rowIndex);
           

            doc.creationDate ??= DateTime.Now;
            doc.year ??= (int)numericUpDown_search_anno.Value;
            doc.category ??= 0;
            doc.id ??= GetNewId();

            Data.GlobalVariables.docs.obj.HandleEdit(doc);

            Utils.Files.SaveFile(Data.GlobalVariables.docs, Data.Constants.PathDocs);
                    
        }

        private Document GetDoc(int rowIndex)
        {
            DataGridViewRow rowAdded = dataGridView_doc.Rows[rowIndex];

            Document doc = Model.Docs.Document.Get(rowAdded, dataGridView_doc);
            return doc;
        }

        private int GetNewId()
        {

            Data.GlobalVariables.docs ??= new Rif<Docs>();
            Data.GlobalVariables.docs.obj ??= new Docs();
            Data.GlobalVariables.docs.obj.documents ??= new List<Document>();

            var x = Data.GlobalVariables.docs.obj.documents
                .Where(x => (x.year == numericUpDown_search_anno.Value || true == false ))
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
            if (this.listBox_cat.SelectedIndex < 0 || this.listBox_cat.SelectedIndex >= this.listBox_cat.Items.Count)
            {
                this.categoryIdSelected = 0;
                Refresh_docs();
                return;
            }

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

           
            dataTable.Rows.Clear();
      

            if (Data.GlobalVariables.docs is { obj.documents: { } })
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
                                if (true == false || this.numericUpDown_search_anno.Value == row.year || row.year == null)
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

        private void Button_doc_elimina_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
                "Vuoi veramente cancellare il documento? E' un'operazione che non si dovrebbe fare con leggerezza.",
                "Sei sicuro?", 
                MessageBoxButtons.YesNo
            );

            if (dialogResult == DialogResult.Yes)
            {
                Delete_selected_doc();
            }

        }

        private void Delete_selected_doc()
        {
            int? rowId = GetSelectedRow(dataGridView_doc);
            if (rowId == null)
                return;


            var doc = GetDoc(rowId.Value);
            if (doc == null)
                return;

            if (doc.id == null)
                return;

            Data.GlobalVariables.docs ??= new Rif<Docs>();
            Data.GlobalVariables.docs.obj ??= new Docs();
            Data.GlobalVariables.docs.obj.Delete(doc.id.Value);
            Refresh_docs();
        }

        private static int? GetSelectedRow(DataGridView dataGridView_doc)
        {
            return dataGridView_doc.SelectedCells.Count > 0
                ? dataGridView_doc.SelectedCells[0].RowIndex
                : dataGridView_doc.SelectedRows.Count > 0 
                    ? dataGridView_doc.SelectedRows[0].Index 
                    : null;
        }

        private void Button_cat_crea_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox_cat.Text))
            {
                var name = textBox_cat.Text;

                Data.GlobalVariables.categories ??= new Rif<Model.Cat.Categories>();
                Data.GlobalVariables.categories.obj ??= new Model.Cat.Categories();
                Data.GlobalVariables.categories.obj.Add(name);

                Refresh_categories();
                Utils.Files.SaveFile(Data.GlobalVariables.categories, Data.Constants.PathCategories);
            }
        }

        private void Button_cat_modifica_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox_cat.Text))
            {
                DialogResult dialogResult = MessageBox.Show(
                    "Vuoi veramente modificare il nome della categoria? E' un'operazione che non si dovrebbe fare con leggerezza.",
                    "Sei sicuro?",
                    MessageBoxButtons.YesNo
                );

                if (dialogResult == DialogResult.Yes)
                {
                    Edit_cat_selected(textBox_cat.Text);
                }
            }
        }


        private void Button_cat_elimina_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
                "Vuoi veramente cancellare la categoria? E' un'operazione che non si dovrebbe fare con leggerezza.",
                "Sei sicuro?",
                MessageBoxButtons.YesNo
            );

            if (dialogResult == DialogResult.Yes)
            {
                Delete_cat_selected();
                Changed_category(null, EventArgs.Empty);


            }
         
        }

        private int? GetCategorySelected()
        {
            if (listBox_cat.SelectedIndex >= 0 && listBox_cat.SelectedIndex < listBox_cat.Items.Count)
            {
                var cat =(Model.Cat.Category) listBox_cat.Items[listBox_cat.SelectedIndex];
                if (cat != null)
                {
                    return cat.Id;
                }
            }

            return null;
        }

        private void Edit_cat_selected(string text)
        {
            int? idCategorySelected = GetCategorySelected();
            if (idCategorySelected != null)
            {
                Data.GlobalVariables.categories ??= new Rif<Model.Cat.Categories>();
                Data.GlobalVariables.categories.obj ??= new Model.Cat.Categories();
                Data.GlobalVariables.categories.obj.EditName(idCategorySelected.Value, text);
                Utils.Files.SaveFile(Data.GlobalVariables.categories, Data.Constants.PathCategories);
                Refresh_categories();
            }
        }

     

        private void Delete_cat_selected()
        {
            int? idCategorySelected = GetCategorySelected();
            if (idCategorySelected != null)
            {
                Data.GlobalVariables.categories ??= new Rif<Model.Cat.Categories>();
                Data.GlobalVariables.categories.obj ??= new Model.Cat.Categories();
                Data.GlobalVariables.categories.obj.DeleteFromId(idCategorySelected.Value);
                Utils.Files.SaveFile(Data.GlobalVariables.categories, Data.Constants.PathCategories);
                Refresh_categories();
            }
        }
    }
}