﻿namespace Dal
{
    using DalApi;

    internal static class Config
    {
        static string s_data_config_xml = "data-config";
        internal static int NextTaskId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextTaskId"); }
        internal static int NextDependencyId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextDependencyId"); }
        internal static DateTime? projectBegining = XMLTools.LoadListFromXMLElement(@"..\xml\data-config.xml").ToDateTimeNullable("StartProjectDate");
        internal static DateTime? projectFinishing = XMLTools.LoadListFromXMLElement(@"..\xml\data-config.xml").ToDateTimeNullable("FinishProjectDate");
    }
}
