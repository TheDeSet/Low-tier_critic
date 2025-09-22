using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Lab_3._1
{
    public enum EnumPlatforms
    {
        [Description("PC")]
        PC,
        [Description("PlayStation")]
        PlayStation,
        [Description("PlayStation 2")]
        PlayStation2,
        [Description("PlayStation 3")]
        PlayStation3,
        [Description("PlayStation 4")]
        PLayStation4,
        [Description("PlayStation 5")]
        PlayStation5,
        [Description("Xbox")]
        Xbox,
        [Description("Xbox 360")]
        Xbox360,
        [Description("Xbox One")]
        XboxOne,
        [Description("Xbox Series")]
        XboxSeries,
        [Description("Nintendo Switch")]
        NintendoSwitch,
        [Description("Nintendo Switch 2")]
        NintendoSwitch2
    }
}
