using System.Collections.Generic;
using SchloooLib.Core;

namespace SchloooLib.Temporal
{
    public sealed class RuntimeTimerConductor : Singleton<RuntimeTimerConductor>
    {
        private List<Timer> timers = new List<Timer>();
        private List<Timer> timerBacklog = new List<Timer>();

        private void Update()
        {
            ProgressBacklog();
            TickTimers();
        }
        
        private void ProgressBacklog()
        {
            if (timerBacklog.Count <= 0) return;
            
            timerBacklog.ForEach(backlogTimer => timers.Add(backlogTimer));
            timerBacklog.Clear();
        }

        private void TickTimers()
        {
            timers.ForEach(timer => timer.Tick());
            timers.RemoveAll(timer => timer.IsFinished);
        }

        public void AddTimer(Timer timer)
        {
            timerBacklog.Add(timer);
        }

        public void StopAllTimers()
        {
            timers.ForEach(timer => timer.Stop());
            timers.Clear();
        }
    }
}