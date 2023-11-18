namespace RequestProcessingPipeline
{
    public static class FromHundredToNineHundredExtensions
    {
        public static IApplicationBuilder UseFromHundredToNineHundred(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FromHundredToNineHundredMiddleware>();
        }
    }
}
