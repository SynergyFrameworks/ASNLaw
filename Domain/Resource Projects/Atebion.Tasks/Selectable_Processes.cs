using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atebion.Tasks
{
    public static class Selectable_Processes
    {
        public static string GenerateShallList_Process = "GenerateShallList";
        public static string GenerateShallList_Name = "Generate Shall List";
        public static string GenerateShallList_Description = "Generate a 'Shall' list from an RFx.";
        public static string GenerateShallList_Instructions = "Select an RFx document and click the Analyze button. After the analysis has been completed, Excel will automatically open the generated Shall list.";
        public static string GenerateShallList_StepText = "Select an RFx document and run analysis|Review Shall list";

        public static string QuickStartCM_Process = "QuickStartCM1";
        public static string QuickStartCM_Name = "Quick Start Compliance Matrix 1";
        public static string QuickStartCM_Description = "Quickly generate a starter Compliance Matrix in an Excel file. Shred a Federal Gov. RFP using the Legal parser, use template CM1, use keyword group Required to help identify requirements.";
        public static string QuickStartCM_Instructions = "Select an RFP document and click the Analyze button|Review the Compliance Matrix";
        public static string QuickStartCM_StepText = "Select an RFP and Run the Analysis|View Compliance Matrix";

        public static string QuickStartCMWithAR_Process = "QuickStartCMWithAR";
        public static string QuickStartCMWithAR_Name = "Quick Start Compliance Matrix 2 with Analysis Results";
        public static string QuickStartCMWithAR_Description = "Quickly generate Analysis Results for a starter Compliance Matrix. Shred a Federal Gov. RFP using the Legal parser, and use keyword group Required to help identify requirements, and display the Analysis Results panel.";
        public static string QuickStartCMWithAR_Instructions = "Select an RFP document and click the Analyze button|Review the Analysis Results and click the Export button to generate a Compliance Matrix";
        public static string QuickStartCMWithAR_StepText = "Select an RFP and Run the Analysis|Review the Analysis Results and click the Export button";


        public static string QuickStartCMClassic_Process = "QuickStartCMClassic";
        public static string QuickStartCMClassic_Name = "Quick Start Compliance Matrix 3 with Keywords selection and Analysis Results";
        public static string QuickStartCMClassic_Description = "Quickly generate Analysis Results for a starter Compliance Matrix. Shred a Federal Gov. RFP using the Legal parser, Keyword group selectable, and display the Analysis Results panel.";
        public static string QuickStartCMClassic_Instructions = "Select an RFP document and click the Analyze button|Review the Analysis Results and click the Export button to generate a Compliance Matrix";
        public static string QuickStartCMClassic_StepText = "Select an RFP |Select Keywords and Run the Analysis|Review the Analysis Results and click the Export button";

        public static string XRefCMwithARAndMB1_Process = "XRefCMwithARAndMB1";
        public static string XRefCMwithARAndMB1_Name = "Cross-Reference Compliance Matrix 1";
        public static string XRefCMwithARAndMB1_Description = "Parse Federal RFP documents sections (e.g. C, L, & M) and generate a Cross-Reference Compliance Matrix";
        public static string XRefCMwithARAndMB_Instructions = "Select Federal RFP documents sections (e.g. C, L, & M). Split the RFP into separate documents per the sections. Review the Analysis Results, add comments/notes, split/combine and remove unwanted parsed segments. Once you are satisfied with the Analysis Results, click the Generate a Cross-Reference Compliance Matrix. After the Cross-Reference Compliance Matrix has been created, drag-and-drop parse segments, win themes, discriminators, and writer assignments into Compliance Matrix."; 
        public static string XRefCMwithARAndMB_StepText = "Select Federal RFP documents sections (e.g. C, L, & M)|Review the Analysis Results for each section Doc.|Generate Cross-Reference Compliance Matrix";

    }
}
