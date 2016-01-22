using System;
using System.Collections.Generic;
using Scientist.Exceptions;

namespace Scientist
{
    public abstract class BaseExperiment
    {
        private bool _RunIf;

        internal BaseExperiment(string NameIn)
        {
            Name = NameIn;
            Publishers = new List<IResultPublisher>();
            _RunIf = true;
        }

        public string Name { get; set; }
        public int PercentageEnabled { get; set; }
        public List<IResultPublisher> Publishers { get; set; }
        internal dynamic Control { get; set; }
        internal dynamic Candidate { get; set; }

        public void RunIf(bool RunIf)
        {
            _RunIf = RunIf;
        }

        public dynamic Run()
        {
            if (Control == null)
            {
                throw new BehaviourMissingException(this, "Control");
            }
            var Result = new Result(this);
            Observation CandidateObservation;
            Observation ControlObservation;
            var Rand = new Random();
            if(Rand.Next(0,2) == 0)
            {
                ControlObservation = GetControlObservation();
                Result.Observations.Add(ControlObservation);
                CandidateObservation = GetCandidateObservation();
                if (CandidateObservation != null)
                {
                    Result.Observations.Add(CandidateObservation);
                }
            }
            else
            {
                CandidateObservation = GetCandidateObservation();
                if (CandidateObservation != null)
                {
                    Result.Observations.Add(CandidateObservation);
                }
                ControlObservation = GetControlObservation();
                Result.Observations.Add(ControlObservation);
            }

            Publish(Result);
            return ControlObservation.Result;
        }

        private Observation GetCandidateObservation()
        {
            Observation CandidateObservation = null;
            if (ExperimentShouldRun())
            {
                if (_RunIf)
                {
                    CandidateObservation = new Observation(this, "Candidate", Candidate);                    
                }
            }
            return CandidateObservation;            
        }

        private Observation GetControlObservation()
        {
            return new Observation(this, "Control", Control);
        }

        private bool ExperimentShouldRun()
        {
            var Rand = new Random();
            return PercentageEnabled > 0 && Rand.Next(100) < PercentageEnabled;
        }

        internal void Publish(Result Result)
        {
            foreach (var ResultPublisher in Publishers)
            {
                ResultPublisher.Publish(Result);
            }
        }
    }
}