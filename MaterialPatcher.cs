using System.Configuration;
using System.Diagnostics;

namespace VisualPinball.MaterialPatcher.GUI
{
    public interface IMaterialPatcher
    {
        bool PatchMaterials(string materialFile, string inputPath, string outputPath);
    }

    public class MaterialPatcherWrapper : IMaterialPatcher
    {
        private string patcherExecutable;

        public MaterialPatcherWrapper()
        {
            patcherExecutable = ConfigurationManager.AppSettings["PatcherExecutable"];
        }

        public bool PatchMaterials(string materialFile, string inputPath, string outputPath)
        {
            using (var process = new Process())
            {
                process.StartInfo.FileName = patcherExecutable;
                process.StartInfo.Arguments = $"{materialFile} {inputPath} {outputPath}";
                process.StartInfo.RedirectStandardOutput = false;
                process.StartInfo.RedirectStandardError = false;

                process.Start();
                process.WaitForExit();

                if (process.ExitCode == 0)
                    return true;
                else
                    return false;
            }
        }
    }
}
