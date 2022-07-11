using System;

namespace DemoProjectForOOB2
{
    class Program
    {
        class baseClass
        {

            // show() is 'virtual' here
            public virtual void show()
            {
                Console.WriteLine("Base class");
            }
        }


        // class 'baseClass' inherit 
        // class 'derived'
        class derived : baseClass
        {

            //'show()' is 'override' here
            public override void show()
            {
                Console.WriteLine("Derived class");
            }
        }

        class GFG
        {

            static void Main(string[] args)
            {
                baseClass obj;

                // 'obj' is the object
                // of class 'baseClass'
                obj = new baseClass();

                // it invokes 'show()' 
                // of class 'baseClass'
                obj.show();


                // the same object 'obj' is now
                // the object of class 'derived'
                obj = new derived();

                // it invokes 'show()' of class 'derived'
                // 'show()' of class 'derived' is overridden
                // for 'override' modifier
                obj.show();

            }
        }
    }
}
