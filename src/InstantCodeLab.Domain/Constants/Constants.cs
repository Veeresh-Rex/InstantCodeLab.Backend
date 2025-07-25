﻿using System.Collections.Generic;
using InstantCodeLab.Domain.Enums;

namespace InstantCodeLab.Domain.Constants;

public static class Constants
{
    public static readonly Dictionary<LanguageCode, int> LanguageVersions = new Dictionary<LanguageCode, int>
        {
            { LanguageCode.java, 5 },
            { LanguageCode.bash, 5 },
            { LanguageCode.c, 6 },
            { LanguageCode.csharp, 5 },
            { LanguageCode.cpp17, 2 },
            { LanguageCode.go, 5 },
            { LanguageCode.kotlin, 4 },
            { LanguageCode.nodejs, 6 },
            { LanguageCode.python3, 5 },
            { LanguageCode.ruby, 5 },
            { LanguageCode.rust, 5 },
            { LanguageCode.sql, 5 },
            { LanguageCode.typescript, 0 }
        };
}
