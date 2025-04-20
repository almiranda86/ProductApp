namespace ProductApp.Service.Helper
{
    public static class GuidIdGenerator
    {
        public static string GenerateId() => Guid.NewGuid().ToString();
    }
}
