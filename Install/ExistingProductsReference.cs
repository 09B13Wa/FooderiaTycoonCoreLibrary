using System.Collections.Generic;
using Microsoft.VisualBasic.CompilerServices;

namespace Install
{
    public class ExistingProductsReference
    {
        public Dictionary<string, (int, int, int)> CpuCaches;
        public Dictionary<string, (int, int, int, IntegratedGraphics)> CpuCores;
        public Dictionary<string, (Socket, int, int, int, int, int, int, Foundry)> CpuPhysical;
        public Dictionary<string, (int, int, int, int, bool, int, int)> CpuPerformance;

        public ExistingProductsReference()
        {
        }
    }
}