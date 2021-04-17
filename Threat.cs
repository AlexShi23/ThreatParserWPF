using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class Threat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public string Target { get; set; }
        public bool BreachOfConf { get; set; }
        public bool BreachOfIntegrity { get; set; }
        public bool BreachOfAccess { get; set; }

        public Threat(int id, string name, string description, string source, string target, bool breachOfConf, bool breachOfIntegrity, bool breachOfAccess)
        {
            Id = id;
            Name = name;
            Description = description;
            Source = source;
            Target = target;
            BreachOfConf = breachOfConf;
            BreachOfIntegrity = breachOfIntegrity;
            BreachOfAccess = breachOfAccess;
        }

        public Threat()
        {
        }
    }
}
