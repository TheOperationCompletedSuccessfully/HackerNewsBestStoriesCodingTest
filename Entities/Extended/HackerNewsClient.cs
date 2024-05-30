namespace HackerNewsBestStories.Entities.Extended
{
    public class HackerNewsClient : HttpClient
    {
        private readonly IConfiguration _configuration;
        
        public HackerNewsClient(IConfiguration configuration) : base()
        {
            _configuration = configuration;
            SafeAddHeaderFromConfigurationSetting("Accept", "v0MimicFirefoxHeaders:Accept");
            SafeAddHeaderFromConfigurationSetting("Accept-Encoding", "v0MimicFirefoxHeaders:Accept-Encoding");
            SafeAddHeaderFromConfigurationSetting("Accept-Language", "v0MimicFirefoxHeaders:Accept-Language");
            SafeAddHeaderFromConfigurationSetting("Connection", "v0MimicFirefoxHeaders:Connection");
            SafeAddHeaderFromConfigurationSetting("DNT", "v0MimicFirefoxHeaders:DNT");
            SafeAddHeaderFromConfigurationSetting("Host", "v0MimicFirefoxHeaders:Host");
            SafeAddHeaderFromConfigurationSetting("Priority", "v0MimicFirefoxHeaders:Priority");
            SafeAddHeaderFromConfigurationSetting("Sec-Fetch-Dest", "v0MimicFirefoxHeaders:Sec-Fetch-Dest");
            SafeAddHeaderFromConfigurationSetting("Sec-Fetch-Mode", "v0MimicFirefoxHeaders:Sec-Fetch-Mode");
            SafeAddHeaderFromConfigurationSetting("Sec-Fetch-Site", "v0MimicFirefoxHeaders:Sec-Fetch-Site");
            SafeAddHeaderFromConfigurationSetting("Sec-Fetch-User", "v0MimicFirefoxHeaders:Sec-Fetch-User");
            SafeAddHeaderFromConfigurationSetting("Sec-GPC", "v0MimicFirefoxHeaders:Sec-GPC");
            SafeAddHeaderFromConfigurationSetting("Upgrade-Insecure-Requests", "v0MimicFirefoxHeaders:Upgrade-Insecure-Requests");
            SafeAddHeaderFromConfigurationSetting("User-Agent", "v0MimicFirefoxHeaders:User-Agent");
        }

        private void SafeAddHeaderFromConfigurationSetting(string headerName,string configurationSettingName)
        {
            if (_configuration[configurationSettingName]!= null)
            {
                DefaultRequestHeaders.Add(headerName, _configuration[configurationSettingName]);
            }
        }
    }
}
