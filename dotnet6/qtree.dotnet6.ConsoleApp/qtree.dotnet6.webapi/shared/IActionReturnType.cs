namespace qtree.dotnet6.webapi.dockerized.shared.interfaces
{
    public interface IActionReturnType<T>
    {
        AvailableStatusCodes Code { get; set; }
        T Object { get; set; }
        List<string> Errors { get; set; }
        string Message { get; set; }

        void Add(string error);
        void AddRange(IEnumerable<string> error);
    }
}