using System.Windows.Forms;

namespace FileBrowser;

public partial class FileBrowserForm : Form
{
    private FilePreviewer _filePreviewer;

    public FileBrowserForm()
    {
        InitializeComponent();
        nextButton.Enabled = false;
        closeButton.Enabled = false;
    }

    private void openButton_Click(object sender, EventArgs e)
    {
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            _filePreviewer = new FilePreviewer(openFileDialog.FileName);
            resultTextBox.Text = _filePreviewer.GetNextLine();
            openButton.Enabled = false;
            closeButton.Enabled = true;
            nextButton.Enabled = true;
        }
    }

    private void nextButton_Click(object sender, EventArgs e)
    {
        resultTextBox.Text = _filePreviewer.GetNextLine();
    }

    private void closeButton_Click(object sender, EventArgs e)
    {
        _filePreviewer.Dispose();
        openButton.Enabled = true;
        nextButton.Enabled = false;
        resultTextBox.Clear();
    }

    private void PreviouseButton_Click(object sender, EventArgs e)
    {
        resultTextBox.Text = _filePreviewer.GetPreviousLine();
    }
}

public class FilePreviewer : IDisposable
{
    //private readonly StreamReader _reader;

    private string[] _allLines;

    private int _index = -1;

    public FilePreviewer(string path)
    {
        //_reader = new StreamReader(path);
        _allLines = File.ReadAllLines(path);
    }

    public string GetNextLine()
    {
        if (_index >= _allLines.Length - 1)
        {
            return "-- END OF FILE --";
        }
        _index++;
        return _allLines[_index];
    }
    public string GetPreviousLine()
    {
        if (_index <= 0)
        {
            return "-- START OF FILE --";
        }
        _index--;
        return _allLines[_index];
    }

    public void Dispose()
    {
        //_reader.Dispose();
        //_allLines.Dispose(); not working
    }
}
