////////////////////////////////////////////////////////////////////////////////
// Script_APP.cs
//
//Script that generate Structures
//
//
// Copyright (c) 2018 Alberto Alarcon Paredes
// Mgter. Fisica Medica Instituto Balseiro
// Lic. en F�sica  Universidad Mayor de San Simon
////////////////////////////////////////////////////////////////////////////////
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using System.Reflection;//da el ensambly
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Media;//da los colores
using System.Windows.Forms;
using System.IO;

// TODO: uncomment the line below if the script requires write access.
[assembly: ESAPIScript(IsWriteable = true)]
[assembly: AssemblyVersion("1.0.0.87")]
[assembly: AssemblyFileVersion("1.0.0.87")]
[assembly: AssemblyInformationalVersion("1.0")]


namespace VMS.TPS
{
    public class Script
    {
        public Script()
        {
        }

        public void Execute(ScriptContext context, System.Windows.Window window/*, ScriptEnvironment environment*/)
        {
            /*SBRT_Prostate x = new SBRT_Prostate();
            string id = x.Class_ID;
            x.St_Prostate(context);*/


            string text = File.ReadAllText(@"U:\14-Scripts Eclipse\Nombres_e_Instructivos\Lic_NET_dot\Licence.txt", Encoding.UTF8);
            Planning_Creation text2 = new Planning_Creation();
            if (text != text2.Key) System.Windows.MessageBox.Show("You dont have a valid key");
                // TODO : Add here your code that is called when the script is launched from Eclipse
                StructureSet ss = context.StructureSet;
            context.Patient.BeginModifications();
            if (context.StructureSet == null) { throw new ArgumentNullException("Required input StructureSet is not available"); }
            if (context.StructureSet.Structures == null) { throw new ArgumentNullException("Required input Structures is not available"); }

            var MainControl = new Planning_Script_V1.UserScript();//llamo la ventana del mainwindow
            window.Content = MainControl;//le doy propiedades
            window.Width = MainControl.Width;
            window.Height = MainControl.Height;
            window.Title = "PLANNING SCRIPT Vs 1.0";
            MainControl.Patients.Content = context.Patient.Name;
            MainControl.ID.Content = context.Patient.Id;
            MainControl.StructSet.Content = context.StructureSet.Id;
            MainControl.ss = ss;//transfiere el paciente actual
            MainControl.sc = context;
            


            List<Planning_Creation> dqm = Planning_Creation.Script();//lamo a la clase y la inicializo como es una lista 
           
            foreach (Planning_Creation x in dqm)//puedo llamar elemento de cada lista
            {

                /*System.Windows.Forms.ComboBox cb = new System.Windows.Forms.ComboBox();
                cb.DrawMode
                cb.Items = s;
                cb.Foreground = Brushes.Blue;//coloca color a las letras
                cb.FontSize = 14;
                cb.Checked += MainControl.Cb_Checked;//coloca el click*/


                MainControl.Combo_planing.Items.Add(x.ID);

                //MainControl.Combo.Items[0].
                //MainControl.Combo.Items[0]
                //MainControl.approved = x.approved;
            }
            foreach (Planning_Creation x in dqm)
            {
                MainControl.Combo_points.Items.Add(x.ID);
            }
            //            MainControl.Combo.Items[0].
        }

    }
}