using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccessLayer.Globals
{
    public class GlobalParameter
    {
        public static string GetParameterValue(string parameterName, SqlDataReader reader)
        {
            int PathIndex = reader.GetOrdinal(parameterName);
            return reader.IsDBNull(PathIndex) ? "" : reader.GetString(PathIndex);

        }

    }
}
