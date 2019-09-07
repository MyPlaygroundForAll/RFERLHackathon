using RFERL.Modules.Framework.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hackathon.Domain.Model
{
    public interface IDataRepository : IDomainDrivenRepository<Data, DateTime>
    {
    }
}
