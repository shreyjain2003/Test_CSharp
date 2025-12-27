using System;

namespace Test_CSharp.MediSure_Clinic_Bill
{
    /// <summary>
    /// Holds billing details of a patient including charges and final payable amount.
    /// </summary>
    public class PatientBill
    {
        /// <summary>Unique bill identifier.</summary>
        public string BillId { get; set; }

        /// <summary>Patient full name.</summary>
        public string PatientName { get; set; }

        /// <summary>Indicates whether the patient has insurance.</summary>
        public bool HasInsurance { get; set; }

        /// <summary>Consultation fee charged.</summary>
        public decimal ConsultationFee { get; set; }

        /// <summary>Laboratory charges.</summary>
        public decimal LabCharges { get; set; }

        /// <summary>Medicine charges.</summary>
        public decimal MedicineCharges { get; set; }

        /// <summary>Total amount before discount.</summary>
        public decimal GrossAmount { get; set; }

        /// <summary>Discount applied based on insurance.</summary>
        public decimal DiscountAmount { get; set; }

        /// <summary>Final amount payable by patient.</summary>
        public decimal FinalPayable { get; set; }
    }

    /// <summary>
    /// Console application entry point for MediSure Clinic Billing system.
    /// </summary>
    class Program1
    {
        /// <summary>Stores the most recently created bill.</summary>
        static PatientBill LastBill = null;

        /// <summary>Indicates if a bill exists.</summary>
        static bool HasLastBill = false;

        /// <summary>
        /// Application entry point and menu controller.
        /// </summary>
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("================== MediSure Clinic Billing ==================");
                Console.WriteLine("1. Create New Bill");
                Console.WriteLine("2. View Last Bill");
                Console.WriteLine("3. Clear Last Bill");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your option: ");

                string pickone = Console.ReadLine();
                Console.WriteLine();

                switch (pickone)
                {
                    case "1":
                        CreateNewBill();
                        break;
                    case "2":
                        ViewLastBill();
                        break;
                    case "3":
                        ClearLastBill();
                        break;
                    case "4":
                        Console.WriteLine("Application closed.");
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        /// <summary>
        /// Creates a new patient bill and performs billing calculations.
        /// </summary>
        #region CreateNewBill
        public static void CreateNewBill()
        {
            PatientBill patientbill = new PatientBill();

            Console.Write("Enter Bill Id: ");
            patientbill.BillId = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(patientbill.BillId)) return;

            Console.Write("Enter Patient Name: ");
            patientbill.PatientName = Console.ReadLine();

            Console.Write("Is the patient insured? (Y/N): ");
            patientbill.HasInsurance = Console.ReadLine()
                .Equals("Y", StringComparison.OrdinalIgnoreCase);

            Console.Write("Enter Consultation Fee: ");
            patientbill.ConsultationFee = Convert.ToDecimal(Console.ReadLine());
            if (patientbill.ConsultationFee <= 0) return;

            Console.Write("Enter Lab Charges: ");
            patientbill.LabCharges = Convert.ToDecimal(Console.ReadLine());
            if (patientbill.LabCharges < 0) return;

            Console.Write("Enter Medicine Charges: ");
            patientbill.MedicineCharges = Convert.ToDecimal(Console.ReadLine());
            if (patientbill.MedicineCharges < 0) return;

            patientbill.GrossAmount = patientbill.ConsultationFee + patientbill.LabCharges + patientbill.MedicineCharges;
            patientbill.DiscountAmount = patientbill.HasInsurance ? patientbill.GrossAmount * 0.10m : 0;
            patientbill.FinalPayable = patientbill.GrossAmount - patientbill.DiscountAmount;

            LastBill = patientbill;
            HasLastBill = true;

            Console.WriteLine("Bill created successfully.");
        }
        #endregion


        /// <summary>
        /// Displays the last created bill.
        /// </summary>
        #region ViewLastBill
        public static void ViewLastBill()
        {
            if (!HasLastBill)
            {
                Console.WriteLine("No bill available.");
                return;
            }

            Console.WriteLine($"BillId: {LastBill.BillId}");
            Console.WriteLine($"Patient: {LastBill.PatientName}");
            Console.WriteLine($"Final Payable: {LastBill.FinalPayable:F2}");
        }
        #endregion


        /// <summary>
        /// Clears the last stored bill.
        /// </summary>
        #region ClearLastBill
        public static void ClearLastBill()
        {
            LastBill = null;
            HasLastBill = false;
            Console.WriteLine("Last bill cleared.");
        }
        #endregion
    }
}
