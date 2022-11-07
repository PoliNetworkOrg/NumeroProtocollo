using System.Data;
using NumerazioneProtocollo.Data;
using NumerazioneProtocollo.Model;
using NumerazioneProtocollo.Model.Cat;
using NumerazioneProtocollo.Model.Docs;
using NumerazioneProtocollo.Model.VarNames;
using NumerazioneProtocollo.Utils;

namespace NumerazioneProtocollo;

public partial class MainForm : Form
{
    private readonly DataTable dataTable = new();
    private int categoryIdSelected;

    public MainForm()
    {
        InitializeComponent();
    }


    private void Form1_Load(object sender, EventArgs e)
    {
        numericUpDown_search_anno.Value = DateTime.Now.Year;

        GlobalVariables.docs ??= new Rif<Docs>();
        GlobalVariables.categories ??= new Rif<Categories>();
        GlobalVariables.paths ??= new Rif<Model.Path.SettingsVar>();

        LoadFiles();


        GlobalVariables.docs.Obj ??= new Docs();
        GlobalVariables.categories.Obj ??= new Categories();

        LoadCategories();
        LoadDocuments();
    }


    private void LoadDocuments()
    {
        foreach (var docHead in HeadList.HeadListVar) dataTable.Columns.Add(docHead.GetName());

        Refresh_docs();

        //dataGridView_doc.RowsAdded += new DataGridViewRowsAddedEventHandler(DocsModified);
        dataGridView_doc.CellEndEdit += CellEditEnded;
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
        GlobalVariables.docs ??= new Rif<Docs>();
        GlobalVariables.docs.Obj ??= new Docs();
        GlobalVariables.docs.Obj.documents ??= new List<Document>();


        var doc = GetDoc(rowIndex);


        doc.creationDate ??= DateTime.Now;
        doc.year ??= (int)numericUpDown_search_anno.Value;
        doc.category ??= GetCategorySelected() ?? 0;
        doc.id ??= GetNewId();

        GlobalVariables.docs.Obj.HandleEdit(doc);

        Files.SaveFile(GlobalVariables.docs, Data.Constants.GetPathDocuments());
    }

    private Document GetDoc(int rowIndex)
    {
        var rowAdded = dataGridView_doc.Rows[rowIndex];

        var doc = Document.Get(rowAdded, dataGridView_doc);
        return doc;
    }

    private int GetNewId()
    {
        GlobalVariables.docs ??= new Rif<Docs>();
        GlobalVariables.docs.Obj ??= new Docs();
        GlobalVariables.docs.Obj.documents ??= new List<Document>();

        var x = GlobalVariables.docs.Obj.documents
            .Where(x => x.year == numericUpDown_search_anno.Value || false)
            .Where(x => x.category == categoryIdSelected && x.category != null)
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
        GlobalVariables.categories ??= new Rif<Categories>();
        GlobalVariables.categories.Obj ??= new Categories();
        GlobalVariables.categories.Obj.categories ??= new List<Category>();


        if (GlobalVariables.categories.Obj.categories.Count == 0)
        {
            Category category = new()
            {
                creationDate = DateTime.Now,
                Id = 0,
                Name = "Generale",
                Description = "Categoria generale di default"
            };
            GlobalVariables.categories.Obj.categories.Add(category);
        }

        Refresh_categories();

        Files.SaveFile(GlobalVariables.categories, Data.Constants.GetPathCategories());

        listBox_cat.SelectedIndexChanged += Changed_category;
    }

    private void Changed_category(object? sender, EventArgs e)
    {
        if (listBox_cat.SelectedIndex < 0 || listBox_cat.SelectedIndex >= listBox_cat.Items.Count)
        {
            categoryIdSelected = 0;
            Refresh_docs();
            return;
        }

        var cat = (Category)listBox_cat.Items[listBox_cat.SelectedIndex];
        if (cat.Id == null) return;
        categoryIdSelected = cat.Id.Value;
        Refresh_docs();
    }

    private void Refresh_categories()
    {
        GlobalVariables.categories ??= new Rif<Categories>();
        GlobalVariables.categories.Obj ??= new Categories();
        GlobalVariables.categories.Obj.categories ??= new List<Category>();

        listBox_cat.Items.Clear();

        foreach (var cat in GlobalVariables.categories.Obj.categories)
        {
            if (cat == null) continue;
            var catString = cat.ToString();
            if (!string.IsNullOrEmpty(catString))
                listBox_cat.Items.Add(cat);
        }

        Category cat_null = new() { Id = null, Name = "[all]", Description = "", creationDate = null };
        listBox_cat.Items.Insert(0, cat_null);
    }

    private static void LoadFiles()
    {
        Files.LoadFile(GlobalVariables.paths, Constants.PathOfSettings);
        Files.LoadFile(GlobalVariables.docs, Data.Constants.GetPathDocuments());
        Files.LoadFile(GlobalVariables.categories, Data.Constants.GetPathCategories());
       
    }

    private void TextBox_search_TextChanged(object sender, EventArgs e)
    {
        Refresh_docs();
    }


    private void Refresh_docs()
    {
        var text = textBox_search.Text.ToLower();


        dataTable.Rows.Clear();


        if (GlobalVariables.docs is { Obj.documents: { } })
            for (var i = 0; i < GlobalVariables.docs.Obj.documents.Count; i++)
            {
                var row = GlobalVariables.docs.Obj.documents[i];

                if (row.id == null) 
                    continue;

                var contained = row.fileName?.ToLower().Contains(text);
                if (!string.IsNullOrEmpty(text) && (contained == null || !contained.Value))
                    continue;

                if (row.category != categoryIdSelected)
                    continue;

                if (numericUpDown_search_anno.Value != row.year && row.year != null)
                    continue;

                var idString = row.id.ToString();
                if (!string.IsNullOrEmpty(textBox_search_id_doc.Text) && (idString == null || !idString.Contains(textBox_search_id_doc.Text)))
                    continue;

                //this row is valid, let's add it
                AddRow(row);
            }

        dataGridView_doc.DataSource = dataTable;
    }

    private void AddRow(Document row)
    {
        var row2 = dataTable.NewRow();

        foreach (var docHead in HeadList.HeadListVar) 
            row2[docHead.GetName()] = docHead.GetValue(row);


        dataTable.Rows.Add(row2);
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
        var dialogResult = MessageBox.Show(
            "Vuoi veramente cancellare il documento? E' un'operazione che non si dovrebbe fare con leggerezza.",
            "Sei sicuro?",
            MessageBoxButtons.YesNo
        );

        if (dialogResult == DialogResult.Yes) Delete_selected_doc();
    }

    private void Delete_selected_doc()
    {
        var rowId = GetSelectedRow(dataGridView_doc);
        if (rowId == null)
            return;


        var doc = GetDoc(rowId.Value);
        if (doc == null)
            return;

        if (doc.id == null)
            return;

        GlobalVariables.docs ??= new Rif<Docs>();
        GlobalVariables.docs.Obj ??= new Docs();
        GlobalVariables.docs.Obj.Delete(doc.id.Value);
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
        if (string.IsNullOrEmpty(textBox_cat.Text)) return;
        var name = textBox_cat.Text;

        GlobalVariables.categories ??= new Rif<Categories>();
        GlobalVariables.categories.Obj ??= new Categories();
        GlobalVariables.categories.Obj.Add(name);

        Refresh_categories();
        Files.SaveFile(GlobalVariables.categories, Data.Constants.GetPathCategories());
    }

    private void Button_cat_modifica_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(textBox_cat.Text)) return;
        var dialogResult = MessageBox.Show(
            "Vuoi veramente modificare il nome della categoria? E' un'operazione che non si dovrebbe fare con leggerezza.",
            "Sei sicuro?",
            MessageBoxButtons.YesNo
        );

        if (dialogResult == DialogResult.Yes) Edit_cat_selected(textBox_cat.Text);
    }


    private void Button_cat_elimina_Click(object sender, EventArgs e)
    {
        var dialogResult = MessageBox.Show(
            "Vuoi veramente cancellare la categoria? E' un'operazione che non si dovrebbe fare con leggerezza.",
            "Sei sicuro?",
            MessageBoxButtons.YesNo
        );

        if (dialogResult != DialogResult.Yes) return;
        Delete_cat_selected();
        Changed_category(null, EventArgs.Empty);
    }

    private int? GetCategorySelected()
    {
        if (listBox_cat.SelectedIndex < 0 || listBox_cat.SelectedIndex >= listBox_cat.Items.Count) return null;
        var cat = (Category)listBox_cat.Items[listBox_cat.SelectedIndex];
        if (cat != null) return cat.Id;

        return null;
    }

    private void Edit_cat_selected(string text)
    {
        var idCategorySelected = GetCategorySelected();
        if (idCategorySelected == null) return;
        GlobalVariables.categories ??= new Rif<Categories>();
        GlobalVariables.categories.Obj ??= new Categories();
        GlobalVariables.categories.Obj.EditName(idCategorySelected.Value, text);
        Files.SaveFile(GlobalVariables.categories, Data.Constants.GetPathCategories());
        Refresh_categories();
    }


    private void Delete_cat_selected()
    {
        var idCategorySelected = GetCategorySelected();
        if (idCategorySelected == null) return;
        GlobalVariables.categories ??= new Rif<Categories>();
        GlobalVariables.categories.Obj ??= new Categories();
        GlobalVariables.categories.Obj.DeleteFromId(idCategorySelected.Value);
        Files.SaveFile(GlobalVariables.categories, Data.Constants.GetPathCategories());
        Refresh_categories();
    }

    private void TextBox_search_id_doc_TextChanged(object sender, EventArgs e)
    {
        Refresh_docs();
    }

    private void Button_settings_Click(object sender, EventArgs e)
    {
        Forms.SettingsForm settingsForm = new Forms.SettingsForm();
        settingsForm.ShowDialog();
    }
}