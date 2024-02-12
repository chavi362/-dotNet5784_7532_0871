namespace Dal
{
    using DalApi;

    // Config class containing configuration settings
    internal static class Config
    {
        // XML file name for configuration data
        static string s_data_config_xml = "data-config";

        // Property to get and increase the next task ID
        internal static int NextTaskId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextTaskId"); }

        // Property to get and increase the next dependency ID
        internal static int NextDependencyId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextDependencyId"); }

        // Property to get the project beginning date from the XML configuration file
        internal static DateTime? projectBegining = XMLTools.LoadListFromXMLElement(@"..\xml\data-config.xml").ToDateTimeNullable("StartProjectDate");

        // Property to get the project finishing date from the XML configuration file
        internal static DateTime? projectFinishing = XMLTools.LoadListFromXMLElement(@"..\xml\data-config.xml").ToDateTimeNullable("FinishProjectDate");
    }
}
