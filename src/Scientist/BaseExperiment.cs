using System.Collections.Generic;

namespace Scientist
{
    public abstract class BaseExperiment
    {
        internal BaseExperiment()
        {
        }

        internal BaseExperiment(string NameIn)
        {
            Name = NameIn;
        }

        public string Name { get; set; }
        public int PercentageEnabled { get; set; }
        public List<IResultPublisher> Publishers { get; set; } 
    }
}