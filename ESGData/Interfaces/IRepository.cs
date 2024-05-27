using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESGData.Interfaces
{
    public interface IRepository<TModel, in TKey>
    {
        TModel Create(TModel model);
        TModel Get(TKey key);
    }
}
