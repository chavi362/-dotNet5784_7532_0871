using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;

public record Dependency
(
    int id,
    int DependentTask,
    int DependensOnTask
    );
