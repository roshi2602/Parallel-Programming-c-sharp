using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


//parallel programming
// in which many processes are carried out simuntaneously
//for this- task must be independent and order of execution is not mattered

//2 types of parallelism
//1)data parallelism- operations are applied to each element in a collection
//ex-ParallelFor, Parallel.ForEach
//2)task parallelism- each process performs different functions
//ex-Parallen.Invoke


//Parallel For Loop
//in this, loop is going to execute on multiple threads
//in this, order of iteration is not going to be sequential
//syntax-
/*
int n=10;
Parallel.For(0,n,i){

});
*/

//import -using System.Threading;
//using System.Threading.Tasks

namespace Parallel_programming_c_sharp
{
    class Program
    {
        static void Main()
        {

            //parallel.For  loop 

            //first using for loop 
            Console.WriteLine("for loop");
            int n = 10;
            for (int i = 0; i < n; i++)
            {
                ////Thread.CurrentThread.ManagedThreadId returns an integer that 
                //represents a unique identifier for the current managed thread.
                Console.WriteLine($"{ i}, { Thread.CurrentThread.ManagedThreadId}");

                //now sleep the loop for 10 miliseconds
                Thread.Sleep(10);

            }
            Console.WriteLine(); //prinitng it


            //using Parallel for  loop
            Console.WriteLine("parallel for in loop");
            Parallel.For(0, n, i =>
            {
                Console.WriteLine($"{i}, {Thread.CurrentThread.ManagedThreadId}");

                //now sleep the loop for 10 miliseconds
                Thread.Sleep(10);
            });
            Console.ReadLine();

            //----------------------------------------------------------------------------
            //Degree of parallelism
            //to specify maimum number of threads to be used to execute a program
            //syntax- 
            /*  var op = new ParallelOptions()
             {
            MaxDegreeOfParallelism = 2
            };
            int n=10;
            Parallel.For(0,n, op,i=>{
            
            });

            */

            //MaxDegreeOfParallelism property affects the number of concurrent operations run by Parallel method calls
            //that are passed this ParallelOptions instance
        
            //cerating instance
            var op = new ParallelOptions()
            {
                //Limiting the maximum degree of parallelism to 2
                MaxDegreeOfParallelism = 2
        };

        //now use parallel For loop
        int num = 10 ;
        Parallel.For(0, num,op, j=>
            {
            Console.WriteLine(@"value of j {0},  thread ={1}", j, Thread.CurrentThread.ManagedThreadId);
                //now sleep the loop for 10 miliseconds
                Thread.Sleep(10);

            });

            Console.WriteLine("degree of parallelism completed");
            Console.ReadLine();

            //As we set the degree of parallelism to 2.
            //So, a maximum of 2 threads is used to execute the code that we can see from the above output.


            //-------------------------------------------------------------
            //Terminating parallel loop
            //break-complete all iterations on all threads that are before the current iteration on current thread
            //stop - stop all iterations


            //---------------------------------------------------------------
            //parallel.ForEach loop

            //this method executes multiple iterations at same time on different processors
            //syntax-
            /*  List<int> il = Enumerable.Range(1,10).ToList();
             
            Prallel.ForEach(il,i=>
            {
            
            });
            */

            //make 1 string
            List<int> ilt = Enumerable.Range(1, 10).ToList();
            //using parallel.foreach loop
            Parallel.ForEach(ilt, k =>
           {
               Console.WriteLine(@"value of k= {0}, Thread ={1}", k, Thread.CurrentThread.ManagedThreadId);

               //now sleep the loop for 10 miliseconds
               Thread.Sleep(10);

           });
            Console.WriteLine("parallel foreach loop completed");
            Console.ReadLine();

            //in output multiple threads will work


            //-------------------------------------------------------------
            //Degree of parallelism in ForEach

            //syntax for Degree of parallelism in ForEach

            var opt = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 2
            };


//create Parallel ForEach loop
            List<int> io = Enumerable.Range(1, 10).ToList();
            //using parallel.foreach loop
            Parallel.ForEach(io,opt, m =>
            {
                Console.WriteLine(@"value of m= {0}, Thread ={1}", m, Thread.CurrentThread.ManagedThreadId);

                //now sleep the loop for 10 miliseconds
                Thread.Sleep(10);
                    
            });
            Console.WriteLine("Degree of parallelism in ForEach");
            Console.ReadLine();

            //in output Whatever number of times we execute the above code, the number of threads will never go above 2.

            //----------------------------------------------------------------
            //Parallel Invoke
            //is used to launch multiple tasks that are going to be exceuted in parallel
            //it requires 1 static method

            //in output Each time you execute the code, you may get a different order of output. 


            //first create 1 static method for Parallel Invoke
            //Demo method made below

            Parallel.Invoke(
                //demo method called 3 times using Parallel Invoke method
                () => Demo(1),
                () => Demo(2),
                () => Demo(3)
                );
            Console.ReadLine();

            //----------------------------------------------------------

            //using Parallel options Class in Parallel.Invoke
            
            //first create 1 static method for Parallel Invoke
            //Demo method made below
            
            ParallelOptions pp = new ParallelOptions
            {
                MaxDegreeOfParallelism = 3
            };
            //pp.MaxDegreeOfParallelism = System.Environment.ProcessorCount - 1;

            Parallel.Invoke(
                //pass pp as first parameter
                pp,
              //demo method called 5 times using Parallel Invoke method
              () => Demo(1),
              () => Demo(2),
              () => Demo(3),
              () => Demo(4),
              () => Demo(5)
              );
            Console.ReadLine();
            //in output , first three tasks have started concurrently as we set the degree of parallelism to 3.
        }
        //--------------------------------------------------------------------

        //create 1 static method for Parallel Invoke
        public static void Demo(int number)
        {
            Console.WriteLine($"{number}started by thread,{Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(1000);
            Console.WriteLine($"{number}completed by thread,{Thread.CurrentThread.ManagedThreadId}");
        }
        //in output any thread can run
       //and output vary 
    }
}
