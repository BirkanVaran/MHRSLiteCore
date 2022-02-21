﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHRSLiteEntityLayer.Enums
{
    public class AllEnums
    {
    }

    public enum Genders : byte
    {
        Belirtilmemiş,
        Erkek,
        Kadın
    }

    public enum RoleNames : byte
    {
        Passive,
        Admin,
        Patient,
        PassiveDoctor,
        ActiveDoctor
    }
}
