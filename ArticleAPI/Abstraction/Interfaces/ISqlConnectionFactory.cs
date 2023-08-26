using Microsoft.Data.SqlClient;

namespace ArticleAPI.Abstraction.Interfaces;

public interface ISqlConnectionFactory
{
    /// <summary>
    /// Create new sql connection without bothering yourself
    /// about connection string
    /// </summary>
    /// <returns></returns>
    SqlConnection CreateConnection();
}