using System;
using UnityEngine;

namespace SchloooLib.Temporal
{
    /// <summary>
    /// Main class to conduct delayed or time-based looped activities.
    /// </summary>
    public class Timer
    {
        /// <summary>
        /// The amount of seconds it will take to run one cycle.
        /// </summary>
        public float Duration { get; }
        /// <summary>
        /// Indicates whether the instance will loop or not.
        /// </summary>
        public bool IsLooping { get; }
        /// <summary>
        /// Indicates whether the instance uses the Unity timescale or real time.
        /// </summary>
        public bool UsesGameTimescale { get; }
        /// <summary>
        /// Indicates whether the instance has already finished or not.
        /// A <see cref="Timer"/> is set as finished when running through a not looping delay or when stopped manually.
        /// </summary>
        public bool IsFinished { get; private set; }
        /// <summary>
        /// Indicates if the instance is currently paused.
        /// The pause flag gets also set when the <see cref="Timer"/> is not run instantly.
        /// </summary>
        public bool IsPaused { get; private set; } 

        private Action onFinish;
        private Action<float> onTick;
        private float startupTime;
        private float lastUpdateTime;
        
        private float? elapsedTimeBeforePause;

        /// <summary>
        /// Constructs a new <c>Timer</c> instance running as an time-delayed loop.
        /// </summary>
        /// <param name="delayPerIteration">The amount of seconds one loop iteration will last.</param>
        /// <param name="onIterationFinish"><see cref="Action"/> delegate called whenever one iteration has finished.</param>
        /// <param name="onTick"><see cref="Action"/> called whenever the <see cref="RuntimeTimerConductor"/> casts an update cycle.</param>
        /// <param name="usesGameTimescale">Adjusts whether the Unity timescale will influence time measurement or not.</param>
        /// <param name="runInstantly">Adjusts whether the delay will run instantly or will be created in a paused state.</param>
        /// <returns>The resulting <c>Timer</c> instance.</returns>
        public static Timer ScheduleLoop(float delayPerIteration, Action onIterationFinish, Action<float> onTick = null, bool usesGameTimescale = false, bool runInstantly = true)
        {
            return new Timer(delayPerIteration, onIterationFinish, onTick, true, usesGameTimescale, runInstantly);
        }
        
        /// <summary>
        /// Constructs a new <c>Timer</c> instance running as an one-time delay.
        /// </summary>
        /// <param name="delayDuration">The amount of seconds the delay will last.</param>
        /// <param name="onFinish"><see cref="Action"/> delegate called whenever one iteration has finished.</param>
        /// <param name="onTick"><see cref="Action"/> called whenever the <see cref="RuntimeTimerConductor"/> casts an update cycle.</param>
        /// <param name="usesGameTimescale">Adjusts whether the Unity timescale will influence time measurement or not.</param>
        /// <param name="runInstantly">Adjusts whether the delay will run instantly or will be created in a paused state.</param>
        /// <returns>The resulting <c>Timer</c> instance.</returns>
        /// <returns></returns>
        public static Timer ScheduleDelay(float delayDuration, Action onFinish, Action<float> onTick = null, bool usesGameTimescale = false, bool runInstantly = true)
        {
            return new Timer(delayDuration, onFinish, onTick, false, usesGameTimescale, runInstantly);
        }

        protected Timer(float duration, Action onFinish, Action<float> onTick = null, bool isLooping = false, bool usesGameTimescale = false, bool runInstantly = true)
        {
            Duration = duration;
            this.onFinish = onFinish;
            this.onTick = onTick;
            IsLooping = isLooping;
            UsesGameTimescale = usesGameTimescale;
            IsFinished = false;

            IsPaused = !runInstantly;
            RuntimeTimerConductor.Instance.AddTimer(this);
        }
        
        /// <summary>
        /// Processes the time variables differentiating between running, paused and finished states.
        /// </summary>
        public void Tick()
        {
            if (IsFinished) return;

            if (IsPaused)
            {
                ProgressPauseState();
                return;
            }

            lastUpdateTime = GetGlobalTime();
            onTick?.Invoke(GetTimeSinceTimerStart());

            if (GetGlobalTime() >= GetFinishTime())
            {
                ProgressCycleFinish();
            }
            
        }
        
        private void ProgressPauseState()
        {
            startupTime += GetTickDeltaTime();
            lastUpdateTime = GetGlobalTime();
        }
        
        private float GetTickDeltaTime()
        {
            return GetGlobalTime() - lastUpdateTime;
        }
        
        private float GetGlobalTime()
        {
            return UsesGameTimescale ? Time.time : Time.realtimeSinceStartup;
        }

        /// <summary>
        /// Calculates the time that has passed since the current cycle has started.
        /// </summary>
        /// <returns></returns>
        public float GetTimeSinceTimerStart()
        {
            if (IsFinished || GetGlobalTime() >= GetFinishTime()) return Duration;
            
            return elapsedTimeBeforePause ?? GetGlobalTime() - startupTime;
        }
        
        private float GetFinishTime()
        {
            return startupTime + Duration;
        }

        private void ProgressCycleFinish()
        {
            onFinish?.Invoke();
            
            if (IsLooping)
            {
                startupTime = GetGlobalTime();
            }
            else
            {
                IsFinished = true;
            }
        }
        
        /// <summary>
        /// Pauses the <see cref="Timer"/>.
        /// Does nothing when the instance is paused already or has finished.
        /// </summary>
        public void Pause()
        {
            if (IsPaused || IsFinished) return;

            elapsedTimeBeforePause = GetTimeSinceTimerStart();
            IsPaused = true;
        }

        /// <summary>
        /// Unpauses the <see cref="Timer"/>.
        /// Does nothing when the instance is unpaused already or has finished.
        /// </summary>
        public void Unpause()
        {
            if (!IsPaused || IsFinished) return;

            elapsedTimeBeforePause = null;
            IsPaused = false;
        }

        /// <summary>
        /// Stops the <see cref="Timer"/>.
        /// A stopped instance can not be resumed later.
        /// </summary>
        public void Stop()
        {
            if (IsFinished) return;

            IsFinished = true;
        }
    }
}