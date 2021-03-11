using System.Windows;

namespace VisualPinball.MaterialPatcher.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string materialFilePath;
        private string inputPath;
        private string outputPath;
        private IMaterialPatcher _materialPatcher;

        public MainWindow()
        {
            InitializeComponent();
            _materialPatcher = new MaterialPatcherWrapper();
        }

        private void BrowseMaterialFileButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.OpenFileDialog())
            {
                dialog.DefaultExt = ".mat";
                dialog.Filter = "MAT Files (*.mat)|*.mat";

                System.Windows.Forms.DialogResult result = dialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    materialFilePath = dialog.FileName;
                    MaterialFileTextBox.Text = dialog.FileName;
                }
            }
        }

        private void BrowseInputPathButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    inputPath = dialog.SelectedPath;
                    InputTextBox.Text = dialog.SelectedPath;
                }
            }
        }

        private void BrowseOutputPathButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    outputPath = dialog.SelectedPath;
                    OutputTextBox.Text = dialog.SelectedPath;
                }
            }
        }

        private void ProcessButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                if (_materialPatcher.PatchMaterials(materialFilePath, inputPath, outputPath))
                    MessageBox.Show("Processing done!", "Done", MessageBoxButton.OK);
                else
                    MessageBox.Show("Error while processing!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(materialFilePath))
            {
                MessageBox.Show("Cannot process without a material file", "No Material File", MessageBoxButton.OK);
                return false;
            }
            else if (string.IsNullOrEmpty(inputPath))
            {
                MessageBox.Show("Cannot process without an input path", "No Input Path", MessageBoxButton.OK);
                return false;
            }
            else if (string.IsNullOrEmpty(outputPath))
            {
                MessageBox.Show("Cannot process without an output path", "No Output Path", MessageBoxButton.OK);
                return false;
            }

            return true;
        }

        private void MaterialFileTextBox_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
                if (files != null && files.Length > 0)
                {
                    materialFilePath = files[0];
                    MaterialFileTextBox.Text = files[0];
                }
            }
        }

        private void InputTextBox_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
                if (files != null && files.Length > 0)
                {
                    inputPath = files[0];
                    InputTextBox.Text = files[0];
                }
            }
        }

        private void OutputTextBox_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
                if (files != null && files.Length > 0)
                {
                    outputPath = files[0];
                    OutputTextBox.Text = files[0];
                }
            }
        }

        private void MaterialFileTextBox_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }

        private void InputTextBox_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }

        private void OutputTextBox_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }
    }
}
