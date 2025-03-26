using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Microsoft.SemanticKernel;

namespace News
{
    public class SaveToDiskPlugin
    {
        [KernelFunction("save_to_file")]
        [Description("Save data to a file with a given file name.")]
        public async Task SaveToFile(Kernel kernel, string filename, string data)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, $"{filename}.txt");
            await File.WriteAllTextAsync(filePath, data);
        }
    }
}
