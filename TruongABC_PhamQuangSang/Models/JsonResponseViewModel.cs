namespace TruongABC_PhamQuangSang.Models;

[Serializable]
public class JsonResponseViewModel
{
    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;

    public string Data { get; set; } = string.Empty;
}
