#nullable enable
namespace LoLApp.Utils;

public static class ImageFilePicker
{
    public static async Task PickImage(Action<FileResult?> callback)
    {
        try
        {
            var options = new PickOptions
            {
                PickerTitle = "Please select a file",
                FileTypes = FilePickerFileType.Images,
            };

            var result = await FilePicker.Default.PickAsync(options);
            if (result is null) return;

            callback(result);
        }
        catch (TaskCanceledException) {  /* ignored */ }
    }
}
