using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace WebAppTruck.Models.Services
{
  public class AccountSrv
  {
    private DBAccess cnx;
    public AccountSrv(string connectionString)
    {
      this.cnx = new DBAccess(connectionString);
    }
  }
}