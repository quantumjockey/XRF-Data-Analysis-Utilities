///////////////////////////////////////
#region Namespace Directives

using CompUhaul.Dialogs;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using WpfHelper.ViewModel.Controls;
using WpfHelper.ViewModel.Windows;
using WpfHelper.ViewModel.Workspaces;
using XRF_Data_Analysis_Utilities.Model;
using XRF_Data_Analysis_Utilities.Files;
using XRF_Data_Analysis_Utilities.ViewModel.Workspaces;
using XRF_Data_Analysis_Utilities.ViewModel.Workspaces.Interfaces;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities.ViewModel.Windows
{
    class MainWindowViewModel : WindowViewModel
    {
        ////////////////////////////////////////
        #region Generic Fields

        // Window-Specific
        private bool _canExport;
        private string _title;

        // Window Dialogs
        private SaveFileDialog ExportImageToFile;
        private OpenFileDialog OpenDumpFile;

        // Workspace-specific
        private SampleWorkspaceViewModel _selectedSample;

        #endregion

        ////////////////////////////////////////
        #region Properties and Indexers

        public bool CanExport
        {
            get
            {
                return _canExport;
            }
            set
            {
                _canExport = value;
                OnPropertyChanged("CanExport");
            }
        }

        public WorkspaceViewModelCollection Samples
        {
            get;
            set;
        }

        public SampleWorkspaceViewModel SelectedSample
        {
            get
            {
                return _selectedSample;
            }
            set
            {
                _selectedSample = value;
                if (_selectedSample != null && _selectedSample.SelectedImageAnalysisWorkspace != null && _selectedSample.SelectedImageAnalysisWorkspace.GetSelectedImage() != null)
                    CanExport = true;
                else
                    CanExport = false;
                OnPropertyChanged("SelectedSample");
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        #endregion

        ////////////////////////////////////////
        #region Actions

        public CommandDrivenControlViewModel OpenFile { get; set; }

        public CommandDrivenControlViewModel ExitProgram { get; set; }

        public CommandDrivenControlViewModel ExportImage { get; set; }

        #endregion

        ////////////////////////////////////////
        #region Constructor


        public MainWindowViewModel()
        {
            Title = Assembly.GetExecutingAssembly().GetName().Name.ToString() + " - version " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            InitializeDialogues();
            InitializeInteractiveControls();
            Samples = new WorkspaceViewModelCollection();
        }

        #endregion

        ////////////////////////////////////////
        #region Action Methods


        private void ExportCanvasToImage()
        {
            ExportImageToFile.FileName = GenerateImageDestinationFilename(_selectedSample.SelectedImageAnalysisWorkspace.GetSelectedData());
            ExportImageToFile.ShowDialog();
        }


        private void ExitApplication()
        {
            App.Current.Shutdown();
        }


        private void OpenDataFile()
        {
            OpenDumpFile.ShowDialog();
        }

        #endregion

        ////////////////////////////////////////
        #region Events


        void ExportImageToFile_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MemoryStream fileDataStream = GenerateTiffFromCanvas(_selectedSample.SelectedImageAnalysisWorkspace.GetSelectedImage().RenderedImage, 96, 1.0);
            WriteStreamToFile(fileDataStream, ExportImageToFile.FileName);
            OpenFileInExplorer(ExportImageToFile.FileName);
        }


        void OpenDumpFile_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            xrfSample sample = DumpFileHandler.GetSampleData(OpenDumpFile.FileName);
            Samples.Add(new SampleWorkspaceViewModel(OpenDumpFile.SafeFileName, ref sample));
            SelectedSample = Samples[Samples.Count - 1] as SampleWorkspaceViewModel;
        }

        #endregion

        ////////////////////////////////////////
        #region Private Methods

        private string GenerateImageDestinationFilename(IImageDataWorkspace _selected)
        {
            return "XRF_Image_" + _selected.Header + "_" + _selected.MinValue + "-" + _selected.MaxValue + "cnts"
                + "_rendered_at_" + _selected.Zoom + "x_zoom"
                + "_UTC" + DateTime.UtcNow.ToString("HH:mm:ss").Replace(' ', '_').Replace('/', '-').Replace(':', '.');
        }

        private MemoryStream GenerateTiffFromCanvas(Canvas imageData, int dpi, double scale)
        {
            int horSize = (int)(imageData.ActualWidth * scale);
            int vertSize = (int)(imageData.ActualHeight * scale);

            System.Windows.Media.PixelFormat format = System.Windows.Media.PixelFormats.Default;

            RenderTargetBitmap rasterizer = new RenderTargetBitmap(horSize, vertSize, dpi, dpi, format);
            rasterizer.Render(imageData);

            BitmapEncoder encoder = new TiffBitmapEncoder();
            MemoryStream tiffData = new MemoryStream();

            encoder.Frames.Add(BitmapFrame.Create(rasterizer));
            encoder.Save(tiffData);
            
            return tiffData;
        }

        private void InitializeInteractiveControls()
        {
            OpenFile = new CommandDrivenControlViewModel((x) => OpenDataFile(), "Open File...", "Displays a dialog through which you can select and open a data file for analysis.");
            ExitProgram = new CommandDrivenControlViewModel((x) => ExitApplication(), "Exit", "Exit the application.");
            ExportImage = new CommandDrivenControlViewModel((x) => ExportCanvasToImage(), "Export Selected Image...", "Exports the selected XRF image frame.");
        }

        private void InitializeDialogues()
        {
            OpenDumpFile = DialogInitializer.InitializeOpenFromFileDialog(".txt", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Open XRF Dump File");
            OpenDumpFile.FileOk += OpenDumpFile_FileOk;

            ExportImageToFile = DialogInitializer.InitializeSaveToFileDialog(".tiff", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Export to TIFF");
            ExportImageToFile.FileOk += ExportImageToFile_FileOk;
        }

        private void OpenFileInExplorer(string filename)
        {
            Process.Start(filename);
        }

        private void WriteStreamToFile(MemoryStream data, string destinationPath)
        {
            using (FileStream FileStream = File.OpenWrite(destinationPath))
            {
                data.WriteTo(FileStream);
                FileStream.Flush();
                FileStream.Close();
            }
        }

        #endregion
    }
}
