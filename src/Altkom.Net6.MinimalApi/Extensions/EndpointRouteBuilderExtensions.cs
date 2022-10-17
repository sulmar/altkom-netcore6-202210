namespace Altkom.Net6.MinimalApi.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        private static readonly string[] HeadVerbs = new[] { "HEAD" };
        private static readonly string[] PatchVerbs = new[] { "PATCH" };

        public static IEndpointConventionBuilder MapHead(this IEndpointRouteBuilder endpoints,
            string pattern,
            Delegate requestDelegate)
        {
            return endpoints.MapMethods(pattern, HeadVerbs, requestDelegate);
        }


        public static IEndpointConventionBuilder MapPatch(this IEndpointRouteBuilder endpoints,
            string pattern,
            Delegate requestDelegate)
        {
            return endpoints.MapMethods(pattern, PatchVerbs, requestDelegate);
        }
    }
}
