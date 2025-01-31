using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    internal static class EntityExtensions
    {
        public static void Delete<T>(this DbSet<T> dbSet, T entity) where T : BaseEntity
        {
            entity.IsDeleted = true;
            dbSet.Update(entity);
        }        
    }
}
