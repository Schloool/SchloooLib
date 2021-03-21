using System.Collections;
using SchloooLib.Temporal;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TestTools;

namespace SchloooLib.Tests
{
    public class TimerTests
    {
        [UnityTest]
        public IEnumerator Delay_Gets_Marked_As_Finished()
        {
            Delay delay = Delay.ScheduleDelay(1f, null);
            yield return new WaitForSeconds(1f);
            
            Assert.IsTrue(delay.IsFinished);
        }
        
        [UnityTest]
        public IEnumerator Delay_Is_Not_Marked_As_Finished_Early()
        {
            Delay delay = Delay.ScheduleDelay(10f, null);
            yield return new WaitForSeconds(1f);
            
            Assert.IsFalse(delay.IsFinished);
            delay.Stop();
        }

        [UnityTest]
        public IEnumerator Timescale_Effects_Scaled_Timer()
        {
            Delay delay = Delay.ScheduleDelay(1.5f, null, usesGameTimescale: true, runInstantly: false);
            Time.timeScale = 0.5f;
            yield return null;
            delay.Unpause();
            
            yield return new WaitForSecondsRealtime(2f);
            
            Assert.IsFalse(delay.IsFinished);
            delay.Stop();
            Time.timeScale = 1f;
        }
        
        [UnityTest]
        public IEnumerator Timescale_Does_Not_Effect_Unscaled_Timer()
        {
            Delay delay = Delay.ScheduleDelay(1.5f, null, runInstantly: false);
            Time.timeScale = 0.5f;
            yield return null;
            delay.Unpause();
            
            yield return new WaitForSecondsRealtime(2f);
            Assert.IsTrue(delay.IsFinished);
            Time.timeScale = 1f;
        }

        [UnityTest]
        public IEnumerator Start_Pause_Gets_Set()
        {
            Delay delay = Delay.ScheduleDelay(10f, null, runInstantly: false);
            yield return null;

            Assert.IsTrue(delay.IsPaused);
        }
        
        [UnityTest]
        public IEnumerator Pause_State_Gets_Set()
        {
            Delay delay = Delay.ScheduleDelay(10f, null);
            yield return null;

            delay.Pause();
            
            Assert.IsTrue(delay.IsPaused);
            delay.Stop();
        }
        
        [UnityTest]
        public IEnumerator Unpause_State_Gets_Set()
        {
            Delay delay = Delay.ScheduleDelay(10f, null);
            yield return null;
            delay.Pause();

            yield return null;
            delay.Unpause();
            
            Assert.IsFalse(delay.IsPaused);
            delay.Stop();
        }
        
        [UnityTest]
        public IEnumerator Pause_State_Effects_Timer()
        {
            Delay delay = Delay.ScheduleDelay(10f, null);

            yield return null;
            delay.Pause();
            float timeBeforePause = delay.GetTimeSinceTimerStart();
            
            yield return new WaitForSeconds(1f);
            Assert.AreEqual(timeBeforePause, delay.GetTimeSinceTimerStart());
        }

        [UnityTest]
        public IEnumerable Looping_Timer_Is_Not_Marked_As_Finished()
        {
            TimeLoop loop = TimeLoop.ScheduleLoop(1f, null);
            
            yield return new WaitForSeconds(2f);
            
            Assert.IsFalse(loop.IsFinished);
            loop.Stop();
        }
        
        [UnityTest]
        public IEnumerable Counting_Loop_Uses_Iteration_Counter()
        {
            int counter = 0;
            CountingTimeLoop loop = CountingTimeLoop.ScheduleCountingLoop(3, 0.1f, iteration => counter++);
            
            yield return new WaitForSeconds(1f);
            
            Assert.AreEqual(3, counter);
        }
    }
}
