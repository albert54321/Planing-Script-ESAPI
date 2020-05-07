using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Planning_Script_V1;

namespace Planning_Script_V3
{
    /// <summary>
    /// Lógica de interacción para Principal.xaml
    /// </summary>
    public partial class Principal : Window
    {
        private BackgroundWorker _bgWorker = new BackgroundWorker();//funciona

        public StructureSet ss;// eto es necesario para que me pasa del main el paciente
        public ScriptContext sc;
        public VMS.TPS.Planning_Creation xapply;

        public Principal()
        {
            InitializeComponent();
            //Combo.DrawMode = DrawMode.OwnerDrawVariable;
            //string selected = Combo.SelectedItem.ToString();
            xapply = xapply = new VMS.TPS.Planning_Creation();
            DataContext = xapply;



        }

        private void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Windows.MessageBox.Show("dentro" + Select_1.Content.ToString());
            xapply = new VMS.TPS.Planning_Creation();
            this.DataContext = xapply;
            List<VMS.TPS.Planning_Creation> dqm = VMS.TPS.Planning_Creation.Script();//lamo a la clase y la inicializo como es una lista 

            foreach (VMS.TPS.Planning_Creation x in dqm)
            {
                if (x.ID == Select_1.Content.ToString()) xapply.Start(x.number, sc, x.approved);
            }
            apply.IsEnabled = true;
            System.Windows.MessageBox.Show("Enjoy your new automatic plan");
        }

        public void apply_button(object sender, RoutedEventArgs e)

        {
            _bgWorker.DoWork += _bgWorker_DoWork;
            //xapply = new VMS.TPS.Planning_Creation();
            System.Windows.MessageBox.Show(Select_1.Content.ToString());

            _bgWorker.RunWorkerAsync();
            if (_bgWorker.IsBusy) { apply.IsEnabled = false; }
        }

        public void close_button(object sender, RoutedEventArgs e)
        {
            System.Windows.Window.GetWindow(this).Close();//cierra la ventana de la interface main
        }

        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Select_1.Content = Combo_planing.SelectedItem.ToString();//coloca en el boton de select el valor de lo escogido
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Credits cred = new Credits();
            WindowCredit cred = new WindowCredit(); // llamo la ventana de WPF de creditos
            cred.ShowDialog();
        }
        //public bool approved;

        private void Combo_points_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Select_1.Content = Combo_points.SelectedItem.ToString();//coloca en el boton de select el valor de lo escogido
        }

        private void execute_button(object sender, RoutedEventArgs e)
        {
            //VMS.TPS.Planning_Creation xapply = new VMS.TPS.Planning_Creation();
            List<VMS.TPS.Planning_Creation> dqm = VMS.TPS.Planning_Creation.Script();//lamo a la clase y la inicializo como es una lista 
                                                                                     //xapply.progress=pbs;////pbs es el progress bar se lo paso por aqui

            foreach (VMS.TPS.Planning_Creation x in dqm)
            {
                if (x.ID == Select_1.Content.ToString()) xapply.Start(x.number, sc, x.approved, true);//true es para que ejecute los puntos
            }
            System.Windows.MessageBox.Show("Finish points");
        }
    }

}
