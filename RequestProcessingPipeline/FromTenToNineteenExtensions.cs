namespace RequestProcessingPipeline
{
    public static class FromTenToNineteenExtensions
    {
        public static IApplicationBuilder UseFromTenToNineteen(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FromTenToNineteenMiddleware>();
        }
    }
}
