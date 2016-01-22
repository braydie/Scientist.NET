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
            Publishers = new List<IResultPublisher>();
        }

        public string Name { get; set; }
        public int PercentageEnabled { get; set; }
        public List<IResultPublisher> Publishers { get; set; } 
        
        internal void Publish(Result Result)
        {
            foreach (var ResultPublisher in Publishers)
            {
                ResultPublisher.Publish(Result);
            }
        }
    }
}