namespace Dal;
using DalApi;
using DO;
internal static class Config
{
    static string s_data_config_xml = "data-config";
    internal static int NextTaskId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextTaskId"); }
    internal static int NextDependencyId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextDependencyId"); }
    internal static DateTime? projectBegining = new DateTime(2023, 1, 1); // Set your desired start date
    internal static DateTime? projectFinishing = new DateTime(2024, 12, 31); // Set your desired end date

}
