using qtree.dotnet6.webapi.dockerized.shared.interfaces;

namespace qtree.dotnet6.webapi.dockerized.shared
{

    /// <summary>
    /// ActionReturnType - op een generieke manier resultaten kunnen teruggegeven naar de frontend.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ActionReturnType<T> : IActionReturnType<T>
    {
        public AvailableStatusCodes Code { get; set; }

        public T Object { get; set; }
        public List<string> Errors { get; set; }
        public string Message { get; set; } = string.Empty;

        public ActionReturnType(T returnObject, AvailableStatusCodes code = null, IEnumerable<string> errors = null)
        {
            Object = returnObject;
            Code = code == null ? AvailableStatusCodes.Ok : code;
            Errors = errors == null ? new List<string>() : errors.ToList();
        }


        /// <summary>
        /// Add an error to the error list
        /// </summary>
        /// <param name="error"></param>
        public void Add(string error)
        {
            if (Errors == null) Errors = new List<string>();
            Errors.Add(error);
        }

        public void AddRange(IEnumerable<string> error)
        {
            if (Errors == null) Errors = new List<string>();

            if (error != null && error.Any()) Errors.AddRange(error);
        }

        public static ActionReturnType<T> GetReturnType(T returnObject)
        {
            return new ActionReturnType<T>(returnObject, AvailableStatusCodes.Ok);
        }

        public static ActionReturnType<T> GetBadRequestReturnType(T returnObject, List<string> errors = null)
        {
            return new ActionReturnType<T>(returnObject, AvailableStatusCodes.BadRequest, errors);
        }
    }
}
