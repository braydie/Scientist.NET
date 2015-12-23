using System;
using System.Collections.Generic;

namespace Scientist
{
    public class Experiment
    {
        private dynamic _Control;
        private dynamic _Candidate;
        private bool _RunIf;

        public Experiment(string ExperimentName)
        {
            Name = ExperimentName;
            _RunIf = true;
        }

        public string Name { get; private set; }
        public int PercentageEnabled { get; set; }
        public List<IResultPublisher> Publishers { get; set; } 

        public void Use<T>(Func<T> Control)
        {
            _Control = Control;
        }
        
        public void Try<T>(Func<T> Candidate)
        {
            _Candidate = Candidate;
        }

        public void RunIf(bool RunIf)
        {
            _RunIf = RunIf;
        }

        public T Run<T>()
        {
            var Result = new Result {Experiment = this};
            if (ExperimentShouldRun())
            {
                if (_RunIf)
                {                    
                    var Candidate = new Observation("candidate", _Candidate);
                    Result.Observations.Add(Candidate);
                }
            }
            
            var Control = new Observation("control", _Control);
            Result.Observations.Add(Control);
            DoLogging(Result);
            return (T)Control.Result;
        }

        private bool ExperimentShouldRun()
        {
            var Rand = new Random();
            return PercentageEnabled > 0 && Rand.Next(100) < PercentageEnabled;
        }

        private void DoLogging(Result Result)
        {
            foreach (var ResultPublisher in Publishers)
            {
                ResultPublisher.Publish(Result);
            }
        }
    }    
}
