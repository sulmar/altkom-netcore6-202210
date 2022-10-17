namespace Altkom.Net6.MinimalApi
{
    public class HelloHandlers
    {
        public static string Hello() => "Hello from static function";
        public static string Hello(string name) => $"Hello {name}!";
    }
}
