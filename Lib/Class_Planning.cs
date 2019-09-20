////////////////////////////////////////////////////////////////////////////////
// Prostate
//
//  A ESAPI v15.1+ script that demonstrates optimization structure creation.
//
// Applies to:
//      Eclipse Scripting API
//          15.1.1
//          15.5
//
// Copyright (c) 2018 Alberto Alarcon Paredes
// Mgter. Fisica Medica Instituto Balseiro
// Lic. en Física  Universidad Mayor de San Simon
////////////////////////////////////////////////////////////////////////////////
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Planning_Script_V1;
/**
* @author $AlbertoAlarcon$
*
* @date - $2018$ 
*/
namespace VMS.TPS//tiene que ser igual que el main
{
    public class Planning_Creation
    {
        public Planning_Creation(/*System.Windows.Controls.ProgressBar progress*/) { } //esto es para poder invocar a la clase//le paso el boton de progresso.
        public string ID { get; set; }//nombre del script a ejecutar
        public bool approved { get; set; }// si esta aprobado
        public string SCRIPT_NAME { get; set; }//el nombre que muestra en la applicacion
        public int number { get; set; }
        public System.Windows.Controls.ProgressBar progress { get; set; }
        
        //La lista de script es aqui
        public static List<Planning_Creation> Script()//ScriptContext sc) //hay que aprobar el script aqui sino no va correr
        {
            List<Planning_Creation> list = new List<Planning_Creation>();
            list.Add(new Planning_Creation//esto es una lista de clases dentro
            {
                ID = "Script_Breast_ChestWall(Mama/Pared/ParedExpansor)",//nombre que aparece en la lista de la interfaz grafica
                approved = true,//si el script esta aprobado o no
                SCRIPT_NAME = "Breast_ChestWall_Structures",//nombre de las advertencias 
                number = 1//nombre del script se enlaza con Start.
            });
            list.Add(new Planning_Creation
            {
                ID = "Script_SBRT_Prostate(SBRT_prostata)",
                approved = true,
                SCRIPT_NAME = "Prostate_Structures",
                number = 0
            });
            list.Add(new Planning_Creation
            {
                ID = "Script_Rectum20Fx(Recto)",
                approved = true,
                SCRIPT_NAME = "Rectum_Structures",
                number = 2
            });
            list.Add(new Planning_Creation
            {
                ID = "Script_Head_Neck25Fx(CYC)",
                approved = true,
                SCRIPT_NAME = "Head_Neck_Structures",
                number = 3
            });
            list.Add(new Planning_Creation
            {
                ID = "Script_Cervix20Fx(CuelloUtero)",
                approved = true,
                SCRIPT_NAME = "Cervix_Structures",
                number = 4
            });
            list.Add(new Planning_Creation
            {
                ID = "Script_Prostate_Combo_HDR_Fx15(Braqui)",
                approved = true,
                SCRIPT_NAME = "Prostate_ComboHDR_Structures",
                number = 5
            });
            list.Add(new Planning_Creation
            {
                ID = "Script_ProstateBed_Fx20(Lecho)",
                approved = true,
                SCRIPT_NAME = "ProstateBed_Structures",
                number = 6
            });
            list.Add(new Planning_Creation
            {
                ID = "Script_Bladder_Fx20(Vejiga)",//saint fausto
                approved = true,
                SCRIPT_NAME = "Bladder_Structures",
                number = 7
            });
            list.Add(new Planning_Creation
            {
                ID = "Script_Endometrium_Fx20(Endometrio)",//saint fausto
                approved = true,
                SCRIPT_NAME = "Bladder_Structures",
                number = 8
            });
            list.Add(new Planning_Creation
            {
                ID = "Script_Liver_Fx3(Higado)",//saint fausto
                approved = true,
                SCRIPT_NAME = "Bladder_Structures",
                number = 9
            });
            return list;
        } //ESTAN LIGADOS EL NUMBER AQUI
        //comienzo del script strutures
        public void start(int template, ScriptContext context, bool appr)
        {//ejecuta el scrit seleccionado en User script.c   LIGADO CON VALOR DE TEMPLATE
            if (template == 0 && appr) Plan_Prostate_5Fx(context);
            //else if (template == 1 && appr) St_Breast_ChestWall(context);
            //else if (template == 2 && appr) St_Breast_ChestWall_RA(context);
            else if (template == 2 && appr) Plan_Rectum_20Fx(context);
            /*else if (template == 3 && appr) St_CYC_25fx(context);
            else if (template == 4 && appr) St_Cervix_25fx(context);
            else if (template == 5 && appr) St_HDR_15fx(context);//
            else if (template == 6 && appr) St_Lecho_20fx(context);
            else if (template == 7 && appr) St_Bladder_20fx(context);//
            else if (template == 8 && appr) St_Endometrium_20fx(context);//endometrium
            else if (template == 9 && appr) St_Higado_3fx(context);//endometrium*/
            else System.Windows.MessageBox.Show("The Script does not approved for clinical use ");
        }// Aqui hay que implementar el nuevo script de estructuras y correlacionar los numeros sino no se ejecuta
        public void VerifSt(Structure st1, bool need, string name)//cambia el nombre y convierte en alta resolucion
        {
            if (st1 == null || st1.IsEmpty)
            {
                System.Windows.MessageBox.Show(string.Format("'{0}' not found!", name), SCRIPT_NAME, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                if (need == true)
                {
                    System.Windows.MessageBox.Show("Script doesnt execute");
                }
            }
            else
            {
                if (name != "Body" && name != "Outer Contour" && st1.CanConvertToHighResolution()) st1.ConvertToHighResolution();
                try
                {
                    if (st1.Id != name) st1.Id = name;
                }
                catch (Exception e)
                {
                    st1.Id = name + ".";
                }
                //if (st1.Id != name) st1.Id = name;              
            }
        }
        public void VerifStLow(Structure st1, bool need, string name)//cambia el nombre y NO convierte en alta resolucion
        {
            if (st1 == null || st1.IsEmpty)
            {
                System.Windows.MessageBox.Show(string.Format("'{0}' not found!", name), SCRIPT_NAME, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                if (need == true)
                {
                    System.Windows.MessageBox.Show("Script doesnt execute");
                }
            }
            else
            {
                try
                {
                    if (st1.Id != name) st1.Id = name;
                }
                catch (Exception e)
                {
                    st1.Id = name + ".";
                }

            }
        }
        public bool HighResol(Structure s)//verifica su una estructura es alta resol devuelve a= false si es alta resol
        {
            bool a;
            if (s == null || s.IsEmpty) a = true;
            else
            {
                if (s.IsHighResolution) a = false;
                else a = true;
            }
            return a;
            
        }

        public void Cropbody(Structure st1, Structure body1)
        {
            if (st1 != null && body1 != null && !st1.IsEmpty)   /// Esto es para que 
            {
                st1.SegmentVolume = body1.And(st1);
            }
        }//crop 0.5cm del body

        public bool IsResim(StructureSet ss)
        {
            bool resim = false;
            if (ss.Structures.Any(x => x.Id.Contains("PTV")))
            {
                resim = true;
            }
            return resim;
        }


        //////////////////////funviones propias del planning
        private double RedondeoArriba(double valor, double paso = 0.5)//redondea a 0.5
        {
            return Math.Ceiling(valor / paso) * paso * 10;
        }

        public void Jaws_corrected(Beam VMAT, Structure ptv_total)
        {
            VMAT.FitCollimatorToStructure(new FitToStructureMargins(5), ptv_total, true, true, false);
            BeamParameters beampar = VMAT.GetEditableParameters();//obtiene los beam parametros del arco 1 como ser losjaws isocentro etc
            //BEAMs
            var controlpoint = beampar.ControlPoints.ElementAt<ControlPointParameters>(0);//obtiene los control point ahi estan los jaws position no se porque funciona con var
            double X1 = controlpoint.JawPositions.X1;//posicion jaws vmat1
            double X2 = controlpoint.JawPositions.X2;
            double Y1 = controlpoint.JawPositions.Y1;
            double Y2 = controlpoint.JawPositions.Y2;
            VRect<double> jaws = new VRect<double>(X1, Y1, X2, Y2);//vrect sirve para applyparameters despues
            if (Y1 <= -107.000001)//-107.00001)
            {
                jaws = new VRect<double>(jaws.X1, -107.0, jaws.X2, jaws.Y2);
            }
            if (Y2 >= 107.000001)//107.00001)
            {
                jaws = new VRect<double>(jaws.X1, jaws.Y1, jaws.X2, 107.0);
            }
            beampar.SetJawPositions(jaws);
            VMAT.ApplyParameters(beampar);
        }

        public ExternalPlanSetup settingPlan(int machine, Patient patient, StructureSet sset, double RxDose, int NFractions, Structure ptv_total, string[] setting_names, Double[] setting_arc, Double[] c,int arc_number = 1 )
        {
            patient.BeginModifications();   // enable writing with this script.
            IEnumerable<Course> sss = patient.Courses;//lista de cursos

            Course curcourse;//creo la  clase course porque no se puede crear dentro de condicional

            if (!sss.Any(x => x.Id == setting_names[0]))//para ver si existen cursos con el mismo nombre
            {
                curcourse = patient.AddCourse();
                curcourse.Id = setting_names[0];
            }
            else
            {// sino se pone corchetes no te deja crear clases
                curcourse = sss.FirstOrDefault(s => s.Id == setting_names[0]);
            }
            ExternalPlanSetup cureps = curcourse.AddExternalPlanSetup(sset);
            cureps.Id = setting_names[1];
            //set calculation model use default??? nose
            cureps.SetCalculationModel(CalculationType.PhotonVMATOptimization, "PO_15151");
            cureps.SetCalculationModel(CalculationType.DVHEstimation, "DVH Estimation Algorithm [15.1.51]");
            cureps.SetCalculationModel(CalculationType.PhotonVolumeDose, "AAA_15151");//CalculationGridSizeInCM 
            cureps.SetCalculationOption("AAA_15151", "CalculationGridSizeInCM", "0.25");
            cureps.SetCalculationOption("AAA_15151", "HeterogeneityCorrection", "ON");
            cureps.SetPrescription(NFractions, new DoseValue(RxDose / NFractions, "Gy"), 1.0);//prescription 0.99=99 %tratamiento sip
            progress.Value = 10;
            //esto da en mm
            VVector isocenter = new VVector(RedondeoArriba(Math.Round(ptv_total.CenterPoint.x+c[0], 0) / 10.0), RedondeoArriba(Math.Round(ptv_total.CenterPoint.y+c[1], 0) / 10.0), RedondeoArriba(Math.Round(ptv_total.CenterPoint.z + c[2], 0) / 10.0));//c es la ctte para prostata para bajar el iso
            ExternalBeamMachineParameters ebmp; //esto lo inicializo antes para poder elegir maquina
            if (machine == 0)
            {
                ebmp = new ExternalBeamMachineParameters("NovalisTX2", setting_names[3], 600, setting_names[4], null);//SRS ARC O STATIC SE PUEDE COLOCAR CUALQUIERA QUE ESTE EN LA LISTA.
            }
            else ebmp = new ExternalBeamMachineParameters("TrueBeamSN3169", setting_names[3], 600, setting_names[4], null);//SRS ARC O STATIC SE PUEDE COLOCAR CUALQUIERA QUE ESTE EN LA LISTA.// 0=nombre del course y 1=del plan, 2=field, 3=energia, 4= srsarc o arc 5=rapidplan

            for (int i = 1; i <= arc_number; i++)
            {
                if (i == 1)

                {
                    Beam VMAT1 = cureps.AddArcBeam(ebmp, new VRect<double>(-100, -100, 100, 100), setting_arc[0], setting_arc[1], setting_arc[2], GantryDirection.Clockwise, 0, isocenter);
                    VMAT1.Id = setting_names[2] + i.ToString() + "_CW";
                    Jaws_corrected(VMAT1, ptv_total);
                }
                else if (i == 2)
                {
                    Beam VMAT2 = cureps.AddArcBeam(ebmp, new VRect<double>(-100, -100, 100, 100), 360 - setting_arc[0], setting_arc[2], setting_arc[1], GantryDirection.CounterClockwise, 0, isocenter);
                    VMAT2.Id = setting_names[2] + i.ToString() + "_CCW";
                    Jaws_corrected(VMAT2, ptv_total);
                }
                else if (i == 3)
                {
                    Beam VMAT3 = cureps.AddArcBeam(ebmp, new VRect<double>(-100, -100, 100, 100), 360 - setting_arc[0], setting_arc[1], setting_arc[2], GantryDirection.Clockwise, 0, isocenter);
                    VMAT3.Id = setting_names[2] + i.ToString() + "_CW";
                    Jaws_corrected(VMAT3, ptv_total);
                }
                else if (i == 4)
                {
                    Beam VMAT4 = cureps.AddArcBeam(ebmp, new VRect<double>(-100, -100, 100, 100), setting_arc[0], setting_arc[2], setting_arc[1], GantryDirection.CounterClockwise, 0, isocenter);
                    VMAT4.Id = setting_names[2] + i.ToString() + "_CCW";
                    Jaws_corrected(VMAT4, ptv_total);
                }
            }
            progress.Value = 15;
            return cureps;
        }

        public void opti_cureps(ExternalPlanSetup cureps)
        {
            OptimizerResult optresult = cureps.OptimizeVMAT(new OptimizationOptionsVMAT(OptimizationIntermediateDoseOption.UseIntermediateDose, string.Empty));
            cureps.OptimizeVMAT();
            cureps.CalculateDose();
            //cureps.PlanNormalizationValue = 100.2f;//esta normalizacion es la isododis de normalizaczacion no lo que colocamos en %tratamiento
        }

        ////ANADIR POR PLAN
        public void Optimization_dose(ExternalPlanSetup cureps, string[] rapidplan, List<string[]> PTVs_names, Double RxDose, int select = 0)
        {
            Dictionary<string, DoseValue> DVH_dose = new Dictionary<string, DoseValue>();
            Dictionary<string, string> DVH_struct = new Dictionary<string, string>();
            bool is_rapidplan = true;
            progress.Value = 20;
            try
            {
                if (rapidplan[select].Contains("PROSTATA"))
                {
                    SetDictionaries_Prost(out DVH_struct, out DVH_dose, cureps, PTVs_names, RxDose, is_rapidplan, select);//aqu queda los diccionarios de rapidplan
                    cureps.CalculateDVHEstimates(rapidplan[select], DVH_dose, DVH_struct);//ID DEL MODELO // DOSIS /MATCH STRUCTURA
                    opti_cureps(cureps);//calcula lo elemenal y la dosis
                }
                else if (rapidplan[select].Contains("RECTO"))
                {
                    SetDictionaries_Rect(out DVH_struct, out DVH_dose, cureps, PTVs_names, RxDose, is_rapidplan, select);//aqu queda los diccionarios de rapidplan
                    cureps.CalculateDVHEstimates(rapidplan[select], DVH_dose, DVH_struct);//ID DEL MODELO // DOSIS /MATCH STRUCTURA
                    opti_cureps(cureps);//calcula lo elemenal y la dosis
                }
            }
            catch (Exception e)
            {
                is_rapidplan = false;
                //seleccionar caso de estructura
                if (rapidplan[select].Contains("PROSTATA"))
                {
                    SetDictionaries_Prost(out DVH_struct, out DVH_dose, cureps, PTVs_names, RxDose, is_rapidplan, select);//aqu queda los diccionarios de rapidplan
                    progress.Value = 50;
                    opti_cureps(cureps);//calcula lo elemenal y la dosis
                }
                else if (rapidplan[select].Contains("RECTO"))
                {
                    SetDictionaries_Rect(out DVH_struct, out DVH_dose, cureps, PTVs_names, RxDose, is_rapidplan, select);//aqu queda los diccionarios de rapidplan
                    progress.Value = 50;
                    opti_cureps(cureps);//calcula lo elemenal y la dosis
                }
                /*else if (rapidplan.Contains("RECTO"))
                                    {
                                        is_rapidplan = false;
                                        SetDictionaries_rectum(out DVH_struct, out DVH_dose, cureps, PTVs_names, RxDose, is_rapidplan);//aqu queda los diccionarios de rapidplan
                                        opti_cureps(cureps);//calcula lo elemenal y la dosis
                                    }*/
                progress.Value = 95;
            }
        }

        public void Normalization(ExternalPlanSetup cureps, Structure S_norm, Double RxDose, int NFractions, int volume_porcent)
        {
            DoseValue normalization = cureps.GetDoseAtVolume(S_norm, volume_porcent, VolumePresentation.Relative, DoseValuePresentation.Absolute);
            double normaliza = Convert.ToDouble(normalization.Dose) / RxDose;
            cureps.SetPrescription(NFractions, new DoseValue(RxDose / NFractions, "Gy"), normaliza);//prescription 0.99=99 %tratamiento sip
            progress.Value = 100;
        }
        ///////Comienza lo personalizado para cada plan:
        //////PROSTATA
        public ExternalPlanSetup SetDictionaries_Prost(out Dictionary<string, string> DVH_struct, out Dictionary<string, DoseValue> DVH_dose, ExternalPlanSetup cureps, List<string[]> PTVs_names, Double RxDose, bool is_rapidplan, int select = 0)
        {
            DVH_struct = new Dictionary<string, string>();
            DVH_dose = new Dictionary<string, DoseValue>();
            string[] PTV_ID17 = { "PTV-PRVs!" };
            string[] PTV_ID20 = { "PTV_High_3625" };
            string[] PTV_ID20_ = { "PTV_High_4000" };
            string[] PTV_ID21 = { "PTV_Low_2500" };
            //names
            string[] N_Urethra = { "Urethra", "Uretra", "uretra" };
            string[] N_Trigone = { "Trigone", "trigono", "Trigono" };
            string[] N_Bladder = { "Bladder", "Vejiga", "vejiga", "Vejiga1" };
            string[] N_Rectum = { "Rectum", "recto", "rectum" };
            string[] N_Body = { "Body", "Outer Contour", "body" };
            //cambiar nombres
            string[] N_HJL = { "FemoralJoint_L", "Hip Joint, Left", "Hip Joint Left", "CFI", "CFi" };//hip joint left
            string[] N_HJR = { "FemoralJoint_R", "Hip Joint, Right", "Hip Joint Right", "CFD" };
            string[] N_Penile = { "PenileBulb", "Penile Bulb", "Pene B", "penile bulb", "B Pene", "Bulbo" };
            //oar
            string[] PRV_Rectum = { "Rectum_PRV05" };
            string[] Rect_ant = { "Rectum_A" };
            string[] Rect_post = { "Rectum_P" };
            //diccionario de estructuras para aparear
            string[] N_Colon = { "Colon", "colon", "sigma", "Grueso" };
            string[] N_Bowel = { "Bowel", "bowels", "intestinos", "Intestino", "intestino", "Delgado" };
            if (is_rapidplan)
            {
                foreach (string[] x in PTVs_names)//anade ptv con dose y estructure
                {
                    if (x != PTV_ID21) DVH_dose.Add(x.FirstOrDefault(y => cureps.StructureSet.Structures.Any(s => s.Id == y)), new DoseValue(RxDose, "Gy"));//COLOCAR NOMBRE DE LA ESTRUCTURA
                    else DVH_dose.Add(x.FirstOrDefault(y => cureps.StructureSet.Structures.Any(s => s.Id == y)), new DoseValue(25, "Gy"));//esto para colocar dosis de 25gy a los ganglios
                    DVH_struct.Add(x.FirstOrDefault(y => cureps.StructureSet.Structures.Any(s => s.Id == y)), x[0]);

                }
                DVH_struct.Add(N_Bladder.FirstOrDefault(x => cureps.StructureSet.Structures.Any(s => s.Id == x)), N_Bladder[0]);
                DVH_struct.Add(N_HJL.FirstOrDefault(x => cureps.StructureSet.Structures.Any(s => s.Id == x)), N_HJL[0]);
                DVH_struct.Add(N_HJR.FirstOrDefault(x => cureps.StructureSet.Structures.Any(s => s.Id == x)), N_HJR[0]);
                DVH_struct.Add(N_Rectum.FirstOrDefault(x => cureps.StructureSet.Structures.Any(s => s.Id == x)), N_Rectum[0]);
                DVH_struct.Add(Rect_ant.FirstOrDefault(x => cureps.StructureSet.Structures.Any(s => s.Id == x)), Rect_ant[0]);
                DVH_struct.Add(Rect_post.FirstOrDefault(x => cureps.StructureSet.Structures.Any(s => s.Id == x)), Rect_post[0]);
                DVH_struct.Add(PRV_Rectum.FirstOrDefault(x => cureps.StructureSet.Structures.Any(s => s.Id == x)), PRV_Rectum[0]);
                DVH_struct.Add(N_Trigone.FirstOrDefault(x => cureps.StructureSet.Structures.Any(s => s.Id == x)), N_Trigone[0]);
                DVH_struct.Add(N_Urethra.FirstOrDefault(x => cureps.StructureSet.Structures.Any(s => s.Id == x)), N_Urethra[0]);
                if (cureps.StructureSet.Structures.FirstOrDefault(x => N_Colon.Any(s => s == x.Id)) != null)
                {
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => N_Colon.Any(s => s == x.Id)), OptimizationObjectiveOperator.Upper, new DoseValue(34.5, "Gy"), 0, 140);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => N_Colon.Any(s => s == x.Id)), OptimizationObjectiveOperator.Upper, new DoseValue(24.5, "Gy"), 10, 100);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id.Contains("Colon_PRV")), OptimizationObjectiveOperator.Upper, new DoseValue(37, "Gy"), 10, 90);
                }
                if (cureps.StructureSet.Structures.FirstOrDefault(x => N_Bowel.Any(s => s == x.Id)) != null)
                {
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => N_Bowel.Any(s => s == x.Id)), OptimizationObjectiveOperator.Upper, new DoseValue(24.5, "Gy"), 0, 190);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => N_Bowel.Any(s => s == x.Id)), OptimizationObjectiveOperator.Upper, new DoseValue(19.5, "Gy"), 10, 150);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id.Contains("Bowel_PRV")), OptimizationObjectiveOperator.Upper, new DoseValue(27, "Gy"), 10, 100);
                }
                progress.Value = 50;

                return cureps;
            }
            else
            {

                if (select == 0)
                {
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id == PTV_ID20[0]), OptimizationObjectiveOperator.Upper, new DoseValue(39, "Gy"), 0, 40);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id == PTV_ID20[0]), OptimizationObjectiveOperator.Lower, new DoseValue(35.2, "Gy"), 100, 120);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id == PTV_ID20[0]), OptimizationObjectiveOperator.Lower, new DoseValue(36.25, "Gy"), 98, 100);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id == PTV_ID17[0]), OptimizationObjectiveOperator.Upper, new DoseValue(39, "Gy"), 0, 40);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == PTV_ID17[0]), OptimizationObjectiveOperator.Lower, new DoseValue(36.25, "Gy"), 100, 120);
                    //oars
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Rectum[0]), OptimizationObjectiveOperator.Upper, new DoseValue(18, "Gy"), 40, 60);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Rectum[0]), OptimizationObjectiveOperator.Upper, new DoseValue(27, "Gy"), 20, 60);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Rectum[0]), OptimizationObjectiveOperator.Upper, new DoseValue(29, "Gy"), 15, 60);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Rectum[0]), OptimizationObjectiveOperator.Upper, new DoseValue(32, "Gy"), 5, 40);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == Rect_ant[0]), OptimizationObjectiveOperator.Upper, new DoseValue(36, "Gy"), 1, 100);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == Rect_post[0]), OptimizationObjectiveOperator.Upper, new DoseValue(16, "Gy"), 1, 80);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == PRV_Rectum[0]), OptimizationObjectiveOperator.Upper, new DoseValue(36.25, "Gy"), 1, 100);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Bladder[0]), OptimizationObjectiveOperator.Upper, new DoseValue(18, "Gy"), 30, 80);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Bladder[0]), OptimizationObjectiveOperator.Upper, new DoseValue(36, "Gy"), 5, 80);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Bladder[0]), OptimizationObjectiveOperator.Upper, new DoseValue(37, "Gy"), 2, 40);
                    //cureps.OptimizationSetup.AddEUDObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Bladder[0]), OptimizationObjectiveOperator.Upper, new DoseValue(15, "Gy"), 1, 40);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_HJL[0]), OptimizationObjectiveOperator.Upper, new DoseValue(15, "Gy"), 5, 40);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_HJR[0]), OptimizationObjectiveOperator.Upper, new DoseValue(15, "Gy"), 5, 40);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Urethra[0]), OptimizationObjectiveOperator.Upper, new DoseValue(38, "Gy"), 0, 80);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Trigone[0]), OptimizationObjectiveOperator.Upper, new DoseValue(38, "Gy"), 0, 80);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Penile[0]), OptimizationObjectiveOperator.Upper, new DoseValue(20, "Gy"), 30, 40);
                    cureps.OptimizationSetup.AddAutomaticNormalTissueObjective(100.0f); //anade normal tissio automatico
                }
                else if (select == 1)
                {
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id == PTV_ID20[0]), OptimizationObjectiveOperator.Upper, new DoseValue(39, "Gy"), 0, 40);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id == PTV_ID20[0]), OptimizationObjectiveOperator.Lower, new DoseValue(35.2, "Gy"), 100, 120);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id == PTV_ID20[0]), OptimizationObjectiveOperator.Lower, new DoseValue(36.25, "Gy"), 98, 100);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id == PTV_ID17[0]), OptimizationObjectiveOperator.Upper, new DoseValue(39, "Gy"), 0, 40);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == PTV_ID17[0]), OptimizationObjectiveOperator.Lower, new DoseValue(36.25, "Gy"), 100, 120);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id == PTV_ID21[0]), OptimizationObjectiveOperator.Upper, new DoseValue(36.25, "Gy"), 0, 60);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id == PTV_ID21[0]), OptimizationObjectiveOperator.Upper, new DoseValue(28, "Gy"), 8, 50);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == PTV_ID21[0]), OptimizationObjectiveOperator.Lower, new DoseValue(25, "Gy"), 100, 110);
                    //oars
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Rectum[0]), OptimizationObjectiveOperator.Upper, new DoseValue(18, "Gy"), 40, 60);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Rectum[0]), OptimizationObjectiveOperator.Upper, new DoseValue(27, "Gy"), 20, 60);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Rectum[0]), OptimizationObjectiveOperator.Upper, new DoseValue(36, "Gy"), 5, 40);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == Rect_ant[0]), OptimizationObjectiveOperator.Upper, new DoseValue(36.25, "Gy"), 1, 100);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == Rect_post[0]), OptimizationObjectiveOperator.Upper, new DoseValue(16, "Gy"), 1, 80);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == PRV_Rectum[0]), OptimizationObjectiveOperator.Upper, new DoseValue(36.25, "Gy"), 1, 100);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Bladder[0]), OptimizationObjectiveOperator.Upper, new DoseValue(18, "Gy"), 30, 80);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Bladder[0]), OptimizationObjectiveOperator.Upper, new DoseValue(36, "Gy"), 5, 80);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Bladder[0]), OptimizationObjectiveOperator.Upper, new DoseValue(37, "Gy"), 2, 40);
                    //cureps.OptimizationSetup.AddEUDObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Bladder[0]), OptimizationObjectiveOperator.Upper, new DoseValue(15, "Gy"), 1, 40);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_HJL[0]), OptimizationObjectiveOperator.Upper, new DoseValue(15, "Gy"), 5, 40);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_HJR[0]), OptimizationObjectiveOperator.Upper, new DoseValue(15, "Gy"), 5, 40);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Urethra[0]), OptimizationObjectiveOperator.Upper, new DoseValue(38, "Gy"), 0, 80);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Trigone[0]), OptimizationObjectiveOperator.Upper, new DoseValue(38, "Gy"), 0, 80);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Penile[0]), OptimizationObjectiveOperator.Upper, new DoseValue(20, "Gy"), 30, 40);
                    if (cureps.StructureSet.Structures.FirstOrDefault(x => N_Colon.Any(s => s == x.Id)) != null)
                    {
                        cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => N_Colon.Any(r => r == y.Id)), OptimizationObjectiveOperator.Upper, new DoseValue(34.5, "Gy"), 0, 140);
                        cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => N_Colon.Any(r => r == y.Id)), OptimizationObjectiveOperator.Upper, new DoseValue(24.5, "Gy"), 10, 100);
                        cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id.Contains("Colon_PRV")), OptimizationObjectiveOperator.Upper, new DoseValue(37, "Gy"), 10, 90);
                    }
                    if (cureps.StructureSet.Structures.FirstOrDefault(x => N_Bowel.Any(s => s == x.Id)) != null)
                    {
                        cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => N_Bowel.Any(s => s == x.Id)), OptimizationObjectiveOperator.Upper, new DoseValue(24.5, "Gy"), 0, 180);
                        cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => N_Bowel.Any(s => s == x.Id)), OptimizationObjectiveOperator.Upper, new DoseValue(19.5, "Gy"), 10, 150);
                        cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id.Contains("Bowel_PRV")), OptimizationObjectiveOperator.Upper, new DoseValue(27, "Gy"), 10, 100);
                    }
                    cureps.OptimizationSetup.AddAutomaticNormalTissueObjective(100.0f); //anade normal tissio automatico
                }
                else if (select == 2)
                {
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id == PTV_ID20_[0]), OptimizationObjectiveOperator.Upper, new DoseValue(43, "Gy"), 0, 40);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id == PTV_ID20_[0]), OptimizationObjectiveOperator.Lower, new DoseValue(38, "Gy"), 100, 120);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id == PTV_ID20_[0]), OptimizationObjectiveOperator.Lower, new DoseValue(40, "Gy"), 98, 100);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id == PTV_ID17[0]), OptimizationObjectiveOperator.Upper, new DoseValue(43, "Gy"), 0, 40);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == PTV_ID17[0]), OptimizationObjectiveOperator.Lower, new DoseValue(40, "Gy"), 100, 120);
                    //oars
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Rectum[0]), OptimizationObjectiveOperator.Upper, new DoseValue(18, "Gy"), 40, 60);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Rectum[0]), OptimizationObjectiveOperator.Upper, new DoseValue(27, "Gy"), 20, 60);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Rectum[0]), OptimizationObjectiveOperator.Upper, new DoseValue(36, "Gy"), 5, 40);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Rectum[0]), OptimizationObjectiveOperator.Upper, new DoseValue(40, "Gy"), 3, 40);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == Rect_ant[0]), OptimizationObjectiveOperator.Upper, new DoseValue(38, "Gy"), 1, 100);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == Rect_post[0]), OptimizationObjectiveOperator.Upper, new DoseValue(16, "Gy"), 1, 80);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == PRV_Rectum[0]), OptimizationObjectiveOperator.Upper, new DoseValue(40, "Gy"), 1, 100);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Bladder[0]), OptimizationObjectiveOperator.Upper, new DoseValue(18, "Gy"), 30, 80);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Bladder[0]), OptimizationObjectiveOperator.Upper, new DoseValue(40, "Gy"), 5, 80);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Bladder[0]), OptimizationObjectiveOperator.Upper, new DoseValue(41, "Gy"), 2, 40);
                    //cureps.OptimizationSetup.AddEUDObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Bladder[0]), OptimizationObjectiveOperator.Upper, new DoseValue(15, "Gy"), 1, 40);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_HJL[0]), OptimizationObjectiveOperator.Upper, new DoseValue(15, "Gy"), 5, 40);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_HJR[0]), OptimizationObjectiveOperator.Upper, new DoseValue(15, "Gy"), 5, 40);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Urethra[0]), OptimizationObjectiveOperator.Upper, new DoseValue(42, "Gy"), 0, 80);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Trigone[0]), OptimizationObjectiveOperator.Upper, new DoseValue(42, "Gy"), 0, 80);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Penile[0]), OptimizationObjectiveOperator.Upper, new DoseValue(20, "Gy"), 30, 40);
                    if (cureps.StructureSet.Structures.FirstOrDefault(x => N_Colon.Any(s => s == x.Id)) != null)
                    {
                        cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => N_Colon.Any(r => r == y.Id)), OptimizationObjectiveOperator.Upper, new DoseValue(34.5, "Gy"), 0, 140);
                        cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => N_Colon.Any(r => r == y.Id)), OptimizationObjectiveOperator.Upper, new DoseValue(24.5, "Gy"), 10, 100);
                        cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id.Contains("Colon_PRV")), OptimizationObjectiveOperator.Upper, new DoseValue(37, "Gy"), 10, 90);
                    }
                    if (cureps.StructureSet.Structures.FirstOrDefault(x => N_Bowel.Any(s => s == x.Id)) != null)
                    {
                        cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => N_Bowel.Any(s => s == x.Id)), OptimizationObjectiveOperator.Upper, new DoseValue(24.5, "Gy"), 0, 180);
                        cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => N_Bowel.Any(s => s == x.Id)), OptimizationObjectiveOperator.Upper, new DoseValue(19.5, "Gy"), 10, 150);
                        cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id.Contains("Bowel_PRV")), OptimizationObjectiveOperator.Upper, new DoseValue(27, "Gy"), 10, 100);
                    }
                    cureps.OptimizationSetup.AddAutomaticNormalTissueObjective(100.0f); //anade normal tissio automatico
                }
                else if (select == 3)
                {
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id == PTV_ID20_[0]), OptimizationObjectiveOperator.Upper, new DoseValue(43, "Gy"), 0, 50);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id == PTV_ID20_[0]), OptimizationObjectiveOperator.Lower, new DoseValue(38, "Gy"), 100, 120);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id == PTV_ID20_[0]), OptimizationObjectiveOperator.Lower, new DoseValue(40, "Gy"), 98, 100);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id == PTV_ID17[0]), OptimizationObjectiveOperator.Upper, new DoseValue(43, "Gy"), 0, 40);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == PTV_ID17[0]), OptimizationObjectiveOperator.Lower, new DoseValue(40, "Gy"), 100, 120);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id == PTV_ID21[0]), OptimizationObjectiveOperator.Upper, new DoseValue(38, "Gy"), 0, 60);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id == PTV_ID21[0]), OptimizationObjectiveOperator.Upper, new DoseValue(28, "Gy"), 8, 50);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == PTV_ID21[0]), OptimizationObjectiveOperator.Lower, new DoseValue(25, "Gy"), 100, 110);
                    //oars
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Rectum[0]), OptimizationObjectiveOperator.Upper, new DoseValue(18, "Gy"), 40, 60);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Rectum[0]), OptimizationObjectiveOperator.Upper, new DoseValue(27, "Gy"), 20, 60);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == Rect_ant[0]), OptimizationObjectiveOperator.Upper, new DoseValue(38, "Gy"), 1, 100);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == Rect_post[0]), OptimizationObjectiveOperator.Upper, new DoseValue(16, "Gy"), 1, 80);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == PRV_Rectum[0]), OptimizationObjectiveOperator.Upper, new DoseValue(40, "Gy"), 1, 100);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Bladder[0]), OptimizationObjectiveOperator.Upper, new DoseValue(18, "Gy"), 30, 80);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Bladder[0]), OptimizationObjectiveOperator.Upper, new DoseValue(40, "Gy"), 5, 80);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Bladder[0]), OptimizationObjectiveOperator.Upper, new DoseValue(41, "Gy"), 2, 40);
                    //cureps.OptimizationSetup.AddEUDObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Bladder[0]), OptimizationObjectiveOperator.Upper, new DoseValue(15, "Gy"), 1, 40);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_HJL[0]), OptimizationObjectiveOperator.Upper, new DoseValue(15, "Gy"), 5, 40);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_HJR[0]), OptimizationObjectiveOperator.Upper, new DoseValue(15, "Gy"), 5, 40);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Urethra[0]), OptimizationObjectiveOperator.Upper, new DoseValue(42, "Gy"), 0, 80);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Trigone[0]), OptimizationObjectiveOperator.Upper, new DoseValue(42, "Gy"), 0, 80);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Penile[0]), OptimizationObjectiveOperator.Upper, new DoseValue(20, "Gy"), 30, 40);
                    if (cureps.StructureSet.Structures.FirstOrDefault(x => N_Colon.Any(s => s == x.Id)) != null)
                    {
                        cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => N_Colon.Any(r => r == y.Id)), OptimizationObjectiveOperator.Upper, new DoseValue(34.5, "Gy"), 0, 140);
                        cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => N_Colon.Any(r => r == y.Id)), OptimizationObjectiveOperator.Upper, new DoseValue(24.5, "Gy"), 10, 100);
                        cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id.Contains("Colon_PRV")), OptimizationObjectiveOperator.Upper, new DoseValue(37, "Gy"), 10, 90);
                    }
                    if (cureps.StructureSet.Structures.FirstOrDefault(x => N_Bowel.Any(s => s == x.Id)) != null)
                    {
                        cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => N_Bowel.Any(s => s == x.Id)), OptimizationObjectiveOperator.Upper, new DoseValue(24.5, "Gy"), 0, 180);
                        cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => N_Bowel.Any(s => s == x.Id)), OptimizationObjectiveOperator.Upper, new DoseValue(19.5, "Gy"), 10, 150);
                        cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id.Contains("Bowel_PRV")), OptimizationObjectiveOperator.Upper, new DoseValue(27, "Gy"), 10, 100);
                    }
                    cureps.OptimizationSetup.AddAutomaticNormalTissueObjective(100.0f); //anade normal tissio automatico
                }
                progress.Value = 45;

                return cureps;
            }
        }

        public ExternalPlanSetup SetDictionaries_Rect(out Dictionary<string, string> DVH_struct, out Dictionary<string, DoseValue> DVH_dose, ExternalPlanSetup cureps, List<string[]> PTVs_names, Double RxDose, bool is_rapidplan, int select = 0)
        {
            DVH_struct = new Dictionary<string, string>();
            DVH_dose = new Dictionary<string, DoseValue>();

            string[] N_Colon = { "Colon", "colon", "sigma", "Grueso" };
            string[] N_Bladder = { "Bladder", "Vejiga", "vejiga", "Vejiga1" };
            string[] N_Bowel = { "Bowel", "Intestino", "intestino", "intestinos", "Delgado" };
            string[] N_GM = { "Gluteus_Maximus", "Gluteo Mayor", "gluteos", "Gluteos", "Glute o Mayor", "Gluteo mayor", "gluteo Mayor" };
            string[] N_GS = { "Gluteal_Skin", "Piel Glutea", "pielG", "Piel glutea", "piel", "Piel", "piel glutea", "Piel_Glutea", "piel_glutea" };
            string[] N_HJL = { "FemoralJoint_L", "Hip Joint, Left", "Hip Joint Left", "CFI", "CFi" };//hip joint left
            string[] N_HJR = { "FemoralJoint_R", "Hip Joint, Right", "Hip Joint Right", "CFD", "CFd" };
            string[] PTV_48 = { "zPTV_Low_4800!", "zPTV_Mid_4800!", "PTV_48Gy", "PTV 48 GY", "PTV 48gy", "PTV 48 gy", "PTV_48 Gy", "PTV 48 Gy", "PTV 48Gy" };
            string[] PTV_54 = { "zPTV_High_5400!", "PTV_54Gy", "PTV 54 GY", "PTV 54gy", "PTV 54 gy", "PTV_SIB 54Gy", "PTV 54 Gy", "PTV 54Gy", "PTV_54 Gy" };
            string[] N_Bowel_PRV = { "Bowel_PRV05", "PRV INTESTINO", "PRV intest", "PRV Intestino", "PRV intestino", "PRV_Intestino", "PRV_intestino" };
            string[] N_Colon_PRV = { "Colon_PRV05", "PRV colon", "PRV COLON", "PRV_COLON", "PRV Colon", "PRV_colon", "PRV_Colon" };

            //diccionario de estructuras para aparear

            if (is_rapidplan)
            {
                foreach (string[] x in PTVs_names)//anade ptv con dose y estructure
                {
                    DVH_dose.Add(x.FirstOrDefault(y => cureps.StructureSet.Structures.Any(s => s.Id == y)), new DoseValue(25, "Gy"));//esto para colocar dosis de 25gy a los ganglios
                    DVH_struct.Add(x.FirstOrDefault(y => cureps.StructureSet.Structures.Any(s => s.Id == y)), x[0]);
                }
                DVH_struct.Add(N_Bladder.FirstOrDefault(x => cureps.StructureSet.Structures.Any(s => s.Id == x)), N_Bladder[0]);
                DVH_struct.Add(N_Bowel.FirstOrDefault(x => cureps.StructureSet.Structures.Any(s => s.Id == x)), N_Bowel[0]);
                DVH_struct.Add(N_Bowel_PRV.FirstOrDefault(x => cureps.StructureSet.Structures.Any(s => s.Id == x)), N_Bowel_PRV[0]);
                DVH_struct.Add(N_Colon.FirstOrDefault(x => cureps.StructureSet.Structures.Any(s => s.Id == x)), N_Colon[0]);
                DVH_struct.Add(N_Colon_PRV.FirstOrDefault(x => cureps.StructureSet.Structures.Any(s => s.Id == x)), N_Colon_PRV[0]);
                DVH_struct.Add(N_HJL.FirstOrDefault(x => cureps.StructureSet.Structures.Any(s => s.Id == x)), N_HJL[0]);
                DVH_struct.Add(N_HJR.FirstOrDefault(x => cureps.StructureSet.Structures.Any(s => s.Id == x)), N_HJR[0]);
                DVH_struct.Add(N_GS.FirstOrDefault(x => cureps.StructureSet.Structures.Any(s => s.Id == x)), N_GS[0]);
                DVH_struct.Add(N_GM.FirstOrDefault(x => cureps.StructureSet.Structures.Any(s => s.Id == x)), N_GM[0]);
                progress.Value = 50;

                return cureps;
            }
            else
            {

                if (select == 0)
                {
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id == PTV_48[0]), OptimizationObjectiveOperator.Upper, new DoseValue(54, "Gy"), 0, 90);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id == PTV_48[0]), OptimizationObjectiveOperator.Upper, new DoseValue(52, "Gy"), 2, 100);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id == PTV_48[0]), OptimizationObjectiveOperator.Lower, new DoseValue(48, "Gy"), 98, 110);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id == PTV_48[0]), OptimizationObjectiveOperator.Lower, new DoseValue(43.5, "Gy"), 100, 120);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id == PTV_54[0]), OptimizationObjectiveOperator.Upper, new DoseValue(57, "Gy"), 0, 90);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id == PTV_54[0]), OptimizationObjectiveOperator.Upper, new DoseValue(53, "Gy"), 0, 120);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == PTV_54[0]), OptimizationObjectiveOperator.Lower, new DoseValue(54, "Gy"), 98, 120);
                    //oars

                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Bladder[0]), OptimizationObjectiveOperator.Upper, new DoseValue(45, "Gy"), 45, 60);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Bladder[0]), OptimizationObjectiveOperator.Upper, new DoseValue(35, "Gy"), 65, 50);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Bowel[0]), OptimizationObjectiveOperator.Upper, new DoseValue(43, "Gy"), 0, 300);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Bowel_PRV[0]), OptimizationObjectiveOperator.Upper, new DoseValue(0.1, "Gy"), 45.5, 100);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Colon[0]), OptimizationObjectiveOperator.Upper, new DoseValue(49, "Gy"), 0, 150);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_Colon_PRV[0]), OptimizationObjectiveOperator.Upper, new DoseValue(51.5, "Gy"), 0.1, 100);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_HJL[0]), OptimizationObjectiveOperator.Upper, new DoseValue(50, "Gy"), 10, 40);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_HJR[0]), OptimizationObjectiveOperator.Upper, new DoseValue(50, "Gy"), 10, 40);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_GS[0]), OptimizationObjectiveOperator.Upper, new DoseValue(50, "Gy"), 1, 40);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(x => x.Id == N_GM[0]), OptimizationObjectiveOperator.Upper, new DoseValue(50, "Gy"), 1, 40);


                    cureps.OptimizationSetup.AddAutomaticNormalTissueObjective(100.0f); //anade normal tissio automatico
                }
                progress.Value = 45;

                return cureps;
            }
        }
        public void Opti_Bow_Col(ExternalPlanSetup cureps, string[] rapidplan, List<string[]> PTVs_names, Double RxDose, List<Structure> st_oar, int select = 0)
        {
            Dictionary<string, DoseValue> DVH_dose = new Dictionary<string, DoseValue>();
            Dictionary<string, string> DVH_struct = new Dictionary<string, string>();
            bool is_rapidplan = true;
            SetDictionaries_Prost(out DVH_struct, out DVH_dose, cureps, PTVs_names, RxDose, is_rapidplan);//aqu queda los diccionarios de rapidplan
            cureps.OptimizationSetup.AddAutomaticNormalTissueObjective(100.0f); //anade normal tissio automatico
            int Colon_priority = 150;
            int Bowel_priority = 190;
            progress.Value = 20;
            try
            {
                var run_DVH = cureps.CalculateDVHEstimates(rapidplan[select], DVH_dose, DVH_struct);//ID DEL MODELO // DOSIS /MATCH STRUCTURA
                                                                                                    //                    System.Windows.MessageBox.Show(run_DVH.Success.ToString());
                if (st_oar.Any(x => x.Id == "Colon"))
                {
                    cureps.OptimizationSetup.AddPointObjective(st_oar.FirstOrDefault(x => x.Id == "Colon"), OptimizationObjectiveOperator.Upper, new DoseValue(34.5, "Gy"), 0, Colon_priority);
                    cureps.OptimizationSetup.AddPointObjective(st_oar.FirstOrDefault(x => x.Id == "Colon"), OptimizationObjectiveOperator.Upper, new DoseValue(24.5, "Gy"), 10, Colon_priority - 60);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id.Contains("Colon_PRV")), OptimizationObjectiveOperator.Upper, new DoseValue(37, "Gy"), 10, Colon_priority - 110);
                }

                if (st_oar.Any(x => x.Id == "Bowel"))
                {
                    cureps.OptimizationSetup.AddPointObjective(st_oar.FirstOrDefault(x => x.Id == "Bowel"), OptimizationObjectiveOperator.Upper, new DoseValue(24.5, "Gy"), 0, Bowel_priority);
                    cureps.OptimizationSetup.AddPointObjective(st_oar.FirstOrDefault(x => x.Id == "Bowel"), OptimizationObjectiveOperator.Upper, new DoseValue(19.5, "Gy"), 10, Bowel_priority - 40);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id.Contains("Bowel_PRV")), OptimizationObjectiveOperator.Upper, new DoseValue(27, "Gy"), 10, Bowel_priority - 110);
                }  
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("no agarro rapidplan", "RP");
                /*List<ProtocolPhasePrescription> prescriptions= new List<ProtocolPhasePrescription>();
                List<ProtocolPhaseMeasure> measures= new List<ProtocolPhaseMeasure>();
                cureps.GetProtocolPrescriptionsAndMeasures(ref prescriptions, ref measures);
                foreach (var prescription in prescriptions)
                {
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id == prescription.StructureId), prescription. OptimizationObjectiveOperator.Upper, prescription.TargetTotalDose new DoseValue(41, "Gy"), 0, 90);
                }*/
                is_rapidplan = false;
                if (select == 0)
                {
                    SetDictionaries_Prost(out DVH_struct, out DVH_dose, cureps, PTVs_names, RxDose, is_rapidplan, select);//aqu queda los diccionarios de rapidplan

                }
                else if (select == 1)
                {
                    SetDictionaries_Prost(out DVH_struct, out DVH_dose, cureps, PTVs_names, RxDose, is_rapidplan, select);//aqu queda los diccionarios de rapidplan
                }
                else if (select == 2)
                {
                    SetDictionaries_Prost(out DVH_struct, out DVH_dose, cureps, PTVs_names, RxDose, is_rapidplan, select);//aqu queda los diccionarios de rapidplan
                }
                else if (select == 3)
                {
                    SetDictionaries_Prost(out DVH_struct, out DVH_dose, cureps, PTVs_names, RxDose, is_rapidplan, select);//aqu queda los diccionarios de rapidplan
                }
                if (st_oar.Any(x => x.Id == "Colon"))
                {
                    cureps.OptimizationSetup.AddPointObjective(st_oar.FirstOrDefault(x => x.Id == "Colon"), OptimizationObjectiveOperator.Upper, new DoseValue(34.5, "Gy"), 0, Colon_priority);
                    cureps.OptimizationSetup.AddPointObjective(st_oar.FirstOrDefault(x => x.Id == "Colon"), OptimizationObjectiveOperator.Upper, new DoseValue(24.5, "Gy"), 10, Colon_priority - 60);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id.Contains("Colon_PRV")), OptimizationObjectiveOperator.Upper, new DoseValue(37, "Gy"), 10, Colon_priority - 110);
                }

                if (st_oar.Any(x => x.Id == "Bowel"))
                {
                    cureps.OptimizationSetup.AddPointObjective(st_oar.FirstOrDefault(x => x.Id == "Bowel"), OptimizationObjectiveOperator.Upper, new DoseValue(24.5, "Gy"), 0, Bowel_priority);
                    cureps.OptimizationSetup.AddPointObjective(st_oar.FirstOrDefault(x => x.Id == "Bowel"), OptimizationObjectiveOperator.Upper, new DoseValue(19.5, "Gy"), 10, Bowel_priority - 40);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id.Contains("Bowel_PRV")), OptimizationObjectiveOperator.Upper, new DoseValue(27, "Gy"), 10, Bowel_priority - 110);
                }
                progress.Value = 45;
            }
            progress.Value = 50;
            opti_cureps(cureps);//calcula lo elemental y la dosis    
            IEnumerable < OptimizationObjective > objetives = Enumerable.Empty<OptimizationObjective>();
            objetives =   cureps.OptimizationSetup.Objectives;//trucho inizializar y luego asignar
            
            //////////////////////////////////////////////////////////////////////////////
            foreach (OptimizationObjective s in objetives) System.Windows.MessageBox.Show("\n"+s.StructureId);

            if (st_oar.Any(x => x.Id == "Bowel"))
            {
                DoseValue colonDose = cureps.GetDoseAtVolume(st_oar.FirstOrDefault(x => x.Id == "Colon"), 0, VolumePresentation.AbsoluteCm3, DoseValuePresentation.Absolute);
                DoseValue bowelDose = cureps.GetDoseAtVolume(st_oar.FirstOrDefault(x => x.Id == "Bowel"), 0, VolumePresentation.AbsoluteCm3, DoseValuePresentation.Absolute);
                while (bowelDose.Dose > 25.2||Bowel_priority<=550)
                {
                    Bowel_priority = Bowel_priority + 40;
                    cureps.OptimizationSetup.RemoveObjective(objetives.FirstOrDefault(x => x.StructureId == "Bowel"));//quito el objetivo de bowel para que no haya mas
                    cureps.OptimizationSetup.AddPointObjective(st_oar.FirstOrDefault(x => x.Id == "Bowel"), OptimizationObjectiveOperator.Upper, new DoseValue(24.5, "Gy"), 0, Bowel_priority);
                    cureps.OptimizationSetup.AddPointObjective(st_oar.FirstOrDefault(x => x.Id == "Bowel"), OptimizationObjectiveOperator.Upper, new DoseValue(19.5, "Gy"), 10, Bowel_priority - 50);
                    cureps.OptimizationSetup.AddPointObjective(cureps.StructureSet.Structures.FirstOrDefault(y => y.Id.Contains("Bowel_PRV")), OptimizationObjectiveOperator.Upper, new DoseValue(27, "Gy"), 10, Bowel_priority - 110);
                    opti_cureps(cureps);//calcula lo elemenal y la dosis
                    bowelDose = cureps.GetDoseAtVolume(st_oar.FirstOrDefault(x => x.Id == "Bowel"), 0, VolumePresentation.AbsoluteCm3, DoseValuePresentation.Absolute);
                }
            }
            progress.Value = 95;
        }

        public void Two_Structures(Structure bowel, Structure colon, ExternalPlanSetup cureps, string[] rapidplan, List<string[]> PTVs_names, Double RxDose,int select,List<Structure> VIP_oar)
        {
            if (bowel == null && colon == null)//intestino
            {
                Optimization_dose(cureps, rapidplan, PTVs_names, RxDose, select);
            }
            else
            {
                if (bowel != null && colon != null)
                {
                    VIP_oar.Add(bowel); VIP_oar.Add(colon);
                    Opti_Bow_Col(cureps, rapidplan, PTVs_names, RxDose, VIP_oar, select);
                }
                else if (bowel != null && colon == null)
                {
                    VIP_oar.Add(bowel);
                    Opti_Bow_Col(cureps, rapidplan, PTVs_names, RxDose, VIP_oar, select);
                }
                else if (colon != null && bowel == null)
                {
                    VIP_oar.Add(colon);
                    Opti_Bow_Col(cureps, rapidplan, PTVs_names, RxDose, VIP_oar, select);
                }
            }
        }
        public void Plan_Prostate_5Fx(ScriptContext context /*, System.Windows.Window window, ScriptEnvironment environment*/)
        {
            Patient patient = context.Patient;
            StructureSet ss = context.StructureSet;
            PlanSetup ps = context.PlanSetup;

            if (patient == null || context.StructureSet == null)
            {
                System.Windows.MessageBox.Show("Please load a patient, 3D image, and structure set before running this script.", SCRIPT_NAME, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            //Que tipo de modelo se elige
            DialogResult result = System.Windows.Forms.MessageBox.Show("Is Dose prescription 36.25Gy?" + "\n" + "If Yes, dose prescription is 36.25Gy" + "\n" + "If No, dose prescription is 40Gy" + "\n" + "If Cancel, exit application.", SCRIPT_NAME, MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Cancel) return;
            DialogResult result1 = System.Windows.Forms.MessageBox.Show("Have Lymph Nodes", SCRIPT_NAME, MessageBoxButtons.YesNo);
            ////////////////////////////////////////////////////////////////////////////
            //Structures esenciales para correr el plan
            string[] PTV_ID19 = { "PTV_Total!", "PTV_Total", "zPTV_Total!" };
            string[] PTV_ID17 = { "PTV-PRVs!" };
            string[] PTV_ID20 = { "PTV_High_3625" };
            string[] PTV_ID20_ = { "PTV_High_4000" };

            string[] PTV_ID21 = { "PTV_Low_2500" };
            //string[] PTV_ID22 = { "PTV_Mid_2750" };
            string[] N_Colon = { "Colon", "colon", "sigma", "Grueso" };
            string[] N_Bowel = { "Bowel", "bowels", "intestinos", "Intestino", "intestino", "Delgado" };
            Structure ptv_total = ss.Structures.FirstOrDefault(x => PTV_ID19.Any(s => s == x.Id));//ptv total
            Structure ptv_prvs = ss.Structures.FirstOrDefault(x => PTV_ID17.Any(s => s == x.Id));//ptv total
            Structure bowel = ss.Structures.FirstOrDefault(x => N_Bowel.Any(s => s == x.Id));//ptv total
            Structure colon = ss.Structures.FirstOrDefault(x => N_Colon.Any(s => s == x.Id));//ptv total
            progress.Value = 2;
            /////////////////////////////////////////////////////////////////////////Prescripcion siempre es de 5 fraction
            double RxDose = 36.25;//por defecto esta en 36.25
            int NFractions = 5;
            string[] setting_names = { "SBRT_Prostate", "SBRT_", "Field", "10X", "SRS ARC" };// 0=nombre del course y 1=del plan, 2=field, 3=energia, 4= srsarc o arc 5=rapidplan
            string[] rapidplan = { "PROSTATA 3625 - SIN FILTRO", "PROSTATA 3625/2500 - SIN FILTRO", "PROSTATA 4000 - SIN FILTRO", "PROSTATA 4000/2500 - SIN FILTRO" };
            Double[] setting_arc = { 10, 181, 179 };//por defecto esta en porstata simple 36.25
            Double[] shift = { 0, 0, -0.5 }; //desplazamiento del isocentro en este caso dice en longitudinal z se ba bajar con el signo dado por que en la funcion es suma
            /////////////////////////////////////////////////////////////////////////////
            //list de oars importantes
            List<Structure> VIP_oar = new List<Structure>();
            List<string[]> PTVs_names = new List<string[]>();//esto para pasar los nombres del ptvs que existen
                                                             /////////////////////////////////////////////////// Comienza la generacion de planes
            progress.Value = 4;
            if (result == DialogResult.Yes && result1 == DialogResult.No)
            {
                if (!ss.Structures.Any(x => x.Id == PTV_ID20[0]))
                {
                    System.Windows.MessageBox.Show(PTV_ID20[0] + " not found, script doesnt execute");
                    return;
                }
                PTVs_names.Add(PTV_ID20); PTVs_names.Add(PTV_ID17);
                setting_names[1] = setting_names[1] + "Prostata";//rectum0 esto coloca el nombre mas 

                ExternalPlanSetup cureps = settingPlan(0, patient, ss, RxDose, NFractions, ptv_total, setting_names, setting_arc,shift ,1);
                Two_Structures(bowel, colon, cureps, rapidplan, PTVs_names, RxDose, 0, VIP_oar);//0 importante
                Normalization(cureps, ptv_prvs, RxDose, NFractions, 98);//normaliza al valor de ptv-prvs98%
            }
            else if (result == DialogResult.Yes && result1 == DialogResult.Yes)
            {
                if (!ss.Structures.Any(x => x.Id == PTV_ID20[0]))
                {
                    System.Windows.MessageBox.Show(PTV_ID20[0] + " not found, script doesnt execute");
                    return;
                }
                setting_arc = new Double[] { 30, 181, 179 };
                PTVs_names.Add(PTV_ID20); PTVs_names.Add(PTV_ID17); PTVs_names.Add(PTV_ID21);//los nombres de los ptvs

                setting_names[1] = setting_names[1] + "Prost+GG";/////////
                ExternalPlanSetup cureps = settingPlan(0, patient, ss, RxDose, NFractions, ptv_total, setting_names, setting_arc, shift,2);
                Two_Structures(bowel, colon, cureps, rapidplan, PTVs_names, RxDose, 1, VIP_oar);
                Normalization(cureps, ptv_prvs, RxDose, NFractions, 98);//normaliza al valor de ptv-prvs98%
            }
            else if (result == DialogResult.No && result1 == DialogResult.No)
            {
                if (!ss.Structures.Any(x => x.Id == PTV_ID20_[0]))
                {
                    System.Windows.MessageBox.Show(PTV_ID20_[0] + " not found, script doesnt execute");
                    return;
                }
                RxDose = 40.0;//dosis prescrita
                PTVs_names.Add(PTV_ID20); PTVs_names.Add(PTV_ID17);

                setting_names[1] = setting_names[1] + "Prostata";//rectum0
                ExternalPlanSetup cureps = settingPlan(0, patient, ss, RxDose, NFractions, ptv_total, setting_names, setting_arc, shift, 1);
                Two_Structures(bowel, colon, cureps, rapidplan, PTVs_names, RxDose, 2, VIP_oar);
                Normalization(cureps, ptv_prvs, RxDose, NFractions, 98);//normaliza al valor de ptv-prvs98%             
            }
            else if (result == DialogResult.No && result1 == DialogResult.Yes)
            {
                if (!ss.Structures.Any(x => x.Id == PTV_ID20_[0]))
                {
                    System.Windows.MessageBox.Show(PTV_ID20_[0] + " not found, script doesnt execute");
                    return;
                }
                RxDose = 40.0;
                setting_arc = new Double[] { 30, 181, 179 };
                PTVs_names.Add(PTV_ID20_); PTVs_names.Add(PTV_ID17); PTVs_names.Add(PTV_ID21);

                setting_names[1] = setting_names[1] + "Prost+GG";//rectum0
                ExternalPlanSetup cureps = settingPlan(0, patient, ss, RxDose, NFractions, ptv_total, setting_names, setting_arc, shift,2);
                Two_Structures(bowel, colon, cureps, rapidplan, PTVs_names, RxDose, 3, VIP_oar);
                Normalization(cureps, ptv_prvs, RxDose, NFractions, 98);//normaliza al valor de ptv-prvs98%
            }
            //ps.AddReferencePoint(ptv_total, ps.Dose.DoseMax3DLocation, "Calculus", null);//coloca punto de referencia no se como hacer que sea 

        }


        public void Plan_Rectum_20Fx(ScriptContext context /*, System.Windows.Window window, ScriptEnvironment environment*/)
        {

            Patient patient = context.Patient;
            StructureSet ss = context.StructureSet;
            PlanSetup ps = context.PlanSetup;

            if (patient == null || context.StructureSet == null)
            {
                System.Windows.MessageBox.Show("Please load a patient, 3D image, and structure set before running this script.", SCRIPT_NAME, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            //Que tipo de modelo se elige
            DialogResult result = System.Windows.Forms.MessageBox.Show("OPtions:" + "\n" + "1.-If Yes, SIB of 52Gy" + "\n" + "2.-If No, SIB of 54Gy" + "\n" + "3.-If Cancel,EXIT", "Question", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Cancel) return;
            ////////////////////////////////////////////////////////////////////////////
            //Structures esenciales para correr el plan
            string[] PTV_T = { "zPTV_Total!", "PTV TOTAL", "PTV Total", "PTV_TOTAL", "PTV_total", "PTV_Total" };
            string[] PTV_48 = { "zPTV_Low_4800!", "zPTV_Mid_4800!", "PTV_48Gy", "PTV 48 GY", "PTV 48gy", "PTV 48 gy", "PTV_48 Gy", "PTV 48 Gy", "PTV 48Gy", "zPTV_Total" };
            string[] PTV_54 = { "zPTV_High_5400!", "PTV_54Gy", "PTV 54 GY", "PTV 54gy", "PTV 54 gy", "PTV_SIB 54Gy", "PTV 54 Gy", "PTV 54Gy", "PTV_54 Gy" };
            string[] GTV = { "GTV_SIB" };//estructura de normalizacion
            /////////////////////////////////////////////////////////////////////////Prescripcion siempre es de 5 fraction
            double RxDose = 54;//por defecto esta en 36.25
            int NFractions = 20;
            string[] setting_names = { "Rectum", "Rectum", "Field", "6X", "ARC" };// 0=nombre del course y 1=del plan, 2=field, 3=energia, 4= srsarc o arc 5=rapidplan
            string[] rapidplan = { "RECTO_5400/4800" };
            Double[] setting_arc = { 30, 181, 179 };//por
            Double[] shift = { 0,0,0};
            int n_total = 1;
            /////////////////////////////////////////////////////////////////////////////
            //list de oars importantes
            List<Structure> VIP_oar = new List<Structure>();
            List<string[]> PTVs_names = new List<string[]>();//esto para pasar los nombres del ptvs que existen
            /////////////////////////////////////////////////// Comienza la generacion de planes
            if (!ss.Structures.Any(x => x.Id == PTV_54[0]) || !ss.Structures.Any(x => x.Id == PTV_T[0]))
            {
                System.Windows.MessageBox.Show(PTV_54[0] + " or " + PTV_T[0] + " not found, script doesnt execute");
                return;
            }
            Structure ptv_total = ss.Structures.FirstOrDefault(x => PTV_T.Any(s => s == x.Id));//ptv total

            if (result == DialogResult.No)
            {
                Structure gtv_54 = ss.Structures.FirstOrDefault(x => GTV.Any(s => s == x.Id));//ptv total

                PTVs_names.Add(PTV_54); PTVs_names.Add(PTV_48);
                setting_names[1] = setting_names[1] + "20FX";//rectum0 esto coloca el nombre mas 
                ExternalPlanSetup cureps = settingPlan(0, patient, ss, RxDose, NFractions, ptv_total, setting_names, setting_arc, shift,1);//c es un shift que sirve en prostata y quizas en otro mas pero aqui no

                Optimization_dose(cureps, rapidplan, PTVs_names, RxDose, 0);
                Normalization(cureps, gtv_54, RxDose, NFractions, 95);//normaliza al valor de gtv95%
            }
            else if (result == DialogResult.Yes)
            {
                System.Windows.MessageBox.Show("Todavia no esta implementado, contactese con su desarrollador local");
            }
        }

        public void Plan_Mama(ScriptContext context /*, System.Windows.Window window, ScriptEnvironment environment*/)
        {
            Patient patient = context.Patient;
            //StructureSet ss = context.StructureSet;
            PlanSetup ps = context.PlanSetup;

            if (patient == null || context.StructureSet == null)
            {
                System.Windows.MessageBox.Show("Please load a patient, 3D image, and structure set before running this script.", SCRIPT_NAME, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            IEnumerable<StructureSet> sss = patient.StructureSets;
            string[] CT_name0 = { "CT_MODIFICADA", "modif", "MODIF", "CT_Modificada" };
            string[] CT_name1 = { "CT_ORIGINAL", "origin", "ORIGI", "CT_Original" };
            StructureSet CT_modificada = sss.FirstOrDefault(s => CT_name0.Any(x => s.Id.Contains(x)));
            StructureSet CT_Original = sss.FirstOrDefault(s => CT_name0.Any(x => s.Id.Contains(x)));
            if (CT_modificada == null || CT_Original == null)
            {
                System.Windows.Forms.MessageBox.Show("Falta la CT modificada u original, revise los nombres, el Script no se ejecutara");
                return;
            }

            //Que tipo de modelo se elige
            DialogResult tto = System.Windows.Forms.MessageBox.Show("Breast or Chest wall or Prosthesis?" + "\n" + "If Yes, the volume is Breast(Mama)." + "\n" + "If No, the volume is Chest wall(Pared)." + "\n" + "If Cancel, the volume is chestwall with expander(Expansor).", SCRIPT_NAME, MessageBoxButtons.YesNoCancel);
            DialogResult fraction = System.Windows.Forms.MessageBox.Show("Fraction: 16Fx or 20Fx?" + "\n" + "If Yes, the volume is 16Fx." + "\n" + "If No, the volume is 20Fx." + "\n" + "If Cancel, Stop Script", SCRIPT_NAME, MessageBoxButtons.YesNoCancel);
            if (fraction == DialogResult.Cancel) return;
            DialogResult nodes = System.Windows.Forms.MessageBox.Show("Have lymph nodes?(Tiene ganglios)", SCRIPT_NAME, MessageBoxButtons.YesNoCancel);
            if (nodes == DialogResult.Cancel) return;
            DialogResult side = System.Windows.Forms.MessageBox.Show("Side of treatment?(Lado de tratamiento): "+"\n" + "If Yes, is Right (Derecha)"+"\n" + "If No, is Left(Izquierda)" + "\n" +"If Cancel, Exit", SCRIPT_NAME, MessageBoxButtons.YesNoCancel);
            ////////////////////////////////////////////////////////////////////////////
            //Structures esenciales para correr el plan
            string[] mama_D = { "Mama_Der" };  //"PTV_52Gy"
            string[] mama_I = { "Mama_I" };  //"PTV_52Gy"
            string[] N_SIB = { "GTV_SIB", "10-SIB", "8 SIB" };
            string[] PTV_ID24 = { "zPTV_Total!" };      //PTV_Total
            string[] PTV_ID20 = { "zPTV_High_5200!" };  //"PTV_52Gy" sc16
            string[] PTV_ID21 = { "zPTV_Low_4000!" };   //PTV_40Gy sc16
            string[] PTV_ID22 = { "zPTV_Gang_4300!" };   //PTV_41Gy c16
            string[] PTV_ID23 = { "zPTV_Prox_4300!" };   //PTV_43.2Gy sc16
            string[] PTV_ID20_ = { "zPTV_High_5600!" }; //PTV_56.4Gy//tengo un problema con los IDS por eso le quito el signo de admiracion sc20
            string[] PTV_ID21_ = { "zPTV_Low_4300!" };  //PTV_43Gy sc20
            string[] PTV_ID22_ = { "zPTV_Gang_4600!" };  //PTV_46Gy c20
            string[] PTV_ID23_ = { "zPTV_Prox_4600!" };  //PTV_45.4Gy sc20
            //Chest wall 16fx
            string[] PTV_ID25 = { "zPTV_High_4400!" };  //PTV_44Gy
            //Chest wall 20fx
            string[] PTV_ID25_ = { "zPTV_High_4700!" }; //PTV_47Gy//problema con el id
            string[] N_Bowel = { "Bowel", "bowels", "intestinos", "Intestino", "intestino", "Delgado" };

            Structure ptv_total = CT_modificada.Structures.FirstOrDefault(x => PTV_ID24.Any(s => s == x.Id));//ptv total
            Structure sib = CT_modificada.Structures.FirstOrDefault(x => N_SIB.Any(s => s == x.Id));//ptv total
            Structure bowel = CT_modificada.Structures.FirstOrDefault(x => N_Bowel.Any(s => s == x.Id));//ptv total

            /////////////////////////////////////////////////////////////////////////Prescripcion siempre es de 5 fraction
            double RxDose = 52.0;//por defecto esta en 36.25
            int NFractions = 16;
            string[] setting_names = { "Mama_RA", "Mama", "Field", "6X", "ARC" };// 0=nombre del course y 1=del plan, 2=field, 3=energia, 4= srsarc o arc 5=rapidplan

            string[] rapidplan = { "MAMA_SIMPLE_DERECHA_16Fx", "MAMA_SIMPLE_DERECHA_20Fx", "MAMA_SIMPLE_IZQUIERDA_16Fx", "MAMA_SIMPLE_IZQUIERDA_20Fx", "MAMA_COMPLETA_DERECHA_16Fx", "MAMA_COMPLETA_DERECHA_20Fx", "MAMA_COMPLETA_IZQUIERDA_16Fx", "MAMA_COMPLETA_IZQUIERDA_20Fx" };
            Double[] setting_arc = { 20, 181, 60 };//por defecto mama der simple
            Double[] shift = { 0, 0, 0 };//por defecto esta en 0 luego cambia
            /////////////////////////////////////////////////////////////////////////////
            //list de oars importantes
            List<Structure> VIP_oar = new List<Structure>();
            List<string[]> PTVs_names = new List<string[]>();//esto para pasar los nombres del ptvs que existen
            /////////////////////////////////////////////////// Comienza la generacion de planes
            if (tto == DialogResult.Yes)//mama
            {
                if (!CT_modificada.Structures.Any(x => x.Id == PTV_ID24[0]))//busca ptv total
                {
                    System.Windows.MessageBox.Show(PTV_ID24[0] + " not found, script doesnt execute");
                    return;
                }
                PTVs_names.Add(PTV_ID20); PTVs_names.Add(PTV_ID23); PTVs_names.Add(PTV_ID21);//anadir las structuras de ptv sib prox dist gg son las de por defecto
                if (fraction == DialogResult.Yes && nodes == DialogResult.No) //16fx simple20 21 23
                {
                    
                    if (side == DialogResult.Yes)//derecha
                    {
                        if (!CT_modificada.Structures.Any(x => x.Id == mama_D[0])) System.Windows.MessageBox.Show("Esta seguro que es derecha? No se pudo encontrar la estructura:" + mama_D[0]);
                        setting_names[1] = setting_names[1] + "_D+SIB";//rectum0 esto coloca el nombre mas 
                        setting_arc =new double[]{ 20, 181, 60 };
                        shift = new double[] { 0.5, 1.0, -0.5 };
                    }
                    else//izq
                    {
                        if (!CT_modificada.Structures.Any(x => x.Id == mama_I[0])) System.Windows.MessageBox.Show("Esta seguro que es izquierda? No se pudo encontrar la estructura:" + mama_I[0]);
                        setting_names[1] = setting_names[1] + "_I+SIB";//rectum0 esto coloca el nombre mas 
                    }

                    
                    ExternalPlanSetup cureps = settingPlan(0, patient, CT_modificada, RxDose, NFractions, ptv_total, setting_names, setting_arc, shift,2);

                    if (bowel == null)//intestino
                    {
                        Optimization_dose(cureps, rapidplan, PTVs_names, RxDose, 0);
                    }
                    else
                    {
                        VIP_oar.Add(bowel);
                        Opti_Bow_Col(cureps, rapidplan, PTVs_names, RxDose, VIP_oar, 0);
                    }
                    Normalization(cureps, sib, RxDose, NFractions, 95);//normaliza al valor de ptv-prvs98%
                }
                else if (fraction == DialogResult.No && nodes == DialogResult.No)//20simple
                {
                    setting_arc = new Double[] { 30, 181, 179 };
                }
                else if (fraction == DialogResult.Yes && nodes == DialogResult.Yes)//16completa
                {
                    ;
                }
                else if (fraction == DialogResult.No && nodes == DialogResult.No)//20completa
                {
                    if (!CT_modificada.Structures.Any(x => x.Id == PTV_ID20[0]))
                    {
                        System.Windows.MessageBox.Show(PTV_ID20[0] + " not found, script doesnt execute");
                        return;
                    }
                    setting_arc = new Double[] { 30, 181, 179 };
                    PTVs_names.Add(PTV_ID20); 

                    setting_names[1] = setting_names[1] + "Prost+GG";/////////
                    ExternalPlanSetup cureps = settingPlan(0, patient, CT_modificada, RxDose, NFractions, ptv_total, setting_names, setting_arc, shift,2);
                    if (bowel == null )//intestino
                    {
                        Optimization_dose(cureps, rapidplan, PTVs_names, RxDose, 1);
                    }
                    else
                    {
                        if (bowel != null )
                        {
                            VIP_oar.Add(bowel);
                            Opti_Bow_Col(cureps, rapidplan, PTVs_names, RxDose, VIP_oar, 1);
                        }
                        else if (bowel != null)
                        {
                            VIP_oar.Add(bowel);
                            Opti_Bow_Col(cureps, rapidplan, PTVs_names, RxDose, VIP_oar, 1);
                        }
                    }
                    Normalization(cureps, sib, RxDose, NFractions, 95);//normaliza al valor de ptv-prvs98%
                }


            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No esta desarrollado aun, contactese con su desarrollador local");
            }

        }



    }
}
