namespace LoLApp.Extensions;

public static class Base64
{
    public static async Task<string> ToBase64(this FileBase file)
    {
        using var memoryStream = new MemoryStream();
        await (await file.OpenReadAsync()).CopyToAsync(memoryStream);
        
        return Convert.ToBase64String(memoryStream.ToArray());
    }
}
