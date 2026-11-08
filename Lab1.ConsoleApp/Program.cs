using System;
using Lab1.BusinessLogic;
using Lab1.Presenter;
using Ninject;

namespace Lab1.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IKernel ninjectKernel = new StandardKernel(new SimpleConfigModule());

            var logic = ninjectKernel.Get<Logic>();
            var model = new MainModel(logic);

            var view = new ConsoleView();
            var presenter = new MainPresenter(view, model);

            view.Run();
        }
    }
}
