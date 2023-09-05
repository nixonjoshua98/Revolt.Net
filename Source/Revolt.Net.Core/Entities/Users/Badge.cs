﻿namespace Revolt.Net.Core.Entities.Users
{
    [Flags]
    public enum Badge
    {
        Developer = 1,
        Translator = 2,
        Supporter = 4,
        ResponsibleDisclosure = 8,
        EarlyAdopter = 256
    }
}