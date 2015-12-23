using System;

namespace Scientist
{
    public class Experiment<T> : BaseExperiment
    {
        private dynamic _Control;
        private dynamic _Candidate;
        private bool _RunIf;

        public Experiment(string ExperimentName) 
            : base(ExperimentName)
        {            
            _RunIf = true;
        }

        public void Use(Func<T> Control)
        {
            _Control = Control;
        }
        
        public void Try(Func<T> Candidate)
        {
            _Candidate = Candidate;
        }

        public void RunIf(bool RunIf)
        {
            _RunIf = RunIf;
        }

        public T Run()
        {
            var Result = new Result(this);
            Observation Candidate;
            Observation Control;
            var Rand = new Random();
            if(Rand.Next(0,2) == 0)
            {
                Control = ObserveControl();
                Result.Observations.Add(Control);
                Candidate = ObserveCandidate();
                if (Candidate != null)
                {
                    Result.Observations.Add(Candidate);
                }
            }
            else
            {
                Candidate = ObserveCandidate();
                if (Candidate != null)
                {
                    Result.Observations.Add(Candidate);
                }
                Control = ObserveControl();
                Result.Observations.Add(Control);
            }
            
            DoLogging(Result);
            return (T)Control.Result;
        }

        private Observation ObserveCandidate()
        {
            Observation Candidate = null;
            if (ExperimentShouldRun())
            {
                if (_RunIf)
                {
                    Candidate = new Observation("Candidate - " + this.Name, _Candidate);                    
                }
            }
            return Candidate;            
        }

        private Observation ObserveControl()
        {
            return new Observation("Control - " + this.Name, _Control);
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
