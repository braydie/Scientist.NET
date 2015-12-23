using System.Collections.Generic;

namespace Scientist
{
    public class Result
    {
        public Result()
        {
            Observations = new List<Observation>();
        }

        public BaseExperiment Experiment { get; set; }
        public List<Observation> Observations { get; set; } 
    }
}
