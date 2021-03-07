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
        public IEnumerator Delay_Timer_Gets_Marked_As_Finished()
        {
            Timer timer = Timer.ScheduleDelay(1f, null);
            yield return new WaitForSeconds(1f);
            
            Assert.IsTrue(timer.IsFinished);
        }
        
        [UnityTest]
        public IEnumerator Delay_Timer_Is_Not_Marked_As_Finished_Early()
        {
            Timer timer = Timer.ScheduleDelay(10f, null);
            yield return new WaitForSeconds(1f);
            
            Assert.IsFalse(timer.IsFinished);
            timer.Stop();
        }
        
        [UnityTest]
        public IEnumerator Looping_Timer_Is_Not_Marked_As_Finished()
        {
            Timer timer = Timer.ScheduleLoop(1f, null);
            yield return new WaitForSeconds(2f);
            
            Assert.IsFalse(timer.IsFinished);
            timer.Stop();
        }
        
        [UnityTest]
        public IEnumerator Timescale_Effects_Scaled_Timer()
        {
            Timer timer = Timer.ScheduleDelay(1.5f, null, usesGameTimescale: true, runInstantly: false);
            Time.timeScale = 0.5f;
            yield return null;
            timer.Unpause();
            
            yield return new WaitForSecondsRealtime(2f);
            
            Assert.IsFalse(timer.IsFinished);
            timer.Stop();
            Time.timeScale = 1f;
        }
        
        [UnityTest]
        public IEnumerator Timescale_Does_Not_Effect_Unscaled_Timer()
        {
            Timer timer = Timer.ScheduleDelay(1.5f, null, runInstantly: false);
            Time.timeScale = 0.5f;
            yield return null;
            timer.Unpause();
            
            yield return new WaitForSecondsRealtime(2f);
            Assert.IsTrue(timer.IsFinished);
            Time.timeScale = 1f;
        }

        [UnityTest]
        public IEnumerator Start_Pause_Gets_Set()
        {
            Timer timer = Timer.ScheduleDelay(10f, null, runInstantly: false);
            yield return null;

            Assert.IsTrue(timer.IsPaused);
            Assert.IsFalse(timer.IsFinished);
        }
        
        [UnityTest]
        public IEnumerator Pause_State_Gets_Set()
        {
            Timer timer = Timer.ScheduleDelay(10f, null);
            yield return null;

            timer.Pause();
            
            Assert.IsTrue(timer.IsPaused);
            timer.Stop();
        }
        
        [UnityTest]
        public IEnumerator Unpause_State_Gets_Set()
        {
            Timer timer = Timer.ScheduleDelay(10f, null);
            yield return null;
            timer.Pause();

            yield return null;
            timer.Unpause();
            
            Assert.IsFalse(timer.IsPaused);
            timer.Stop();
        }
        
        [UnityTest]
        public IEnumerator Pause_State_Effects_Timer()
        {
            Timer timer = Timer.ScheduleDelay(10f, null);

            yield return null;
            timer.Pause();
            float timeBeforePause = timer.GetTimeSinceTimerStart();
            
            yield return new WaitForSeconds(1f);
            Assert.AreEqual(timeBeforePause, timer.GetTimeSinceTimerStart());
        }
    }
}
