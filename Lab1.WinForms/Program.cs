using System;
using System.Windows.Forms;
using Lab1.BusinessLogic;
using Lab1.Presenter;
using Lab1.Shared;
using Ninject;

namespace Lab1.WinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IKernel ninjectKernel = new StandardKernel(new SimpleConfigModule());

            // Resolve logic and wrap it into IMainModel
            var logic = ninjectKernel.Get<Logic>();
            IMainModel model = new MainModel(logic);

            // Create view and presenter
            var mainForm = new MainForm();
            var presenter = new MainPresenter(mainForm, model);

            Application.Run(mainForm);
        }
    }
}
