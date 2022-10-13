using AutoMapper;
using Microsoft.Extensions.Options;

namespace _4JTools.AutoMapperMapping
{
    public class DateTimeTypeConverter : ITypeConverter<string, DateTime?>, ITypeConverter<DateTime?, string>, ITypeConverter<DateTime, string>
    {
        private readonly AutoMapperOptions _opts;

        public DateTimeTypeConverter(IOptions<AutoMapperOptions> opts)
        {
            _opts = opts.Value;
        }

        public DateTime? Convert(string source, DateTime? destination, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return null;
            }

            return DateTime.TryParse(source, out var dateTime) ? dateTime : null;
        }

        public string? Convert(DateTime? source, string destination, ResolutionContext context)
        {
            return (source == null) ? null : Convert(source.Value, destination, context);
        }

        public string Convert(DateTime source, string destination, ResolutionContext context)
        {
            return source.ToString(_opts.DateTimeConverterFormat);
        }
    }
}
