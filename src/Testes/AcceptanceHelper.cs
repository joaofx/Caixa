namespace IntegrationTests
{
    using Felice.Core;

    public static class AcceptanceHelper
    {
        public static T Resolve<T>(this object @object)
        {
            return Dependency.Resolve<T>();
        }
    }
}