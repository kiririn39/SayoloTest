namespace Project.Source.Networking
{
    public class HttpContentType
    {
        private readonly string _type;


        private HttpContentType(string type) => _type = type;

        public static HttpContentType Json => new HttpContentType("text/json");
        public static HttpContentType Xml => new HttpContentType("application/xml");
        public static HttpContentType Text => new HttpContentType("text/html");

        public override bool Equals(object obj)
        {
            if (obj is HttpContentType)
            {
                return (obj as HttpContentType)._type == this._type;
            }
            
            return base.Equals(obj);
        }

        public bool Equals(HttpContentType other) => _type == other._type;
        public bool Equals(string other) => _type == other;

        public override int GetHashCode() => (_type != null ? _type.GetHashCode() : 0);

        public static implicit operator string(HttpContentType type) => type._type;
    }
}