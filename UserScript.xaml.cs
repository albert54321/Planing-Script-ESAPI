using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Application = VMS.TPS.Common.Model.API.Application;

namespace Planning_Script_V1
{
    /// <summary>
    /// Lógica de interacción para UserScript.xaml
    /// </summary>
    public partial class UserScript : System.Windows.Controls.UserControl
    {
        public UserScript()
        {
            InitializeComponent();
            //Combo.DrawMode = DrawMode.OwnerDrawVariable;
            //string selected = Combo.SelectedItem.ToString();
        }
        public StructureSet ss;
        public ScriptContext sc;

        public void apply_button(object sender, RoutedEventArgs e)

        {
            VMS.TPS.Planning_Creation xapply = new VMS.TPS.Planning_Creation();
            List<VMS.TPS.Planning_Creation> dqm = VMS.TPS.Planning_Creation.Script();//lamo a la clase y la inicializo como es una lista 
            //xapply.progress=pbs;////pbs es el progress bar se lo paso por aqui
            pbs.Value = xapply.progress;

            foreach (VMS.TPS.Planning_Creation x in dqm)
            {
                if (x.ID == Select_1.Content.ToString()) xapply.start(x.number, sc, x.approved);
            }

            System.Windows.MessageBox.Show("Enjoy your new automatic plan");
        }

        public void close_button(object sender, RoutedEventArgs e)
        {
           /* var curso = sc.Patient.Courses.FirstOrDefault(x=>x.Id.Contains("SBRT_Prostate"));//para hacer lo del punto de referencia 
            
            System.Windows.MessageBox.Show(curso.Id);
            var plans =     curso.PlanSetups;
            var punto3 = sc.Patient.Courses.FirstOrDefault(x => x.Id.Contains("SBRT_Prostate")).PlanSetups.FirstOrDefault(y => y.Id.Contains("SBRT_Prostata")).AddReferencePoint(sc.Patient.Courses.FirstOrDefault(x => x.Id.Contains("SBRT_Prostate")).PlanSetups.FirstOrDefault(y => y.Id.Contains("SBRT_Prostata")).StructureSet.Structures.FirstOrDefault(y => y.Id.Contains("PTV_High_3625")), null, "PTV_High_3625", "PTV_High_3625");

           foreach (var x in plans)
            {
                if (x != null)
                {
                    var punto = x.AddReferencePoint(x.StructureSet.Structures.FirstOrDefault(y=>y.Id.Contains("PTV_High_3625")), null,"PTV_High_3625", "PTV_High_3625");
                    punto.TotalDoseLimit = new DoseValue(36.25,DoseValue.DoseUnit.Gy);
                    punto.SessionDoseLimit = new DoseValue(7.25, DoseValue.DoseUnit.Gy);
                    punto.DailyDoseLimit = new DoseValue(7.25, DoseValue.DoseUnit.Gy);

                    System.Windows.MessageBox.Show(x.Id);
                }
                else System.Windows.MessageBox.Show("0");
            }*/
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
        private void Combo_DrawItem(object sender, DrawItemEventArgs e)//no entra esta funcion no se porque
        {
            //string name = Combo.Items(e.Index);
            /*e.DrawBackground();
            System.Drawing.Brush brush;
            // Get the item text.

            string text = ((System.Windows.Controls.ComboBox)sender).Items[e.Index].ToString();
            System.Windows.MessageBox.Show(text);
            if (true)
            {
                brush = System.Drawing.Brushes.Green;
            }
            else
            {
                    brush = System.Drawing.Brushes.Red;
            }
            // Draw the text.

            e.Graphics.DrawString(text, ((System.Windows.Forms.Control)sender).Font, brush, e.Bounds.X, e.Bounds.Y);
            e.DrawFocusRectangle();*/
            e.DrawBackground();
            List<VMS.TPS.Planning_Creation> dqm = VMS.TPS.Planning_Creation.Script();//lamo a la clase y la inicializo como es una lista 
            var comboBox = (System.Windows.Forms.ComboBox)sender;
            var fontFamily = (System.Drawing.FontFamily)comboBox.Items[e.Index];
            var font = new Font(fontFamily, comboBox.Font.SizeInPoints);
            int b = 0;
            foreach (VMS.TPS.Planning_Creation x in dqm)
            {
                if (x.approved) e.Graphics.DrawString(comboBox.Items[b].ToString(), font, System.Drawing.Brushes.Green, e.Bounds);
                else e.Graphics.DrawString(comboBox.Items[b].ToString(), font, System.Drawing.Brushes.Red, e.Bounds); ;
                b += 1;
            }
        }
        private void Pbs_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void Combo_points_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Select_1.Content = Combo_points.SelectedItem.ToString();//coloca en el boton de select el valor de lo escogido
        }

        private void execute_button(object sender, RoutedEventArgs e)
        {
            VMS.TPS.Planning_Creation xapply = new VMS.TPS.Planning_Creation();
            List<VMS.TPS.Planning_Creation> dqm = VMS.TPS.Planning_Creation.Script();//lamo a la clase y la inicializo como es una lista 
            //xapply.progress=pbs;////pbs es el progress bar se lo paso por aqui
            pbs.Value = xapply.progress;

            foreach (VMS.TPS.Planning_Creation x in dqm)
            {
                if (x.ID == Select_1.Content.ToString()) xapply.start(x.number, sc, x.approved,true);//true es para que ejecute los puntos
            }
            System.Windows.MessageBox.Show("Finish points");
        }
    }
}
