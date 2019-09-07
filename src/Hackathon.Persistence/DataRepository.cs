using Hackathon.Domain.Model;
using Hackathon.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Persistence
{
    public class DataRepository : BaseRepository<HackathonDbContext, Data, DateTime>, IDataRepository
    {
        public DataRepository(HackathonDbContext context) 
            : base(context)
        {
        }

        public override IEnumerable<Data> Find(Expression<Func<Data, bool>> spec)
        {
            return Dbset
                .Include(i => i.Articles)
                .ToList();
        }

        public override async Task<IEnumerable<Data>> FindAsync(Expression<Func<Data, bool>> spec)
        {
            return await Dbset
                .Include(i => i.Articles)
                .ToListAsync();
        }

        public override Data FindById(DateTime id)
        {
            return Dbset
                .Include(i => i.Articles)
                .FirstOrDefault(d => d.Id.Equals(id));
        }

        public override async Task<Data> FindByIdAsync(DateTime id)
        {
            return await Dbset
                .Include(i => i.Articles)
                .FirstOrDefaultAsync(d => d.Id.Equals(id));
        }

        public override Data FindOne(Expression<Func<Data, bool>> spec)
        {
            return Dbset
                .Include(i => i.Articles)
                .FirstOrDefault(spec);
        }

        public override async Task<Data> FindOneAsync(Expression<Func<Data, bool>> spec)
        {
            return await Dbset
                .Include(i => i.Articles)
                .FirstOrDefaultAsync(spec);
        }
    }
}
