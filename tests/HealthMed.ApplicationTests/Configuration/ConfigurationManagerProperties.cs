namespace HealthMed.ApplicationTests.Configuration
{
    public class ConfigurationManagerProperties
    {
        public static IEnumerable<KeyValuePair<string, string?>> GetConfigurationProperties()
        {
            var configurationProperties = new List<KeyValuePair<string, string>>
            {
                GetJwtConfiguration()
            };

            return configurationProperties;
        }

        private static KeyValuePair<string, string> GetJwtConfiguration()
        {
            var secretJwtKey = "SecretJWT";
            var secretJwtValue = "001ade8b-7f95-4b86-902d-51d0867e1bc9";

            return new KeyValuePair<string, string>(secretJwtKey, secretJwtValue);
        }
    }
}
