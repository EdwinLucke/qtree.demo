using System.Net;
using System.Reflection;

namespace qtree.dotnet6.webapi.dockerized.shared
{
    /// <summary>
    /// https://movares-ta.visualstudio.com/LTD/_wiki/wikis/LTD.wiki/1567/HTTP-response-codes-guidelines
    /// Thgese are the status codes that are able to convey a message tpo the frontend, alllowing for application specific reuse of standardized statuscodes
    /// </summary>    
    public class AvailableStatusCodes : Enumeration
    {
        public static readonly AvailableStatusCodes Ok = new((int)HttpStatusCode.OK, "OK");
        public static readonly AvailableStatusCodes Created = new((int)HttpStatusCode.Created, "The resource has been created");
        public static readonly AvailableStatusCodes NotModified = new((int)HttpStatusCode.NotModified, "The resourtce is not modified");
        public static readonly AvailableStatusCodes BadRequest = new((int)HttpStatusCode.BadRequest, "This request could not be handled, see errors for more information");
        public static readonly AvailableStatusCodes ForBidden = new((int)HttpStatusCode.Forbidden, "You are not allowed to perform the action");
        public static readonly AvailableStatusCodes Conflict = new((int)HttpStatusCode.Conflict, "There is a vconflict when performing this action, see errors for more information");
        public static readonly AvailableStatusCodes UnsupportedMediaType = new((int)HttpStatusCode.UnsupportedMediaType, "The mediatype you have provided is not supported");
        public static readonly AvailableStatusCodes InternalServerError = new((int)HttpStatusCode.InternalServerError, "Internal server error when processing request");

        public AvailableStatusCodes(int id, string name) : base(id, name)
        {
        }
    }

    public abstract class Enumeration : IComparable
    {
        public string Name { get; private set; }

        public int Id { get; private set; }

        protected Enumeration(int id, string name) => (Id, Name) = (id, name);

        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                        .Select(f => f.GetValue(null))
                        .Cast<T>();

        public override bool Equals(object? obj)
        {
            if (obj is not Enumeration otherValue)
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => Id.GetHashCode();

        public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
        {
            var absoluteDifference = Math.Abs(firstValue.Id - secondValue.Id);
            return absoluteDifference;
        }

        public static T FromValue<T>(int value) where T : Enumeration
        {
            var matchingItem = Parse<T, int>(value, "value", item => item.Id == value);
            return matchingItem;
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumeration
        {
            var matchingItem = Parse<T, string>(displayName, "display name", item => item.Name == displayName);
            return matchingItem;
        }

        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
                throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

            return matchingItem;
        }

        public int CompareTo(object? obj) => Id.CompareTo(((Enumeration)obj).Id);
    }
}
