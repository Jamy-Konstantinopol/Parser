using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    // Класс, объекты которого будут сохранены в файл result.csv
    internal class TaskReward
    {
        public string? ListName { get; set; }
        public string? ObjectName { get; set; }
        public int Money { get; set; }
        public int Details { get; set; }
        public int Reputation { get; set; }
        public bool IsUsed { get; set; }
    }
}
