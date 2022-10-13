using _4JTools.Extensions.Linq;

namespace _4JTools.Extensions.String
{
    public static class ConvertExtensions
    {
        public static IEnumerable<string> ToStringTypeValues(this IEnumerable<string> values, Type type, out bool valid)
        {
            var allValid = true;

            var convertedValues = new List<string>();

            values?.ForEach(value =>
            {
                var stringTypeValue = value.ToStringTypeValue(type, out bool valueValid);

                if (stringTypeValue != null)
                {
                    convertedValues.Add(stringTypeValue);
                }

                allValid = allValid && valueValid;
            });

            valid = allValid;

            return convertedValues.ToArray();
        }

        public static string? ToStringTypeValue(this string value, Type type, out bool valid)
        {
            valid = true;

            try
            {
                if (type == typeof(string))
                {
                    return $"\"{value}\"";
                }

                if (type == typeof(DateTime))
                {
                    var datetime = DateTime.Parse(value);

                    return $"DateTime({datetime.Year}, {datetime.Month}, {datetime.Day}, {datetime.Hour}, {datetime.Minute}, {datetime.Second}).Date";
                }

                if (int.TryParse(value, out var number))
                {
                    return type.IsEnum
                        ? number.ToString()
                        : Convert.ChangeType(number, type).ToString();
                }

                return type.IsEnum
                    ? Convert.ToInt32(Enum.Parse(type, value, true)).ToString()
                    : Convert.ChangeType(value, type).ToString();
            }
            catch (Exception)
            {
                valid = false;

                return null;
            }
        }
    }
}
