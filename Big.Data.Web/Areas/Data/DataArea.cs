using NewLife;
using NewLife.Cube;
using System.ComponentModel;

namespace Big.Data.Web.Areas.Data;

[DisplayName("大数据")]
public class DataArea : AreaBase
{
    public DataArea() : base(nameof(DataArea).TrimEnd("Area")) { }

    static DataArea()
    {
        RegisterArea<DataArea>();
    }
}