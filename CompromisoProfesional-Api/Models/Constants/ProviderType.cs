namespace CompromisoProfesional_Api.Models.Constants
{
    public class ProviderType
    {
        public const string APPLIANCES = "APPLIANCES";
        public const string SUPPLIES = "SUPPLIES";

        public static bool Validate(string type)
        {
            return !string.IsNullOrEmpty(type) && typeof(ProviderType).GetFields().Any(f => f.GetValue(null)?.ToString() == type);
        }

        public static List<string> GetAll()
        {
            var types = new List<string>();

            foreach (var field in typeof(ProviderType).GetFields())
            {
                var value = field.GetValue(null)?.ToString();
                if (!string.IsNullOrEmpty(value))
                    types.Add(value);
            }

            return types;
        }
    }
}
