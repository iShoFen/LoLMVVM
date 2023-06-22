#nullable enable
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace LoLApp.Utils;

public static class ImageFilePicker
{
    public static async Task<FileResult?> PickImage()
    {
        try
        {
            var options = new PickOptions
            {
                PickerTitle = "Please select a file",
                FileTypes = FilePickerFileType.Images,
            };

            var result = await FilePicker.Default.PickAsync(options);

            return result;
        }
        catch (TaskCanceledException) {  /* ignored */ }
        
        return null;
    }
}
