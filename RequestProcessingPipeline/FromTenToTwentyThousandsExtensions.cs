namespace RequestProcessingPipeline
{
    public static class FromTenToTwentyThousandsExtensions
    {
        public static IApplicationBuilder UseFromTenToTwentyThousands(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FromTenToTwentyThousandsMiddleware>();
        }
    }
}
