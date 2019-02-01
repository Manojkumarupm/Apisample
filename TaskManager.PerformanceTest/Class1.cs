using NBench;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.BL;
using TaskManager.DAL;

namespace TaskManager.PerformanceTest
{
    public class Class1
    {
        private const string SumCounterName = "AddCounter";
        private Counter addCounter;


        private const int AcceptableMinAddThroughput = 25000;

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            addCounter = context.GetCounter(SumCounterName);

        }
        [PerfBenchmark(RunMode = RunMode.Throughput, TestMode = TestMode.Test)]
        [CounterThroughputAssertion(SumCounterName, MustBe.LessThan, AcceptableMinAddThroughput)]
        public void GetUsersThroughput_ThroughputMode(BenchmarkContext context)
        {
            UserCrud usercrud = new UserCrud();
            List<User> user = usercrud.GetAllUser().ToList();
            addCounter.Increment();
        }
        [PerfBenchmark(RunMode = RunMode.Throughput, TestMode = TestMode.Test)]
        [CounterThroughputAssertion(SumCounterName, MustBe.LessThan, AcceptableMinAddThroughput)]
        public void GetParentTaskThroughput_ThroughputMode(BenchmarkContext context)
        {
            ParentTaskCrud parentTaskcrud = new ParentTaskCrud();
            List<ParentTask> parentTask = parentTaskcrud.GetAllParentTask().ToList();
            addCounter.Increment();
        }
        [PerfBenchmark(RunMode = RunMode.Throughput, TestMode = TestMode.Test)]
        [CounterThroughputAssertion(SumCounterName, MustBe.LessThan, AcceptableMinAddThroughput)]
        public void GetProjectThroughput_ThroughputMode(BenchmarkContext context)
        {
            ProjectCrud projectcrud = new ProjectCrud();

            List<Project> project = projectcrud.GetAllProject().ToList();
            addCounter.Increment();
        }
        [PerfBenchmark(RunMode = RunMode.Throughput, TestMode = TestMode.Test)]
        [CounterThroughputAssertion(SumCounterName, MustBe.LessThan, AcceptableMinAddThroughput)]
        public void GetTasksThroughput_ThroughputMode(BenchmarkContext context)
        {
            TaskCrud taskcrud = new TaskCrud();

            List<TaskInformation> task = taskcrud.GetAllTasks().ToList();
            addCounter.Increment();
        }

        [PerfCleanup]
        public void Cleanup(BenchmarkContext context)
        {
            //does nothing
        }
    }
}
