namespace RequestProcessingPipeline
{
    public static class FromTwentyToHundredThousandsExtensions
    {
        public static IApplicationBuilder UseFromTwentyToHundredThousands(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FromTwentyToHundredThousandsMiddleware>();
        }
    }
}
