namespace Models.ViewModels
{
    public class ResponseVM
    {
        public object? Data { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "";
    }
}
