using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface IDal
    {
        IEngineer Engineer { get; }
        ITask Task { get; }
        IDependency Dependency { get; }
        public void Reset();
        public DateTime? ProjectStartDate { get; set; }

        public DateTime? ProjectEndDate { get; set; }
    }

    }
