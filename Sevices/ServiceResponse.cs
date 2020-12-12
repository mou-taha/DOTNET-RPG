namespace DOTNET_RPG.Sevices
{
    public class ServiceResponse<T>
    {
        public T DATA { get; set; }
        public bool success { get; set; }=true;
        public string message { get; set; }=null;
    }
}