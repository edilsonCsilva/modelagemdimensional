using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace ExtractorCore
{
    interface IDataBase
    {
        SQLiteConnection DbConnection();
    }
}
