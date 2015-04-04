using ODataWebAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODataWebAPI.Data.Configurations
{
    public class CompanyConfiguration: EntityTypeConfiguration<Company>
    {
        public CompanyConfiguration()
        {
            ToTable("Companies");
        }
    }
}
