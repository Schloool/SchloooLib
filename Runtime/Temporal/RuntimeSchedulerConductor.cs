using System.Collections.Generic;
using SchloooLib.Core;

namespace SchloooLib.Temporal
{
    /// <summary>
    /// Central controller to register and update <see cref="Delay"/> instances.
    /// </summary>
    public sealed class RuntimeSchedulerConductor : Singleton<RuntimeSchedulerConductor>
    {
        private List<AbstractScheduler> schedulers = new List<AbstractScheduler>();
        private List<AbstractScheduler> schedulerBacklog = new List<AbstractScheduler>();

        private void Update()
        {
            ProgressBacklog();
            TickSchedulers();
        }
        
        private void ProgressBacklog()
        {
            if (schedulerBacklog.Count <= 0) return;
            
            schedulerBacklog.ForEach(backlogScheduler => schedulers.Add(backlogScheduler));
            schedulerBacklog.Clear();
        }

        private void TickSchedulers()
        {
            schedulers.ForEach(scheduler => scheduler.Tick());
            schedulers.RemoveAll(scheduler => scheduler.IsFinished);
        }

        /// <summary>
        /// Registers a new <see cref="Delay"/> that will be registered to be updated.
        /// </summary>
        /// <param name="scheduler">The new scheduler instance.</param>
        public void AddTimer(AbstractScheduler scheduler)
        {
            schedulerBacklog.Add(scheduler);
        }

        /// <summary>
        /// Stops all <see cref="AbstractScheduler"/>s controlled by this <see cref="RuntimeSchedulerConductor"/> instance.
        /// </summary>
        public void StopAllTimers()
        {
            schedulers.ForEach(timer => timer.Stop());
            schedulers.Clear();
        }
    }
}